using System;
using System.Collections.Generic;

namespace WebApplication4;

public partial class Role
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
public partial class RoleModel
{
    public int Id { get; set; }

    public string RoleName { get; set; }



    public static explicit operator Role(RoleModel from)
    {
        return new Role() { Id = from.Id, RoleName = from.RoleName };
    }

    public static explicit operator RoleModel(Role from)
    {
        return new RoleModel() { Id = from.Id, RoleName = from.RoleName };
    }
}