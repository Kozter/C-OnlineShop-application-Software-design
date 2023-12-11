using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineShoppApp.Models;

public class Payment
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey(nameof(Customer))]
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    [ForeignKey(nameof(Order))]
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }

    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? PaymentType { get; set; } // e.g., "Credit", "Debit", "EPayment"
}