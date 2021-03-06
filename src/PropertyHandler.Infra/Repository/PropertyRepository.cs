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

        public async Task<int> Insert(Property entity)
        {

            var parametros = new
            {
                RegisterDate = DateTime.Now,
                Active = true,
                entity.Code,
                entity.Description,
                entity.Title,
                entity.RentPrice,
                entity.SalePrice,
                entity.TaxPrice,
                entity.CondominiumPrice,
                entity.OwnerName,
                entity.IsSpotlight,
                entity.Status,
                entity.Type,
                entity.SpecificType
            };

            var sqlQuery = @"INSERT INTO Properties (RegisterDate,Active,Code,Description, Title,RentPrice,
                SalePrice,TaxPrice,CondominiumPrice,OwnerName,IsSpotlight,Status,Type,SpecificType)
                VALUES(@RegisterDate,@Active,@Code,@Description,@Title,@RentPrice,
                @SalePrice,@TaxPrice,@CondominiumPrice,@OwnerName,@IsSpotlight,@Status,@Type,@SpecificType) 
                SELECT @@IDENTITY AS [@@IDENTITY];";

            using var connection = new SqlConnection(_sql.GetConnectionString());
            var idInserted = await connection.ExecuteScalarAsync<int>(sqlQuery, parametros);

            return idInserted;
        }
        public async Task<Property> GetPerId(int id)
        {
            var sqlQuery = "select * from Properties where Id = @id";

            using var connection = new SqlConnection(_sql.GetConnectionString());
            var property = await connection.QueryFirstOrDefaultAsync<Property>(sqlQuery, new { id });

            return property;
        }
        public async Task<bool> UpdateAddressAndDetail(int addressId, int detailId, int propertyId)
        {
            var sqlQuery = "update  Properties set AdressId = @addressId, DetailsId = @detailId where Id = @propertyId";

            var param = new
            {
                addressId,
                detailId,
                propertyId
            };

            using var connection = new SqlConnection(_sql.GetConnectionString());
            var updateReturn = await connection.ExecuteAsync(sqlQuery, param);

            return updateReturn == 1;
        }
        public async Task<Property> GetWithFilters()
        {
            throw new NotImplementedException();
        }
    }
}
