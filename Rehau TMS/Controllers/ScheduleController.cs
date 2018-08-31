using System;
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
            var schedulelist = _context.Schedule
                .Include(s => s.ApplicationUser)
                .Include(s => s.Article)
                .Include(s => s.Tool)
                .Include(s => s.WorkType)
                .ToList();

            return View(schedulelist);
        }

        public ActionResult Create()
        {
            var appusers = _context.Users.ToList();
            var articles = _context.Article.ToList();
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

        public ActionResult Edit()
        {
            return View();
        }

    }
}