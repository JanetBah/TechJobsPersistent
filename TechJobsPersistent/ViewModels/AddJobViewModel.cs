using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(180, MinimumLength =3, ErrorMessage ="Name must be between 3 and 180 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Employer Id is required")]
        [Range(0,50,ErrorMessage ="Id must be between 0 and 50")]
        public int EmployerId { get; set; }

        public List<SelectListItem> Employers { get; set; }

        public List<Skill> Jobskill { get; set; }

        public AddJobViewModel(List<Employer> employers, List<Skill> jobskill)
        {
            Employers = new List<SelectListItem>();
            Jobskill = jobskill;

            foreach (var emp in employers)
            {
                Employers.Add(new SelectListItem
                {
                    Text = emp.Name,
                    Value = emp.Id.ToString()
                });
            }
        }

        public AddJobViewModel()
        {

        }


    }

}
