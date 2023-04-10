﻿namespace Ecommerce.Domain.Entities
{
    public class EnderecoEntrega : BaseEntity
    {
        public long UsuarioId { get; set; }

        public string NomeEndereco { get; set; }

        public string Cep { get; set; }

        public string Estado { get; set; }

        public string Cidade { get; set; }        

        public string Bairro { get; set; }

        public string Endereco { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public Usuario Usuario { get; set; }
    }
}
