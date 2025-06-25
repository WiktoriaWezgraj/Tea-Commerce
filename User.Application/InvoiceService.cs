using Microsoft.Extensions.Options;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Net;
using System.Net.Mail;
using User.Domain;

namespace User.Application;

public class InvoiceService : IInvoiceService
{
    private readonly SmtpSettings _smtp;

    public InvoiceService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtp = smtpSettings.Value;
    }

    public async Task GenerateAndSendInvoiceAsync(InvoiceRequest request)
    {
        // 1. Wygeneruj plik PDF do pamięci
        var fileBytes = GenerateInvoicePdf(request);

        // 2. Zapisz tymczasowy plik PDF
        var tempFile = Path.GetTempFileName().Replace(".tmp", ".pdf");
        await File.WriteAllBytesAsync(tempFile, fileBytes);

        // 3. Wyślij maila
        using var client = new SmtpClient(_smtp.Host)
        {
            Port = _smtp.Port,
            Credentials = new NetworkCredential(_smtp.Email, _smtp.Password),
            EnableSsl = true
        };

        var message = new MailMessage(_smtp.Email, request.Email)
        {
            Subject = "Twoja faktura",
            Body = "W załączniku znajdziesz fakturę za zamówienie."
        };

        message.Attachments.Add(new Attachment(tempFile));
        await client.SendMailAsync(message);
    }

    private byte[] GenerateInvoicePdf(InvoiceRequest request)
    {
        var stream = new MemoryStream();

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(12));
                page.Content()
                    .Column(col =>
                    {
                        col.Item().Text("FAKTURA VAT").FontSize(20).Bold();
                        col.Item().Text($"Data: {DateTime.Now:yyyy-MM-dd}");
                        col.Item().Text($"E-mail: {request.Email}");
                        col.Item().PaddingVertical(10).LineHorizontal(1);

                        col.Item().Text("Produkty:").FontSize(14).Bold();
                        foreach (var name in request.ProductNames)
                        {
                            col.Item().Text($"• {name}");
                        }

                        col.Item().PaddingVertical(10).Text($"Suma do zapłaty: {request.TotalPrice} zł")
                            .FontSize(14).Bold().FontColor(Colors.Red.Medium);
                    });
            });
        }).GeneratePdf(stream);

        return stream.ToArray();
    }
}

