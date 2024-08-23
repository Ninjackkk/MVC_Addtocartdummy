using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MVC_Addtocartdummy.Data;
using MVC_Addtocartdummy.Models;

namespace MVC_Addtocartdummy.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext db;
        private IWebHostEnvironment env;
        public DashboardController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public IActionResult Index(string choice)
        {

            if (choice == "LTH")
            {

                var data = db.ProductsTable.OrderBy(x => x.Price).ToList();
                return View(data);
            }
            else
            {
                var data = db.ProductsTable.ToList();
                return View(data);
            }                                                            //approach 1

            //List<Product> data;

            //if (choice == "LTH")
            //{

            //    data = db.ProductsTable.OrderBy(x => x.Price).ToList();
            //}
            //else
            //{
            //   data = db.ProductsTable.ToList();


            //}
            //return View(data);                                         //approach 2
        }           

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductViewModel pm)
        {
            var path = env.WebRootPath;
            var filePath = "Content/Images" + pm.Picture.FileName;
            var fullPath = Path.Combine(path, filePath);
            UploadFile(pm.Picture, fullPath);
            var obj = new Product()
            {
                Pname = pm.Pname,
                Pcat = pm.Pcat,
                Price = pm.Price,
                Picture = filePath
            };
            db.Add(obj);
            db.SaveChanges();
   
            return RedirectToAction("AddProduct");

        }
        public void UploadFile(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }

        public IActionResult AddToCart(int id)
        {
            string session = HttpContext.Session.GetString("suser");
            var prod=db.ProductsTable.Find(id);
            var obj = new Cart()
            {
                Pname = prod.Pname,
                Pcat = prod.Pcat,
                Picture = prod.Picture,
                Price = prod.Price,
                suser = session

            };
            db.Cart.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}





