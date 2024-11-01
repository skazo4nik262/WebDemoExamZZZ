using System;
using System.Collections.Generic;

namespace WebApplication4;

public partial class Fond
{
    public int Id { get; set; }

    public int? FloorId { get; set; }

    public int? Number { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Floor? Floor { get; set; }
}
