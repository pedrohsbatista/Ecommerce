namespace Ecommerce.Domain.Entities
{
    public class UsuarioDepartamento : BaseEntity
    {
        public long UsuarioId { get; set; }

        public long DepartamentoId { get; set; }

        public Usuario Usuario { get; set; }

        public Departamento Departamento { get; set; }
    }
}
