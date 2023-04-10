using System.Collections.Generic;

namespace Ecommerce.Domain.Entities
{
    public class Departamento : BaseEntity
    {
        public string Nome { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
    }
}
