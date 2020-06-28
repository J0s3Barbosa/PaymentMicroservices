using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentMicroservices.Domain.Excecoes
{
    public class ExcecaoDeDominio : Exception
    {
        public ExcecaoDeDominio() { }
        public ExcecaoDeDominio(string message) : base(message) { }
        public ExcecaoDeDominio(string message, Exception inner) : base(message, inner) { }

    }
}
