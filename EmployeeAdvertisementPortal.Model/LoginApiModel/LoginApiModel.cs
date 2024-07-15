using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeAdvertisementPortal
{
    public class LoginApiModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[\w-\.]+@gmail\.com$", ErrorMessage = "Invalid email format. Only Gmail addresses are allowed.")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
