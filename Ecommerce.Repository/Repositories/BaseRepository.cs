using Dapper;
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

        public BaseRepository(IOptions<AppSettings> appSettings)
        {
            Connection = new SqlConnection(appSettings.Value.ConnectionStrings.ConnectionSql);            
        }

        public virtual List<T> GetAll()
        {
            return Connection.Query<T>(GetAllSql<T>()).ToList();
        }

        public virtual T Get(long id)
        {
            return Connection.QuerySingleOrDefault<T>(GetByIdSql<T>(), new { Id = id });
        }
               
        public virtual void Insert(T entity)
        {            
            (entity as BaseEntity).Id = Connection.Query<long>(InsertSql<T>(), entity).Single();
        }

        public virtual void Update(T entity)
        {               
            Connection.Execute(UpdateSql<T>(), entity);
        }

        public virtual void Delete(long id)
        {
            Connection.Execute(DeleteSql<T>(), new { Id = id });
        }

        private List<string> GetProperties<T2>()
        {
            var propertiesEntity = typeof(T2).GetProperties();
            var properties = new List<string>();
            foreach (var propertyEntity in propertiesEntity)
            {
                if (!propertyEntity.PropertyType.IsPrimitive() || propertyEntity.Name == nameof(BaseEntity.Id))
                    continue;

                properties.Add(propertyEntity.Name);
            }
            return properties;
        }

        protected string GetAllSql<T2>()
        {
            return string.Format("SELECT * FROM {0};", typeof(T2).Name);
        }

        protected string GetByIdSql<T2>()
        {
            return string.Format("SELECT * FROM {0} WHERE Id = @Id;", typeof(T2).Name);
        }

        protected string InsertSql<T2>()
        {
            var properties = GetProperties<T2>();
            return string.Format("INSERT INTO {0} ({1}) VALUES ({2});SELECT CAST (SCOPE_IDENTITY() AS BIGINT);", typeof(T2).Name, string.Join(",", properties), string.Join(",", properties.Select(x => $"@{x}")));
        }

        protected string UpdateSql<T2>()
        {
            var properties = GetProperties<T2>();
            return string.Format("UPDATE {0} SET {1} WHERE Id = @Id;", typeof(T2).Name, string.Join(",", properties.Select(x => $"{x} = @{x}")));
        }

        protected string DeleteSql<T2>()
        {            
            return string.Format("DELETE FROM {0} WHERE Id = @Id;", typeof(T2).Name);
        }
    }
}
