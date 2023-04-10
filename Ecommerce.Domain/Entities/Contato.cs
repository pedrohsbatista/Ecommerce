namespace Ecommerce.Domain.Entities
{
    public class Contato : BaseEntity
    {
        public long UsuarioId { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        public Usuario Usuario { get; set; }
    }
}
