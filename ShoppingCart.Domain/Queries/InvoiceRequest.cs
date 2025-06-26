namespace ShoppingCart.Domain.Queries;

public class SendInvoiceRequest
{
    public int CartId { get; set; }
    public string Email { get; set; } = string.Empty;
}

