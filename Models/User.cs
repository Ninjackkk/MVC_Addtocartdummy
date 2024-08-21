using Microsoft.AspNetCore.Mvc;

namespace MVC_Addtocartdummy.Models
{
    public class User
    {
        public int Id { get; set; }
        [Remote(action: "CheckExisting",controller:"Auth")] // redirecting the data to validation url
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }


    }
}
