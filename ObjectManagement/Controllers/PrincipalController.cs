using ObjectManagement.DAL;
using ObjectManagement.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ObjectManagement.Security;

namespace ObjectManagement.Controllers
{
    public class PrincipalController : Controller
    {
        private ObjectManagementContext db = new ObjectManagementContext();

        // GET: Principal/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: Principal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "PrincipalID,Username,Password")] Principal principal)
        {
            principal.Password = SecurityManager.Hash(principal.Password);
            try
            {
                if (ModelState.IsValid)
                {
                    db.Principals.Add(principal);
                    db.SaveChanges();
                    ViewBag.Message = "A new account has been created. Please log in.";
                    
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (DataException e)
            {
                ModelState.AddModelError("", "Unable to save changes. Probably a user with such name already exists. Try again with another username, and if the problem persists see your system administrator.");
            }

            return View(principal);
        }

        // GET: Principal/Create
        public ActionResult LogIn()
        {
            return View();
        }

        // POST: Principal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn([Bind(Include = "PrincipalID,Username,Password")] Principal principal)
        {
            try
            {
                principal.Password = SecurityManager.Hash(principal.Password);

                Principal existingPrincipal = db.Principals
                                        .Where(p => p.Username == principal.Username &&
                                            p.Password == principal.Password)
                                        .FirstOrDefault();

                if (existingPrincipal == null)
                {
                    ModelState.AddModelError("", "No user exists with given username/password combination.");
                    return View();
                }
               
                HttpContext.Session["principalID"] = existingPrincipal.PrincipalID;
                return RedirectToAction("Index", "Item");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(principal);
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
