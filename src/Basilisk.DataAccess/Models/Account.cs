using System;
using System.Collections.Generic;

namespace Basilisk.DataAccess.Models;

public partial class Account
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public RoleEnum Role { get; set; } 
}
