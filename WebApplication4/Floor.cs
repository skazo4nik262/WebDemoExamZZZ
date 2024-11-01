using System;
using System.Collections.Generic;

namespace WebApplication4;

public partial class Floor
{
    public int Id { get; set; }

    public string? FloorName { get; set; }

    public virtual ICollection<Fond> Fonds { get; set; } = new List<Fond>();

    public virtual ICollection<Postoialci> Postoialcis { get; set; } = new List<Postoialci>();
}
