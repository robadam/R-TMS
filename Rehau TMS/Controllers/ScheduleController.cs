﻿using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rehau_TMS.ViewModels;
using Rehau_TMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Rehau_TMS.Controllers
{
    public class ScheduleController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Schedule
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

    }
}