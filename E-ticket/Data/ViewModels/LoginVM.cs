using System.ComponentModel.DataAnnotations;

namespace E_ticket.Data.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Email is required")]
        [Display(Name ="Email Address")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
