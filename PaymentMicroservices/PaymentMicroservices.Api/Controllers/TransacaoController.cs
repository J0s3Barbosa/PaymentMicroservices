using Microsoft.AspNetCore.Mvc;
using PaymentMicroservices.Api.Extensions;
using PaymentMicroservices.Application.Transacao;
using PaymentMicroservices.Application.Transacao.Requisicao;
using PaymentMicroservices.Domain;
using System.Web.Http.Description;

namespace PaymentMicroservices.Api.Controllers
{
    [ApiVersion("1")]
    [SwaggerGroup("Transaction Api")]
    //[Authorize]
    [ApiController, Route("api/v{version:apiVersion}/[controller]"), Produces("application/json")]
    public class TransacaoController : ControllerBase
    {
        private readonly IServicoTransacao _servico;

        public TransacaoController(IServicoTransacao servico)
        {
            _servico = servico;
        }


        [HttpPost]
        [ResponseType(typeof(RequisicaoDeTransacao))]
        [ProducesResponseType(200, Type = typeof(RetornoTransacao))]
        [ProducesResponseType(400, Type = typeof(RespostaErroPadrao))]
        [ProducesResponseType(401, Type = typeof(RespostaErroPadrao))]
        [ProducesResponseType(403, Type = typeof(RespostaErroPadrao))]
        public ActionResult Post(RequisicaoDeTransacao requisicao)
        {
            var resultado = _servico.Efetuar(requisicao);

            if (!resultado.Sucesso)
            {
                return StatusCode(resultado.Erro.StatusCode, new RespostaErroPadrao(resultado.Erro));
            }

            return Ok(resultado.Objeto);
        }




    }
}
