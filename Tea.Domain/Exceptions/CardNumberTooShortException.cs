namespace Tea.Domain.Exceptions
{
    public class CardNumberTooShortException : Exception
    {
        public CardNumberTooShortException() : base("Card number is too short.") { }
    }
}
