using ShoppingCart.Domain.Models;

namespace ShoppingCart.Application.Services;

public interface IInvoiceService
{
    Task GenerateAndSendInvoiceAsync(Order order, string recipientEmail);
}

