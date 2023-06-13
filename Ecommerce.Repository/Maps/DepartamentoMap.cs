using Dapper.FluentMap.Mapping;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Repository.Maps
{
    public class DepartamentoMap : EntityMap<Departamento>
    {
        public DepartamentoMap()
        {
            Map(x => x.Descricao).ToColumn("Nome");
        }
    }
}