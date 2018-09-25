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

        [HttpGet]
        public ActionResult Index(DateTime? start, DateTime? end, string user)
        {
            if (start == null)
            {
                start = DateTime.Today.AddDays(-7);
            }
            if (end == null)
            {
                end = DateTime.Today;
            }
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                if (user == "0" || user == null)
                {
                    var adminschedulelistdefault = _context.Schedule
                    .Include(s => s.ApplicationUser)
                    .Include(s => s.Article)
                    .Include(s => s.Tool)
                    .Include(s => s.WorkType)
                    .Where(s => s.Date >= start && s.Date <= end)
                    .ToList()
                    .OrderByDescending(o => o.Id);
                    return View(adminschedulelistdefault);
                }
                else
                {
                    var adminschedulelist = _context.Schedule
                        .Include(s => s.ApplicationUser)
                        .Include(s => s.Article)
                        .Include(s => s.Tool)
                        .Include(s => s.WorkType)
                        .Where(s => s.Date >= start && s.Date <= end && s.ApplicationUserId == user)
                        .ToList()
                        .OrderByDescending(o => o.Id);
                    return View(adminschedulelist);
                }
            };

            string curruser = User.Identity.GetUserId();
            var schedulelist = _context.Schedule.Where(u => u.ApplicationUserId == curruser)
                .Include(s => s.ApplicationUser)
                .Include(s => s.Article)
                .Include(s => s.Tool)
                .Include(s => s.WorkType)
                .Where(s => s.Date >= start && s.Date <= end)
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
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(schedule).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index", "Schedule");
            }

            return View(schedule);
        }

        public JsonResult GetArticlesList()
        {
            _context.Configuration.ProxyCreationEnabled = false;
            List<Article> ArticlesList = _context.Article.Where(x => x.Status == true).ToList();
            var t = new Article();
            t.Name = "Wybierz artyku³";
            ArticlesList.Insert(0, t);
            return Json(ArticlesList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetToolsList(int ArticlesModelsId)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            List<Tool> ToolsList = _context.Tool.Where(x => x.ToolStatusId < 3 && x.Id > 0 && x.ArticleId == ArticlesModelsId).ToList();
            var t = new Tool();
            t.Name = "--Wybierz narzêdzie--";
            ToolsList.Insert(0, t);
            return Json(ToolsList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetOptionsList(int ToolsModelsid)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            List<Tool> t = _context.Tool.Where(a => a.Id == ToolsModelsid).ToList();
            var st = t.First();
            int state = st.ToolStatusId;
            List<Options> OptionsList = _context.Options.Where(x => x.ToolsModelStateId == state && x.Id > 0).ToList();
            var o = new Options();
            o.Name = "--Wybierz opcjê--";
            OptionsList.Insert(0, o);
            return Json(OptionsList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOptionsAdditionalList(int OptionsModelsId)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            List<OptionsAdditional> OptionsAdditionalsList = _context.OptionsAdditional.Where(x => x.OptionsId == OptionsModelsId && x.Id > 0).ToList();
            var o = new OptionsAdditional();
            o.Name = "--Wybierz podopcjê--";
            OptionsAdditionalsList.Insert(0, o);
            return Json(OptionsAdditionalsList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserList()
        {
            _context.Configuration.ProxyCreationEnabled = false;
            List<ApplicationUser> Users = _context.Users.ToList();
            return Json(Users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Report()
        {

            return View();
        }
    }
}