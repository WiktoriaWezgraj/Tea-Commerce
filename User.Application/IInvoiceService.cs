using User.Domain;

namespace User.Application;

public interface IInvoiceService
{
    Task GenerateAndSendInvoiceAsync(InvoiceRequest request);
}
