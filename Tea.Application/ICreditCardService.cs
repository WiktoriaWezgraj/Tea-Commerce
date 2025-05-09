using Tea.Domain.Enums;

namespace Tea.Application
{
    public interface ICreditCardService
    {
        void ValidateCard(string cardNumber);

        CreditCardProvider? GetCardProvider(string cardNumber);
    }
}
