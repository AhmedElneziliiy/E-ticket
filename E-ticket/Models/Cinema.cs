using E_ticket.Data.Repository;
using System.ComponentModel.DataAnnotations;

namespace E_ticket.Models
{
    public class Cinema:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Cinema Name")]
        public string Name { get; set; }
        [Display(Name = "Photo")]
        public string Logo { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }


        public List<Movie> Movies { get; set; }

    }
}
