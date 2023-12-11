using System.ComponentModel.DataAnnotations;

public class CartItem
{
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
    public int Quantity { get; set; }
    [Range(0.0, double.MaxValue, ErrorMessage = "Price must be a non-negative number.")]
    public decimal Price { get; set; }
    public decimal Amount() => (Quantity * Price);

    public override string ToString()
    {
        return $"Cart:: [Product: {ProductName}, Quantity: {Quantity}, Unit Price: {Price}, Amount: {Amount()}]\n";
    }
}
