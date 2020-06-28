using System;
using System.Data;

namespace PaymentMicroservices.Infra.Repositorios
{
    public class Repositorio
    {
        protected IDbConnection Connection { get; }


        public Repositorio(IDbConnection connection)
        {
            this.Connection = connection;
            this.Connection.Open();
        }

        protected virtual void EnsureConnectionIsOpen()
        {
            if (ShouldOpenConnection(this.Connection.State))
            {
                lock (this.Connection)
                {
                    if (ShouldOpenConnection(this.Connection.State))
                    {
                        this.Connection.Open();
                    }
                }
            }
        }

        private static bool ShouldOpenConnection(ConnectionState connectionState)
        {
            return connectionState == ConnectionState.Closed || connectionState == ConnectionState.Broken;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Connection != null)
                {
                    this.Connection.Dispose();
                }
            }
        }

    }
}
