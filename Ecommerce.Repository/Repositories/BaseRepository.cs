using Ecommerce.Domain.Config;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Ecommerce.Repository.Repositories
{
    public class BaseRepository<T>
    {
        protected IDbConnection Connection { get; set; }

        public BaseRepository(AppSettings appSettings)
        {
            Connection = new SqlConnection(appSettings.ConnectionStrings.ConnectionSql);
        }

        public virtual List<T> GetAll()
        {
            throw new System.NotImplementedException();
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
