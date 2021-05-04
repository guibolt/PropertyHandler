using PropertyHandler.Core.Interfaces;

namespace PropertyHandler.Core.Sql
{
    public class Sql : ISql
    {
        private readonly string _connectionString;

        public Sql(string connectionString) => _connectionString = connectionString;

        public string GetConnectionString() => _connectionString;
   
    }
}
