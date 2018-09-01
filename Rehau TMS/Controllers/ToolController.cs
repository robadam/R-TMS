using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
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
            var toolStatus = _context.ToolStatus;
            var toolsList = (from tool in _context.Tool
                               select new
                               {
                                   ToolId = tool.ToolId,
                                   Name = tool.Name,
                                   ArticleId = tool.ArticlesId,
                                   SerialNumber = tool.SerialNumber,
                                   ToolStatusId = tool.ToolsStatusId
                               }).ToList().Select(t => new Tool()

                               {
                                   ToolId = t.ToolId,
                                   Name = t.Name,
                                   ArticlesId = t.ArticleId,
                                   SerialNumber = t.SerialNumber,
                                   ToolsStatusId = t.ToolStatusId
                               });
            return View(toolsList);
        }

        public ActionResult Create()
        {
            ViewBag.ToolsStatusId = new SelectList(_context.ToolStatus, "Id", "Name");
            ViewBag.ArticleId = new SelectList(_context.Article.Where(a => a.Status == true), "ArticleId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ToolId,Name,ToolsStatusId,ArticlesId,SerialNumber")] Tool toolsModels)
        {
            if (ModelState.IsValid)
            {
                _context.Tool.Add(toolsModels);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArticlesId = new SelectList(_context.Article, "ArticleId", "Name", toolsModels.ArticlesId);
            ViewBag.ToolsStatusId = new SelectList(_context.ToolStatus, "Id", "Name", toolsModels.ToolsStatusId);
            return View(toolsModels);
        }

        public ActionResult Edit(int? id)
        {
            Tool toolsModels = _context.Tool.Find(id);
            if (toolsModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArticlesId = new SelectList(_context.Article, "ArticleId", "Name", toolsModels.ArticlesId);
            ViewBag.ToolsStatusId = new SelectList(_context.ToolStatus, "Id", "Name", toolsModels.ToolsStatusId);
            return View(toolsModels);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ToolId,Name,ToolsStatusId,ArticlesId,SerialNumber")] Tool toolsModels)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(toolsModels).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArticlesId = new SelectList(_context.Article, "ArticleId", "Name", toolsModels.ArticlesId);
            ViewBag.ToolsStatusId = new SelectList(_context.ToolStatus, "Id", "Name", toolsModels.ToolsStatusId);
            return View(toolsModels);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}