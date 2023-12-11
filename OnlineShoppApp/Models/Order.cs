using System.ComponentModel.DataAnnotations;

namespace OnlineShoppApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public DateTime OrderDate { get; set; }

        public string? Status { get; set; }

        [Required]
        public string? PaymentMethod { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
