using PrimeStore.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PrimeStore.ViewModels
{
    public class FileViewModel
    {

        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }

    }
}
