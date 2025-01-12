using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeStore.Data.Models;

public partial class User : IdentityUser
{
    public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();
}
