

using PaymentMicroservices.Application.Transacao.Requisicao;
using PaymentMicroservices.Domain;

namespace PaymentMicroservices.Application.Transacao
{
    public interface IServicoTransacao
    {
        RespostaPadrao<RetornoTransacao> Efetuar(RequisicaoDeTransacao transacao);
    }
}
