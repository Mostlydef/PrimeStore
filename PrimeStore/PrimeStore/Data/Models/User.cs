using System;
using System.Collections.Generic;

namespace PrimeStore.Data.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Username { get; set; } = null!;

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();

    public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();
}
