using System;
using System.Collections.Generic;

namespace WebApplication4;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public bool FirstSign { get; set; }

    public DateTime? LastVisit { get; set; }

    public bool IsBlocked { get; set; }

    public int? ErrorCount { get; set; }

    public virtual Role Role { get; set; } = null!;
}

public partial class UserModel
{

    public int Id { get; set; }

    public string Login { get; set; } 

    public string Password { get; set; } 

    public int RoleId { get; set; }

    public bool FirstSign { get; set; }

    public DateTime? LastVisit { get; set; }

    public bool IsBlocked { get; set; }

    public int? ErrorCount { get; set; }

    public RoleModel Role { get; set; }

    public static explicit operator User (UserModel from)
    {
        return new User { Id = from.Id, ErrorCount = from.ErrorCount, Login = from.Login, Password = from.Password, RoleId = from.RoleId, FirstSign = from.FirstSign, LastVisit = from.LastVisit, IsBlocked = from.IsBlocked};
    }

    public static explicit operator UserModel(User from)
    {
        return new UserModel { Id = from.Id, ErrorCount = from.ErrorCount, Login = from.Login, Password = from.Password, RoleId = from.RoleId, FirstSign = from.FirstSign, LastVisit = from.LastVisit, IsBlocked = from.IsBlocked, Role = (RoleModel)from.Role };
    }
}
