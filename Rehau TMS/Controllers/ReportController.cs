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
        public ActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
            {
                string msg = "Wybierz zakres danych";
                var emptyViewModel = new ReportsViewModel
                {
                    EmptyModelMessage = msg
                };
                return View(emptyViewModel);
            }

            //Declare reports here
            var worktypereport = _context.Schedule.Where(s => s.WorkType != null && s.Date >= startDate && s.Date <= endDate).ToList();


            //Assign reports data here
            var viewModel = new ReportsViewModel
            {
                Schedules = worktypereport
            };

            return View(viewModel);
        }
    }
}