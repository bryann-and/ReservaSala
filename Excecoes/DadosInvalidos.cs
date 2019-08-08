using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace System
{
    public class DadosInvalidosException : Exception
    {
        public string Erro { get; private set; }

        public DadosInvalidosException() { }

        /// <summary>
        /// Inicializa o objeto com uma mensagem de erro de todos os valores invalidos
        /// </summary>
        /// <param name="erros">uma lista dos erros</param>
        public DadosInvalidosException(List<ValidationResult> erros)
        {
            Erro = "";

            foreach (ValidationResult item in erros)
            {
                foreach (string campo in item.MemberNames)
                {
                    Erro += campo + ": ";
                }

                Erro += item.ErrorMessage;
            }
        }

        public DadosInvalidosException(string message) : base(message) { }

        public DadosInvalidosException(string message, Exception inner) : base(message, inner) { }
    }
}
