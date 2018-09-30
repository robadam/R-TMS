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
            return View();
        }
    }
}