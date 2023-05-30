using System;
using System.Collections.Generic;

namespace PrimeStore.Data.Models;

public partial class Basket
{
    public int Id { get; set; }

    public string Path { get; set; } = null!;

    public int UserId { get; set; }

    public int[]? IdPreviousFolder { get; set; }

    public virtual ICollection<File> Files { get; set; } = new List<File>();

    public virtual User User { get; set; } = null!;
}
