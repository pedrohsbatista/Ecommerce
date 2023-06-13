using Ecommerce.Domain.Entities;
using System;

namespace Ecommerce.Domain.Storeds
{
    public class StoredUsuario : BaseEntity
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Sexo { get; set; }

        public string Rg { get; set; }

        public string Cpf { get; set; }

        public string NomeMae { get; set; }

        public string SituacaoCadastro { get; set; }

        public DateTimeOffset DataCadastro { get; set; }
    }
}
