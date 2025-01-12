using System;
using System.Collections.Generic;

namespace PrimeStore.Data.Models;

public partial class SharedFile
{
    public string Guid { get; set; }

    public int FileId { get; set; }

    public virtual File File { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
