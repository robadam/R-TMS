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
                WorkTypes = worktypes,
            };

            return View(viewModel);
        }

        public ActionResult Edit()
        {
            return View();
        }

    }
}