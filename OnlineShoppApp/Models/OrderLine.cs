using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineShoppApp.Models;

public class OrderLine
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Order))]
    public int OrderId { get; set; }

    public virtual Order? Order { get; set; }

    [ForeignKey(nameof(Product))]
    public int ProductId { get; set; }

    public virtual Product? Product { get; set; }
    public int Quantity { get; set; }
    public decimal OrderAmount { get; set; }

}
