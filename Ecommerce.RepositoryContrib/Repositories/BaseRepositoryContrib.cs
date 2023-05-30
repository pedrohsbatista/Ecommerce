using Dapper.Contrib.Extensions;
using Ecommerce.Domain.Config;
using Ecommerce.Domain.Entities;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Ecommerce.RepositoryContrib.Repositories
{
    public class BaseRepositoryContrib<T> where T : class
    {
        protected IDbConnection Connection { get; set; }

        public BaseRepositoryContrib(IOptions<AppSettings> appSettings)
        {
            Connection = new SqlConnection(appSettings.Value.ConnectionStrings.ConnectionSql);
            SqlMapperExtensions.TableNameMapper = (type) =>
            {
                return type.Name;
            };
        }

        public virtual List<T> GetAll()
        {
            return Connection.GetAll<T>().ToList();
        }

        public virtual T Get(long id)
        {
            return Connection.Get<T>(id);
        }

        public virtual void Insert(T entity)
        {
            (entity as BaseEntity).Id = Connection.Insert(entity);
        }

        public virtual void Update(T entity)
        {
            Connection.Update(entity);
        }

        public virtual void Delete(long id)
        {
            var entity = Get(id);
            Connection.Delete(entity);
        }  
    }
}
