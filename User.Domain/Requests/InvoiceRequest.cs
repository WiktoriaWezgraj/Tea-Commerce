namespace User.Domain;

public class InvoiceRequest
{
    public string Email { get; set; } = default!;
    public List<string> ProductNames { get; set; } = new();
    public decimal TotalPrice { get; set; }
}

