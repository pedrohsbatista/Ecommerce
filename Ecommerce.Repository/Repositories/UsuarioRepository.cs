using Ecommerce.Domain.Config;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepository;
using Microsoft.Extensions.Options;

namespace Ecommerce.Repository.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {        
        public UsuarioRepository(IOptions<AppSettings> appSettings) : base(appSettings)
        {            
        }
    }
}
