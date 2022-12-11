using E_ticket.Data.Repository;
using System.ComponentModel.DataAnnotations;

namespace E_ticket.Models
{
    public class Actor:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Profile Picture")]
        [Required(ErrorMessage = "profile url is required")]
        public string ProfileURL { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full name is required")]

        public string FullName { get; set; }

        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography  is required")]

        public string Bio { get; set; }


        public List<Actor_Movie> Actor_Movies { get; set; }
    }
}
