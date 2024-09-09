using Bl;
using LapShop.Bl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;




namespace LapShop.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin")]

    [Area("admin")]
    public class RolesController : Controller
    {
        private LapShopContext context;

        public RolesController()
        {
            context = new LapShopContext();
        }

        // GET: Roles
        public IActionResult List()
        {
            var roles = context.Roles.ToList();
            return View(roles);
        }

        //// GET: Roles/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Roles/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(ApplicationRole role)
        {
            if (ModelState.IsValid)
            {
                context.Roles.Add(role);
                context.SaveChanges();
                return RedirectToAction("List");
            }
            return View("List",role);
        }

        // GET: Roles/Edit/5
        public IActionResult Edit(string? id)
        {
            var role = new ApplicationRole();
            if (id != null)
            {
                role = context.Roles.FirstOrDefault(a=> a.Id == id);
            }
            return View(role);
        }


        // GET: Roles/Delete/5
        public IActionResult Delete(string id)
        {
            var role = context.Roles.FirstOrDefault(a => a.Id == id);
            context.Roles.Remove(role);
            context.SaveChanges();

            return RedirectToAction("List");
        }

    }
}
