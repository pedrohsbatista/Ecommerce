using Dapper;
using Ecommerce.Domain.Config;
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
            throw new System.NotImplementedException();
        }

        public virtual void Update(T entity)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Delete(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
