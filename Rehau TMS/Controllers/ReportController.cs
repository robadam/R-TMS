using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rehau_TMS.ViewModels;
using Rehau_TMS.Models;

namespace Rehau_TMS.Controllers
{
    public class ReportController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Report
        public ActionResult Index()
        {
            var worktypereport = _context.Schedule.Where(s => s.WorkType != null).ToList();

            var viewModel = new RepWorkTypeViewModel
            {
                Schedules = worktypereport
            };

            return View(viewModel);
        }
    }
}