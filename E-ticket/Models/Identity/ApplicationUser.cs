using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_ticket.Models.Identity
{
    public class ApplicationUser:IdentityUser
    {
        [Display(Name ="Full Name")]
        public string FullName { get; set; }
    }
}
