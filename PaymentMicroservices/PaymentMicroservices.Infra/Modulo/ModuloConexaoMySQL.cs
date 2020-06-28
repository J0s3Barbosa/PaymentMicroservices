using Autofac;
using MySql.Data.MySqlClient;
using PaymentMicroservices.Infra.Repositorios;
using System.Data;

namespace PaymentMicroservices.Infra.Modulo
{
    public class ModuloConexaoMySQL : Module
    {
        public const string CONNECTION_NAME = "MySqlConnection";

        protected override void Load(ContainerBuilder builder)
        {
            var name = this.GetType().Name;
            builder.RegisterType<InicializadorDeBancoDeDados>()
                   .As<InicializadorDeBancoDeDados>()
                   .WithParameter("nomeConexao", CONNECTION_NAME)
                   .SingleInstance()
                   .OnActivated(h => h.Instance.Inicializar());

            builder.RegisterType<MySqlConnection>()
                   .AsSelf()
                   .WithParameter((pi, ctx) => pi.ParameterType == typeof(string),
                                  (pi, ctx) => ctx.Resolve<InicializadorDeBancoDeDados>()
                                                  .StringConexao)
                   .Named<IDbConnection>(name);

            builder.RegisterType<Repositorio>()
                   .AsSelf();
        }
    }
}
