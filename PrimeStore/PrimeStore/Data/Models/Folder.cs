using System;
using System.Collections.Generic;

namespace PrimeStore.Data.Models;

public partial class Folder
{
    public int Id { get; set; }

    public string Foldername { get; set; } = null!;

    public DateTime CreationTime { get; set; }

    public int UserId { get; set; }

    public int[]? IdNextFolder { get; set; }

    public int[]? IdPreviousFolder { get; set; }

    public virtual ICollection<File> Files { get; set; } = new List<File>();

    public virtual User User { get; set; } = null!;
}
