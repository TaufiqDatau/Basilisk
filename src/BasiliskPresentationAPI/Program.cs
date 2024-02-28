using Basilisk.Busines.Interface;
using Basilisk.Busines.Repositories;
using Basilisk.DataAccess;
using Basilisk.Presentation.API.Helpers;
using Basilisk.Presentation.API.Suppliers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using Basilisk.Presentation.API.Auth;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

Dependencies.AddDataAccessServices(builder.Services, builder.Configuration);

builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<SupplierService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddSwaggerGen(
                options =>
                {
                    options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Description = "Standard auth header using the bearer scheme",
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });
                    options.OperationFilter<SecurityRequirementsOperationFilter>(); //dotnet add package Swashbuckle.AspNetCore.Filters;
                }
            );

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                               builder.Configuration.GetSection("AppSettings:TokenSignature").Value))
                    });

builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = false
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Basilisk API"});
});

builder.Services.AddCors(options =>

    options.AddPolicy(name: "_webBasilisk",
        policy => policy.WithOrigins("http://localhost:5127")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        ));

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseCors("_webBasilisk");
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(
    configuration=> configuration.SwaggerEndpoint("/swagger/v1/swagger.json", "Basilisk API V1"));
app.Run();
