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
    public class AddressRepository : IAddressRepository
    {
        private readonly ISql _sql;

        public AddressRepository(ISql sql)
        {
            _sql = sql;
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            var sqlQuery = "select * from Address";

            using var connection = new SqlConnection(_sql.GetConnectionString());
            var addresses = await connection.QueryAsync<Address>(sqlQuery);

            return addresses;
        }

        public async Task<Address> GetPerId(int id)
        {
            var sqlQuery = "select * from Address where Id = @id";

            using var connection = new SqlConnection(_sql.GetConnectionString());
            var address = await connection.QueryFirstOrDefaultAsync<Address>(sqlQuery, new { id });

            return address;
        }

        public async Task<Address> GetPerPropetyId(int propertyId)
        {
            var sqlQuery = "select * from Address where PropertyId = @propertyId";

            using var connection = new SqlConnection(_sql.GetConnectionString());
            var address = await connection.QueryFirstOrDefaultAsync<Address>(sqlQuery, new { propertyId });

            return address;
        }

        public async Task<int> Insert(Address entity)
        {
            var parametros = new
            {
                RegisterDate = DateTime.Now,
                Active = true,
                entity.Street,
                entity.LocationNumber,
                entity.Cep,
                entity.District,
                entity.City,
                entity.State,
                entity.PropertyId
            };

            var sqlQuery = @"insert into Address (RegisterDate,Active,Street,LocationNumber,Cep,District,State,City,PropertyId)
                             Values(@RegisterDate,@Active,@Street,@LocationNumber,@Cep,@District,@State,@City,@PropertyId)
                             SELECT @@IDENTITY AS [@@IDENTITY];";

            using var connection = new SqlConnection(_sql.GetConnectionString());
            var idInserted = await connection.ExecuteScalarAsync<int>(sqlQuery, parametros);

            return idInserted;
        }
    }
}
