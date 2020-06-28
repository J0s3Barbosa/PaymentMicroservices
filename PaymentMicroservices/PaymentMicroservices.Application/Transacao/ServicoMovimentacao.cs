using PaymentMicroservices.Application.Repositorios.Interfaces;
using PaymentMicroservices.Domain.Modelo;

namespace PaymentMicroservices.Application.Transacao
{
    public class ServicoMovimentacao : IServicoMovimentacao
    {
        private readonly IContaCorrenteRepositorio _contaCorrenteRepositorio;

        public ServicoMovimentacao(IContaCorrenteRepositorio contaCorrenteRepositorio) 
        {
            _contaCorrenteRepositorio = contaCorrenteRepositorio;
        }

        public void Creditar(ContaCorrente contaCorrente, decimal valor)
        {
            contaCorrente.Creditar(valor);
            _contaCorrenteRepositorio.AtualizarSaldo(contaCorrente);
        }

        public void Debitar(ContaCorrente contaCorrente, decimal valor)
        {
            contaCorrente.Debitar(valor);
            _contaCorrenteRepositorio.AtualizarSaldo(contaCorrente);
        }
    }
}
