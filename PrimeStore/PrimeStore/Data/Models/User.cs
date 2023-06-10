using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeStore.Data.Models;

public partial class User : IdentityUser
{
    public override string Id { get; set; }  

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Username { get; set; } = null!;

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();

    public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();
}
