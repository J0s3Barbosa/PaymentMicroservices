using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentMicroservices.Application.Transacao
{
    public class RetornoTransacao
    {
        public string Mensagem { get; set; }


        public RetornoTransacao(string mensagem) 
        {
            this.Mensagem = mensagem;
        }
    }
}
