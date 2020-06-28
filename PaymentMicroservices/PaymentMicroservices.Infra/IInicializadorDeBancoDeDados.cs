using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentMicroservices.Infra
{
    public interface IInicializadorDeBancoDeDados
    {
        string StringConexao { get; }
        void Inicializar();

    }
}
