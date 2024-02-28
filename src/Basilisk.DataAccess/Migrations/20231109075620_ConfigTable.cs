using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Basilisk.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ConfigTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Role = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configs",
                columns: table => new
                {
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configs", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ContactPerson = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    City = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Cost = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Remark = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salesmen",
                columns: table => new
                {
                    EmployeeNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Level = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    HiredDate = table.Column<DateTime>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    City = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    SuperiorEmployeeNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salesmen", x => x.EmployeeNumber);
                    table.ForeignKey(
                        name: "FK_Salesmen_Salesmen",
                        column: x => x.SuperiorEmployeeNumber,
                        principalTable: "Salesmen",
                        principalColumn: "EmployeeNumber");
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ContactPerson = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    JobTitle = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    City = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    InvoiceNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    SalesEmployeeNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "date", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "date", nullable: true),
                    DueDate = table.Column<DateTime>(type: "date", nullable: true),
                    DeliveryId = table.Column<long>(type: "bigint", nullable: false),
                    DeliveryCost = table.Column<decimal>(type: "money", nullable: false),
                    DestinationAddress = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    DestinationCity = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DestinationPostalCode = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.InvoiceNumber);
                    table.ForeignKey(
                        name: "FK_Orders_Customers",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Deliveries",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Salesmen",
                        column: x => x.SalesEmployeeNumber,
                        principalTable: "Salesmen",
                        principalColumn: "EmployeeNumber");
                });

            migrationBuilder.CreateTable(
                name: "SalesmenRegions",
                columns: table => new
                {
                    SalesmanEmployeeNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    RegionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesmenRegions", x => new { x.SalesmanEmployeeNumber, x.RegionId });
                    table.ForeignKey(
                        name: "FK_SalesmenRegions_Regions",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalesmenRegions_Salesmen",
                        column: x => x.SalesmanEmployeeNumber,
                        principalTable: "Salesmen",
                        principalColumn: "EmployeeNumber");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    OnOrder = table.Column<int>(type: "int", nullable: false),
                    Discontinue = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Suppliers",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    InvoiceNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails_1", x => new { x.InvoiceNumber, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders",
                        column: x => x.InvoiceNumber,
                        principalTable: "Orders",
                        principalColumn: "InvoiceNumber");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "UQ_Categories_CategoryName",
                table: "Categories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryId",
                table: "Orders",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SalesEmployeeNumber",
                table: "Orders",
                column: "SalesEmployeeNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Salesmen_SuperiorEmployeeNumber",
                table: "Salesmen",
                column: "SuperiorEmployeeNumber");

            migrationBuilder.CreateIndex(
                name: "IX_SalesmenRegions_RegionId",
                table: "SalesmenRegions",
                column: "RegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Configs");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "SalesmenRegions");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Salesmen");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
