using Tea.Domain.Enums;

namespace Tea.Application
{
    public interface ICreditCardService
    {
        void ValidateCard(string cardNumber);

        public string GetCardType(string cardNumber);    
    }
}
