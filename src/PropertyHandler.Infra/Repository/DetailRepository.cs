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
    public class DetailRepository : IDetailRepository
    {
        private readonly ISql _sql;

        public DetailRepository(ISql sql)
        {
            _sql = sql;
        }

        public async Task<IEnumerable<Detail>> GetAll()
        {
            var sqlQuery = "select * from Details";

            using var connection = new SqlConnection(_sql.GetConnectionString());
            var details = await connection.QueryAsync<Detail>(sqlQuery);

            return details;
        }

        public async Task<Detail> GetPerId(int id)
        {
            var sqlQuery = "select * from Details where Id = @id";

            using var connection = new SqlConnection(_sql.GetConnectionString());
            var details = await connection.QueryFirstOrDefaultAsync<Detail>(sqlQuery, new { id });

            return details;
        }

        public async Task<Detail> GetPerPropetyId(int propertyId)
        {
            var sqlQuery = "select * from Details where PropertyId = @propertyId";

            using var connection = new SqlConnection(_sql.GetConnectionString());
            var details = await connection.QueryFirstOrDefaultAsync<Detail>(sqlQuery, new { propertyId });

            return details;
        }

        public async Task<int> Insert(Detail entity)
        {
            var sqlQuery = @"insert into Details(RegisterDate,IsActive,PropertySize,BedRoomQuantity,CarVacancyQuantity,BathRoomQuantity,PropertyId)
                             Values(@RegisterDate,@IsActive,@PropertySize,@BedRoomQuantity,@CarVacancyQuantity,@BathRoomQuantity,@PropertyId)
                             SELECT @@IDENTITY AS [@@IDENTITY];";
            var parametros = new
            {
                RegisterDate = DateTime.Now,
                IsActive = true,
                entity.PropertySize,
                entity.BedRoomQuantity,
                entity.CarVacancyQuantity,
                entity.BathRoomQuantity,
                entity.PropertyId
            };

            using var connection = new SqlConnection(_sql.GetConnectionString());
            var idInserted = await connection.ExecuteScalarAsync<int>(sqlQuery, parametros);

            return idInserted;
        }
    }
}
