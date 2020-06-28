using PaymentMicroservices.Domain.Modelo;

namespace PaymentMicroservices.Application.Repositorios.Interfaces
{
    public interface IContaCorrenteRepositorio
    {
        ContaCorrente Obter(ContaCorrente contaCorrente);
        void AtualizarSaldo(ContaCorrente contaCorrente);

    }
}
