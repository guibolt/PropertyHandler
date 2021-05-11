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
    public class ImageRepository : IImageRepository
    {
        private readonly ISql _sql;

        public ImageRepository(ISql sql)
        {
            _sql = sql;
        }

        public async Task<IEnumerable<PropertyImage>> GetAll()
        {
            var sqlQuery = "select * from Images";

            using var connection = new SqlConnection(_sql.GetConnectionString());
            var images = await connection.QueryAsync<PropertyImage>(sqlQuery);

            return images;
        }

        public async Task<PropertyImage> GetPerId(int id)
        {

            var sqlQuery = "select * from Images where Id = @id";

            using var connection = new SqlConnection(_sql.GetConnectionString());
            var image = await connection.QueryFirstOrDefaultAsync<PropertyImage>(sqlQuery, new { id });

            return image;
        }

        public async Task<IEnumerable<PropertyImage>> GetAllPerPropetyId(int propertyId)
        {
            var sqlQuery = "select * from Images where PropertyId = @propertyId";

            using var connection = new SqlConnection(_sql.GetConnectionString());
            var images = await connection.QueryAsync<PropertyImage>(sqlQuery, new { propertyId });

            return images;
        }

        public async Task<int> Insert(PropertyImage entity)
        {
            var sqlQuery = @"insert into Images (RegisterDate,IsActive,Name,FileType,FileId,PropertyId)
                             VALUES(@RegisterDate,@IsActive,@Name,@FileType,@FileId,@PropertyId)";

            var parametros = new
            {
                RegisterDate = DateTime.Now,
                IsActive = true,
                entity.Name,
                entity.FileType,
                entity.FileId,
                entity.PropertyId
            };

            using var connection = new SqlConnection(_sql.GetConnectionString());
            var affectedRows = await connection.ExecuteAsync(sqlQuery, parametros);

            return affectedRows;
        }

        public async Task<PropertyImage> GetPerFileId(Guid fileId)
        {
            var sqlQuery = "select FileType,Name from Images where FileId = @fileId";

            using var connection = new SqlConnection(_sql.GetConnectionString());
            var image = await connection.QueryFirstOrDefaultAsync<PropertyImage>(sqlQuery, new { fileId });

            return image;
        }
    }
}
