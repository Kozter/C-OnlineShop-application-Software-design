using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShoppApp.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? Telephone { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
