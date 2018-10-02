using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rehau_TMS.Models;
using System.ComponentModel.DataAnnotations;

namespace Rehau_TMS.ViewModels
{
    public class RepWorkTypeViewModel
    {
        public IEnumerable<Schedule> Schedules { get; set; }
    }
}