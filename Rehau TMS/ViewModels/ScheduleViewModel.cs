using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Rehau_TMS.Models;

namespace Rehau_TMS.ViewModels
{
    public class ScheduleViewModel
    {
        public Schedule Schedule { get; set; }
        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Options> Options { get; set; }
        public IEnumerable<OptionsAdditional> OptionsAdditionals { get; set; }
        public IEnumerable<Tool> Tools { get; set; }
        public IEnumerable<WorkType> WorkTypes { get; set; } 
    }
}