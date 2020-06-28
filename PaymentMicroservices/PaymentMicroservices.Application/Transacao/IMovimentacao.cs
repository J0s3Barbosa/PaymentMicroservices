using PaymentMicroservices.Domain.Modelo;

namespace PaymentMicroservices.Application.Transacao
{
    public interface IServicoMovimentacao
    {
        void Debitar(ContaCorrente contaCorrente, decimal valor);
        void Creditar(ContaCorrente contaCorrente, decimal valor);
    }
}
