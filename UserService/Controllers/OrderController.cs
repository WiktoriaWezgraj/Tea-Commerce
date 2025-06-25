using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Application;
using User.Domain;

namespace UserService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public OrderController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmOrder([FromBody] InvoiceRequest request)
    {
        await _invoiceService.GenerateAndSendInvoiceAsync(request);
        return Ok(new { Message = "Invoice sent to e-mail." });
    }
}

