using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_ticket.Models.Identity;

namespace E_ticket.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }


        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
