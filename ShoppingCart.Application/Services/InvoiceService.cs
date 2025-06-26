using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using ShoppingCart.Domain.Models;
using System.Reflection.Metadata;

namespace ShoppingCart.Application.Services;

public class InvoiceService : IInvoiceService
{
    private readonly SmtpSettings _smtp;

    public InvoiceService(IOptions<SmtpSettings> smtpOptions)
    {
        _smtp = smtpOptions.Value;
    }

    public async Task GenerateAndSendInvoiceAsync(Order order, string recipientEmail)
    {
        var fileName = $"Invoice_{order.OrderId}.pdf";
        var filePath = Path.Combine(Path.GetTempPath(), fileName);

        // Generowanie PDF
        QuestPDF.Fluent.Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.Content().Column(col =>
                {
                    col.Item().Text($"FAKTURA #{order.OrderId}").FontSize(20).Bold();
                    col.Item().Text($"Data: {order.CreatedAt:yyyy-MM-dd}");
                    col.Item().LineHorizontal(1);

                    foreach (var item in order.Items)
                    {
                        col.Item().Text($"Produkt {item.ProductId}, Ilość: {item.Quantity}, Cena: {item.Price:C}");
                    }

                    col.Item().LineHorizontal(1);
                    col.Item().Text($"Łączna kwota: {order.Items.Sum(i => i.Quantity * i.Price):C}").Bold();
                });
            });
        }).GeneratePdf(filePath);

        using var client = new SmtpClient(_smtp.Host, _smtp.Port)
        {
            Credentials = new NetworkCredential(_smtp.SenderEmail, _smtp.Password),
            EnableSsl = true
        };

        using var message = new MailMessage(_smtp.SenderEmail, recipientEmail)
        {
            Subject = $"Faktura #{order.OrderId}",
            Body = "W załączniku przesyłamy fakturę za Twoje zamówienie.",
        };

        using (var attachment = new Attachment(filePath))
        {
            message.Attachments.Add(attachment);
            await client.SendMailAsync(message);
        }

        File.Delete(filePath);

    }
}
