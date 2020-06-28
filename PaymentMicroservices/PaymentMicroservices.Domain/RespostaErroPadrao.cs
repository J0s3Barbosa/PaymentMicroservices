using System.Collections.Generic;

namespace PaymentMicroservices.Domain
{
    public class RespostaErroPadrao
    {
        public List<string> Erros { get; set; }

        public RespostaErroPadrao(RespostaErro respostaErro) 
        {
            this.Erros = respostaErro.Mensagens;
        }
    }
}
