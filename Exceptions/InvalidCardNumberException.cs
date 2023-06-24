namespace BlackjackServer.Exceptions
{
    public class InvalidCardNumberException : Exception
    {
        public InvalidCardNumberException(short cardNumber)
            : base($"Invalid card number: {cardNumber}. Card numbers should be between 1 and 13.")
        {
        }
    }

   

}
