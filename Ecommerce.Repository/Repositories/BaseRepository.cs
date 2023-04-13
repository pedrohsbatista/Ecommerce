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

        public BaseRepository(IOptions<AppSettings> appSettings)
        {
            Connection = new SqlConnection(appSettings.Value.ConnectionStrings.ConnectionSql);            
        }

        public virtual List<T> GetAll()
        {
            return Connection.Query<T>($"select * from {typeof(T).Name.ToLower()}".ToLower()).ToList();
        }

        public virtual T Get(long id)
        {
            throw new System.NotImplementedException();
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
