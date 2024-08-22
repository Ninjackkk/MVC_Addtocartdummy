using System.ComponentModel.DataAnnotations;

namespace MVC_Addtocartdummy.Models
{
    public class SignIn
    {
        [Required(ErrorMessage ="Email is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }

    }
}
