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
            var toolsList = _context.Tool
                .Include(t => t.Articles)
                .Include(t => t.ToolStatus)
                .Where(t=>t.Id>0)
                .ToList();
            return View(toolsList);
        }

        public ActionResult Create()
        {
            ViewBag.ToolStatusId = new SelectList(_context.ToolStatus, "Id", "Name");
            ViewBag.ArticleId = new SelectList(_context.Article.Where(a => a.Status == true), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ToolStatusId,ArticleId,SerialNumber")] Tool toolsModels)
        {
            if (ModelState.IsValid)
            {
                _context.Tool.Add(toolsModels);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArticleId = new SelectList(_context.Article, "Id", "Name", toolsModels.ArticleId);
            ViewBag.ToolStatusId = new SelectList(_context.ToolStatus, "Id", "Name", toolsModels.ToolStatusId);
            return View(toolsModels);
        }

        public ActionResult Edit(int? id)
        {
            Tool toolsModels = _context.Tool.SingleOrDefault(t => t.Id == id);
            if (toolsModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArticleId = new SelectList(_context.Article, "Id", "Name", toolsModels.Articles);
            ViewBag.ToolStatusId = new SelectList(_context.ToolStatus, "Id", "Name", toolsModels.ToolStatus);
            return View(toolsModels);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tool toolsModels)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(toolsModels).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArticleId = new SelectList(_context.Article, "Id", "Name", toolsModels.ArticleId);
            ViewBag.ToolsStatuId = new SelectList(_context.ToolStatus, "Id", "Name", toolsModels.ToolStatusId);
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