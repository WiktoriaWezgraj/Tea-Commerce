using Tea.Application;
using Tea.Domain.Enums;
using Tea.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tea_Commerce.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CreditCardController : ControllerBase
{
    private readonly ICreditCardService _creditCardService;

    public CreditCardController(ICreditCardService creditCardService)
    {
        _creditCardService = creditCardService;
    }

    [HttpGet("{cardNumber}")]
    public IActionResult ValidateCard(string cardNumber)
    {
        try
        {
            _creditCardService.ValidateCard(cardNumber);
            _creditCardService.GetCardType(cardNumber);
            return Ok(new {cardProvider = _creditCardService.GetCardType(cardNumber)});
        }
        catch (CardNumberTooLongException)
        {
            return StatusCode(414, "Card number is too long.");
        }
        catch (CardNumberTooShortException)
        {
            return StatusCode(400, "Card number too short.");
        }
        catch (CardNumberInvalidException)
        {
            return StatusCode(406, "Card number invalid.");
        }
        catch (NotSupportedException)
        {
            return StatusCode(406, "Card provider not supported.");
        }

    }
}
