namespace System
{
    public class DadosInvalidosException : Exception
    {
        public DadosInvalidosException() { }

        public DadosInvalidosException(string message) : base(message) { }

        public DadosInvalidosException(string message, Exception inner) : base(message, inner) { }
    }
}
