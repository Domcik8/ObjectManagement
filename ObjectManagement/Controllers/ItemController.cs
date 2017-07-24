using ObjectManagement.DAL;
using ObjectManagement.Models;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ObjectManagement.Controllers
{
    public class ItemController : Controller
    {
        private ObjectManagementContext db = new ObjectManagementContext();

        // GET: Item
        public ActionResult Index()
        {
            int principalID = GetPrincipalID();
            if (principalID == 0)
                return RedirectToAction("Index", "Home");

            var dbItems = db.Items.Where(i => i.PrincipalID == principalID);
            return View(dbItems.ToList());
        }

        // GET: Item/Details/5
        public ActionResult Details(int? id)
        {
            int principalID = GetPrincipalID();
            if (principalID == 0)
                return RedirectToAction("Index", "Home");

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Item item = db.Items.Find(id);
            if (item == null)
                return HttpNotFound();

            if (item.PrincipalID != principalID)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View(item);
        }

        // GET: Item/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Due,Completed,Comment")] Item item)
        {
            int principalID = GetPrincipalID();
            if (principalID == 0)
                return RedirectToAction("Index", "Home");
            item.PrincipalID = principalID;

            try
            {
                if (ModelState.IsValid)
                {
                    db.Items.Add(item);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(item);
        }

        // GET: Item/Edit/5
        public ActionResult Edit(int? id)
        {
            int principalID = GetPrincipalID();
            if (principalID == 0)
                return RedirectToAction("Index", "Home");

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Item item = db.Items.Find(id);
            if (item == null)
                return HttpNotFound();

            if (item.PrincipalID != principalID)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View(item);
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            int principalID = GetPrincipalID();
            if (principalID == 0)
                return RedirectToAction("Index", "Home");

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var itemToUpdate = db.Items.Find(id);

            if (itemToUpdate.PrincipalID != principalID)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            bool completed = itemToUpdate.Completed;

            if (TryUpdateModel(itemToUpdate, "",
                new string[] {  "Name", "Due", "Completed", "Comment" }))
            {
                if (completed == false && itemToUpdate.Completed == true 
                    && itemToUpdate.Due < DateTime.Now)
                {
                    ModelState.AddModelError("", "Can not complete given item, it is overdue.");
                    return View(itemToUpdate);
                }

                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View(itemToUpdate);
        }

        // GET: Item/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            int principalID = GetPrincipalID();
            if (principalID == 0)
                return RedirectToAction("Index", "Home");

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (saveChangesError.GetValueOrDefault())
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";

            Item item = db.Items.Find(id);
            if (item == null)
                return HttpNotFound();

            if (item.PrincipalID != principalID)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View(item);
        }

        // POST: Principal/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            int principalID = GetPrincipalID();
            if (principalID == 0)
                return RedirectToAction("Index", "Home");

            try
            {
                Item item = db.Items.Find(id);

                if (item.PrincipalID != principalID)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                db.Items.Remove(item);
                db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private int GetPrincipalID()
        {
            if (HttpContext.Session["principalID"] == null)
                return 0;

            return (int)HttpContext.Session["principalID"];
        }
    }
}
