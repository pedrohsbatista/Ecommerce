﻿using Dapper;
using Ecommerce.Domain.Config;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Extensions;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Ecommerce.Repository.Repositories
{
    public class BaseRepository<T>
    {
        protected IDbConnection Connection { get; set; }        

        private string _entityName => typeof(T).Name.ToLower();

        public BaseRepository(IOptions<AppSettings> appSettings)
        {
            Connection = new SqlConnection(appSettings.Value.ConnectionStrings.ConnectionSql);            
        }

        public virtual List<T> GetAll()
        {
            return Connection.Query<T>($"select * from {_entityName}").ToList();
        }

        public virtual T Get(long id)
        {
            return Connection.QuerySingleOrDefault<T>($"select * from {_entityName} where id = @id", new { Id = id });
        }
               
        public virtual void Insert(T entity)
        {
            var properties = GetProperties();

            var sql = $"INSERT INTO {_entityName} ({string.Join(",", properties)}) VALUES ({string.Join(",", properties.Select(x => $"@{x}"))});SELECT CAST (SCOPE_IDENTITY() AS BIGINT);";
          
            (entity as BaseEntity).Id = Connection.Query<long>(sql, entity).Single();
        }

        public virtual void Update(T entity)
        {
            var properties = GetProperties();
            var sql = $"UPDATE {_entityName} SET {string.Join(",", properties.Select(x => $"{x} = @{x}"))} WHERE Id = @Id";
            Connection.Execute(sql, entity);
        }

        public virtual void Delete(long id)
        {
            var sql = $"DELETE FROM {_entityName} WHERE Id = @Id";
            Connection.Execute(sql, new { Id = id });
        }

        private List<string> GetProperties()
        {
            var propertiesEntity = typeof(T).GetProperties();
            var properties = new List<string>();
            foreach (var propertyEntity in propertiesEntity)
            {
                if (!propertyEntity.PropertyType.IsPrimitive())
                    continue;

                properties.Add(propertyEntity.Name);
            }
            return properties;
        }
    }
}
