using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Rehau_TMS.Models;
using System.ComponentModel.DataAnnotations;

namespace Rehau_TMS.Controllers
{
    [AllowAnonymous]
    public class RoleController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Role
        public ActionResult Index()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }

        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
               _context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                _context.SaveChanges();
                ViewBag.ResultMessage = "Rola została dodana.";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: /Roles/Edit/5
        public ActionResult Edit(string roleName)
        {
            var thisRole = _context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                _context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string RoleName)
        {
            var thisRole = _context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            _context.Roles.Remove(thisRole);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}