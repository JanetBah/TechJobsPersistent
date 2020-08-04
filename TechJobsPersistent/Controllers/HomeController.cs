using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;
        private object addJobViewModel;

        public Job Jobs { get; private set; }
        public string name { get; private set; }

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            //List of skills 
            List<Skill> jobSkills = context.Skills.ToList();
            List<Employer> employers = context.Employers.ToList();
            //list of employers
            AddJobViewModel addJobViewModel = new AddJobViewModel(employers, jobSkills);
           
            return View(addJobViewModel);
        }

        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string[] selectedSkills)
        {

            //checking errors in model state
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors);
            }
            if (ModelState.IsValid)
            {
                string employerName = addJobViewModel.Name;
                int employerId = addJobViewModel.EmployerId;

                                         
                
                    Job job = new Job
                    {
                        Name = employerName,
                        Employer = context.Employers.Find(employerId),
                        EmployerId =employerId
                        

                    };
                    foreach(var skill in selectedSkills)
                    {
                        JobSkill js = new JobSkill
                        {
                            Job = job,
                            SkillId = Int32.Parse(skill),
                            JobId = job.Id
                        };

                        context.JobSkills.Add(js);
                    }
                    context.Jobs.Add(job);
                    context.SaveChanges();

                    return Redirect("/Home/");
                                                
            }

            List<Skill> jobSkills = context.Skills.ToList();
            List<Employer> employers = context.Employers.ToList();
            //list of employers
            AddJobViewModel x = new AddJobViewModel(employers, jobSkills);
            addJobViewModel.Jobskill = x.Jobskill;
            addJobViewModel.Employers = x.Employers;

            return View("AddJob", addJobViewModel);

        }
        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
