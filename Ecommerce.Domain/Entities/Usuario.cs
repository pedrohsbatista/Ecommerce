using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Sexo { get; set; }

        public string Rg { get; set; }

        public string Cpf { get; set; }

        public string NomeMae { get; set; }

        public string SituacaoCadastro { get; set; }

        public DateTimeOffset DataCadastro { get; set; }

        public Contato Contato { get; set; }

        public ICollection<EnderecoEntrega> Enderecos { get; set; }

        public ICollection<Departamento> Departamentos { get; set; }
    }
}
