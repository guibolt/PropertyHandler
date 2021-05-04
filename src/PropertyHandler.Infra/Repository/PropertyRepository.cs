using Dapper;
using PropertyHandler.Core.Entities;
using PropertyHandler.Core.Interfaces;
using PropertyHandler.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PropertyHandler.Infra.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ISql _sql;

        public PropertyRepository(ISql sql)
        {
            _sql = sql;
        }

        public async Task<IEnumerable<Property>> GetAll()
        {
            var sqlQuery = "select * from Properties";

            using var connection = new SqlConnection(_sql.GetConnectionString());
            var properties = await connection.QueryAsync<Property>(sqlQuery);
            return properties;
        }

        public async Task<bool> Insert(Property entity)
        {
            throw new NotImplementedException();
        }
        public async Task<Property> GetPerId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Property> GetWithFilters()
        {
            throw new NotImplementedException();
        }
    }
}
