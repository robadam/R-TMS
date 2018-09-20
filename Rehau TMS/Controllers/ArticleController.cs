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
    public class ArticleController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Article
        public ActionResult Index()
        {
            var articleList = (from article in _context.Article
                               select new
                               {
                                   ArticleId = article.Id,
                                   Name = article.Name,
                                   SerialNumber = article.ArticleSerialNumber,
                                   Status = article.Status
                               }).ToList().Select(a => new Article()

                               {
                                   Id = a.ArticleId,
                                   Name = a.Name,
                                   ArticleSerialNumber = a.SerialNumber,
                                   Status = a.Status
                               }).Where(a=>a.Id>0);
            return View(articleList);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ArticleSerialNumber,Status")] Article articlesModels)
        {
            if (ModelState.IsValid)
            {
                _context.Article.Add(articlesModels);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(articlesModels);
        }

        public ActionResult Edit(int? id)
        {
            Article articlesModels = _context.Article.Find(id);
            if (articlesModels == null)
            {
                return HttpNotFound();
            }
            return View(articlesModels);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ArticleSerialNumber,Status")] Article articlesModels)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(articlesModels).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(articlesModels);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Details(int? id)
        {
            var articleDetails = _context.Tool.Where(t => t.ArticleId == id);
            return View(articleDetails);
        }
    }
}