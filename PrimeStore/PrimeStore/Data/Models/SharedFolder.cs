using System;
using System.Collections.Generic;

namespace PrimeStore.Data.Models;

public partial class SharedFolder
{
    public string UserId { get; set; }

    public int FolderId { get; set; }

    public virtual Folder Folder { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
