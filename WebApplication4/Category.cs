using System;
using System.Collections.Generic;

namespace WebApplication4;

public partial class Category
{
    public int Id { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<FondStatus> FondStatuses { get; set; } = new List<FondStatus>();

    public virtual ICollection<Fond> Fonds { get; set; } = new List<Fond>();

    public virtual ICollection<Postoialci> Postoialcis { get; set; } = new List<Postoialci>();
}
