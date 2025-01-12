using System;
using System.Collections.Generic;

namespace PrimeStore.Data.Models;

public partial class File
{
    public int Id { get; set; }

    public int FolderId { get; set; }

    public string Size { get; set; } = null!;

    public DateTime UploadTime { get; set; }

    public byte[] Data { get; set; } = null!;

    public string? Filename { get; set; }

    public bool InBasket { get; set; }

    public virtual Folder Folder { get; set; } = null!;
}
