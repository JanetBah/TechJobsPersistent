using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechJobsPersistent.ViewModels
{
    public class AddEmployerViewModel
    {
        [Required(ErrorMessage ="Name is Required")]
        [StringLength(180, MinimumLength =3, ErrorMessage ="Name must be between 3 and 180")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Location is Required")]
        [StringLength(180, MinimumLength =3, ErrorMessage ="Location must be between 3 and 180")]
        public string Location { get; set; }
    }
}
