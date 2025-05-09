using Tea.Application;
using Tea.Domain.Enums;
using Tea.Domain.Exceptions;

namespace Tea.Domain.Tests;

public class CreditCardProviderTests
{
    [Fact]
    public void ValidateCard_ShouldThrowCardNumberTooShortException()
    {
        var credit = new CreditCardService();
        string cardNumber = "123456789015";
        Assert.Throws<CardNumberTooShortException>(() => credit.ValidateCard(cardNumber));
    }

    [Fact]
    public void ValidateCard_ShouldThrowCardNumberTooLongException()
    {
        var credit = new CreditCardService();
        string cardNumber = "12345678901234567894";
        Assert.Throws<CardNumberTooLongException>(() => credit.ValidateCard(cardNumber));
    }

    [Fact]
    public void ValidateCard_ShouldThrowCardNumberInvalidException()
    {
        var credit = new CreditCardService();
        string cardNumber = "alamakotaalamako";
        Assert.Throws<CardNumberInvalidException>(() => credit.ValidateCard(cardNumber));

    }

    [Fact]
    public void GetCardType_ShouldReturnVisa()
    {
        var credit = new CreditCardService();
        string cardNumber = "4111111111111111";
        Assert.Equal(CreditCardProvider.Visa, credit.GetCardProvider(cardNumber));
    }
}