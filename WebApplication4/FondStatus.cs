using System;
using System.Collections.Generic;

namespace WebApplication4;

public partial class FondStatus
{
    public int Id { get; set; }

    public int? Number { get; set; }

    public int? CategotyId { get; set; }

    public string? Status { get; set; }

    public DateOnly? OutDate { get; set; }

    public virtual Category? Categoty { get; set; }
}
