namespace Tea.Domain.Exceptions
{
    public class CardNumberInvalidException : Exception
    {
        public CardNumberInvalidException() : base("Card number is invalid.") { }
    }
}
