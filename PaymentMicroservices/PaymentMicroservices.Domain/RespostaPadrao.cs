using FluentValidation.Results;
using PaymentMicroservices.Domain.Excecoes;
using System;
using System.Linq;

namespace PaymentMicroservices.Domain
{
    public class RespostaPadrao<T> where T : class
    {
        public bool Sucesso { get; set; }
        public RespostaErro Erro { get; set; }
        public T Objeto { get; set; }

        public void DefinirSucesso(bool sucesso)
        {
            Sucesso = sucesso;
        }

        public void ConfigurarResposta(bool sucesso, T data)
        {
            this.DefinirSucesso(sucesso);
            this.Objeto = data;
        }

        public void ConfigurarResposta(ValidationResult validacao)
        {
            this.DefinirSucesso(false);
            this.Erro = new RespostaErro(validacao.Errors.ToList());
        }

        public void ConfigurarResposta(Exception ex)
        {
            this.DefinirSucesso(false);
            this.Erro = new RespostaErro(ex.Message, 500);
        }

        public void ConfigurarResposta(ExcecaoDeDominio ex)
        {
            this.DefinirSucesso(false);
            this.Erro = new RespostaErro(ex.Message, 400);
        }

        public void ConfigurarResposta(string message)
        {
            this.DefinirSucesso(false);
            this.Erro = new RespostaErro(message);
        }

        public void ConfigurarResposta(string message, int statusCode)
        {
            this.DefinirSucesso(false);
            this.Erro = new RespostaErro(message, statusCode);
        }
    }
}
