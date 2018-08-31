using System;
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
    [Authorize(Roles = "Admin, Moderator")]
    public class ToolController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Tool
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.ToolsStatusId = new SelectList(_context.ToolStatus, "Id", "Name");
            ViewBag.ArticleId = new SelectList(_context.Article, "Id", "Name");
            return View();
        }

        public ActionResult Edit()
        {
            ViewBag.ToolsStatusId = new SelectList(_context.ToolStatus, "Id", "Name");
            ViewBag.ArticleId = new SelectList(_context.Article, "Id", "Name");
            return View();
        }
    }
}