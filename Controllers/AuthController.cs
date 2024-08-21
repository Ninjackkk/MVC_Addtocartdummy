using Microsoft.AspNetCore.Mvc;
using MVC_Addtocartdummy.Data;
using MVC_Addtocartdummy.Models;

namespace MVC_Addtocartdummy.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext db;

        public AuthController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(User u)
        {
            u.Role = "User";
            db.User.Add(u);
            db.SaveChanges();
            return RedirectToAction("SignIn");
        }

        [AcceptVerbs("Post","Get")]                       // using Action Verbs 
        public IActionResult CheckExisting(string email) // this action name must be mentioned using remote keyword in the specific model , in this case User
        {
            var data = db.User.Where(x => x.Email == email).SingleOrDefault();
            if(data!=null)
            {
                return Json($"Email {email} already exists");
            }
            else
            {
                return Json(true);
            }
        }



    }
}
