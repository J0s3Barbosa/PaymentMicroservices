using Xunit;
using Microsoft.Extensions.Configuration;

namespace PaymentMicroservices.IntegrationTest.Infra
{
    public class InicializadorDeBancoDeDadosTeste
    {

        protected IConfiguration Configuration { get; private set; }

        public InicializadorDeBancoDeDadosTeste()
        {
            this.Configuration = InitConfiguration(); 

        }
        public static IConfiguration InitConfiguration()
        {

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            return config;
        }



        [Theory(DisplayName = "Cenario - verifica connectionString not null")]
        [Trait("Category", "Success")]
        [InlineData("MySqlConnection")]
        public void Deve_Retornar_Sucesso_Ao_Retornar_Valor(string connectionName)
        {
            var stringConexao = this.Configuration.GetConnectionString(connectionName);

            Assert.NotNull(stringConexao);

        }


    }

}
