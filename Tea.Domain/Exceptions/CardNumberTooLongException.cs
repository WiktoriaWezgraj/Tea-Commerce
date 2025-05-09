namespace Tea.Domain.Exceptions
{
    public class CardNumberTooLongException : Exception
    {
        public CardNumberTooLongException() : base("Card number is too long.") { }
    }
}
