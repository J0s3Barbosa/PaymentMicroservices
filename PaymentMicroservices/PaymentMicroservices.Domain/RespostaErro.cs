using FluentValidation.Results;
using System.Collections.Generic;

namespace PaymentMicroservices.Domain
{
    public class RespostaErro
    {
        public List<string> Mensagens { get; set; } = new List<string>();
        public int StatusCode { get; set; }

        public RespostaErro()
        {
            Mensagens = new List<string>();
            this.StatusCode = 400;
        }

        public RespostaErro(string mensagem, int statusCode = 400)
        {
            this.StatusCode = statusCode;
            this.Mensagens.Add(mensagem);
        }

        public RespostaErro(List<ValidationFailure> mensagens, int statusCode = 400)
        {
            this.StatusCode = statusCode;
            foreach (var item in mensagens)
            {
                Mensagens.Add(item.ErrorMessage);
            }
        }
    }
}
