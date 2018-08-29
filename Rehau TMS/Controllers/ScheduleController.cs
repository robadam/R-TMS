using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rehau_TMS.Controllers
{
    [Authorize]
    public class ScheduleController : Controller
    {
        // GET: Account/Login
        public ActionResult Index()
        {
            return View();
        }

    }
}