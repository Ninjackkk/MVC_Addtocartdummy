using System.Text;
using Microsoft.AspNetCore.Authentication;
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
        public static string Encrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] pass = ASCIIEncoding.ASCII.GetBytes(password);
                string encryptedpass = Convert.ToBase64String(pass);
                return encryptedpass;
            }
        }
        public static string Decrypt(string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] pass = Convert.FromBase64String(password);
                string decryptpass = ASCIIEncoding.ASCII.GetString(pass);
                return decryptpass;
            }
        }

        [HttpPost]
        public IActionResult Signup(User u)
        {
            u.Password = Encrypt(u.Password);             // using Encryption Function
            u.Role = "User";
            db.User.Add(u);
            db.SaveChanges();
            return RedirectToAction("SignIn");
        }

        [AcceptVerbs("Post","Get")]                      // using Action Verbs 
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

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(SignIn u)
        {
            var data=db.User.Where(x=>x.Email.Equals(u.Email)).SingleOrDefault();
            if(data!=null)
            {
                bool us=data.Email.Equals(u.Email)&&Decrypt(data.Password).Equals(u.Password);  //using Decrypt to decrypt the encrypted password 
                if (us)
                {
                    HttpContext.Session.SetString("myuser",data.Email);
                    if(HttpContext.Session.GetString("myuser").ToString()!=null)
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        return RedirectToAction("SignIn");
                    }
                   
                }
                else
                {
                    TempData["errorpassword"] = "Invalid Password";
                }
            }
            else
            {
                TempData["errorusername"] = "Invalid Username";

            }
            return View();

        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn", "Auth");
        }

    }
}
