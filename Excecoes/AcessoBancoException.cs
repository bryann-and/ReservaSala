namespace System
{
    public class AcessoBancoException : Exception
    {
        public AcessoBancoException() { }

        public AcessoBancoException(string message) : base(message) { }

        public AcessoBancoException(string message, Exception inner) : base(message, inner) { }
    }
}
