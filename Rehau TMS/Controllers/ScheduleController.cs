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
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                var adminschedulelist = _context.Schedule
                    .Include(s => s.ApplicationUser)
                    .Include(s => s.Article)
                    .Include(s => s.Tool)
                    .Include(s => s.WorkType)
                    .ToList()
                    .OrderByDescending(o => o.Id);

                return View(adminschedulelist);
            };

            string curruser = User.Identity.GetUserId();
            var schedulelist = _context.Schedule.Where(u => u.ApplicationUserId == curruser)
                .Include(s => s.ApplicationUser)
                .Include(s => s.Article)
                .Include(s => s.Tool)
                .Include(s => s.WorkType)
                .ToList()
                .OrderByDescending(o => o.Id);
            
            return View(schedulelist);
        }

        public ActionResult Create()
        {
            var appusers = _context.Users.Where(a => a.Name != "Admin" && a.IsActive == true).ToList();
            var articles = _context.Article.Where(a => a.Status == true).ToList();
            var options = _context.Options.ToList();
            var optionsadd = _context.OptionsAdditional.ToList();
            var tools = _context.Tool.ToList();
            var worktypes = _context.WorkType.ToList();

            var viewModel = new ScheduleViewModel
            {
                ApplicationUsers = appusers,
                Articles = articles,
                Options = options,
                OptionsAdditionals = optionsadd,
                Tools = tools,
                WorkTypes = worktypes
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Schedule schedule)
        {
            _context.Schedule.Add(schedule);
            _context.SaveChanges();
            return RedirectToAction("Index", "Schedule");
        }

        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Edit(int Id)
        {
            var schedule = _context.Schedule.SingleOrDefault(s => s.Id == Id);
            if (schedule == null)
                return HttpNotFound();

            var viewModel = new ScheduleViewModel
            {
                Schedule = schedule,

                ApplicationUsers = _context.Users.Where(a => a.Name != "Admin" && a.IsActive == true).ToList(),
                Articles = _context.Article.Where(a => a.Status == true).ToList(),
                Options = _context.Options.ToList(),
                OptionsAdditionals = _context.OptionsAdditional.ToList(),
                Tools = _context.Tool.ToList(),
                WorkTypes = _context.WorkType.ToList()
            };

            return View(viewModel);
        }

    }
}