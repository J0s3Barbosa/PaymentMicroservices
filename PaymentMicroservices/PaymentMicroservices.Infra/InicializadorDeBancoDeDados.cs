using Microsoft.Extensions.Configuration;
using System.Collections.Generic;


namespace PaymentMicroservices.Infra
{

    public class InicializadorDeBancoDeDados : IInicializadorDeBancoDeDados
    {
        private string _stringConexao;

        public virtual string StringConexao
        {
            get { return this._stringConexao; }
            protected set { this._stringConexao = value; }
        }

        protected IConfiguration Configuration { get; private set; }

        public virtual void Inicializar()
        {

        }

        public InicializadorDeBancoDeDados(IConfiguration configuration, string nomeConexao)
        {
            this.Configuration = configuration;
            this.StringConexaoDoBancoDeDados(nomeConexao);
        }

        private void StringConexaoDoBancoDeDados(string connectionName)
        {
            var stringConexao = this.Configuration.GetConnectionString(connectionName);
            if (string.IsNullOrWhiteSpace(stringConexao))
            {
                throw new KeyNotFoundException($"Connectionstring ${connectionName} not found.");
            }

            this.StringConexao = stringConexao;
        }
    }
}
