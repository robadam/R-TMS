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
                var emptyViewModel = new ReportsViewModel
                {
                    ModelControl = false
                };
                return View(emptyViewModel);
            }
            else
            {
                var schedules = _context.Schedule.Where(s => s.Date >= startDate && s.Date <= endDate).ToList();
                var viewModel = new ReportsViewModel
                {
                    ModelControl = true,
                    Schedules = schedules
                };

                return View(viewModel);
            }
        }
    }
}