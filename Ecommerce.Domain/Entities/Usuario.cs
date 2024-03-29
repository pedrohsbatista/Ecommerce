﻿using System;
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

        public ICollection<EnderecoEntrega> Enderecos { get; set; } = new List<EnderecoEntrega>();

        public ICollection<UsuarioDepartamento> Departamentos { get; set; } = new List<UsuarioDepartamento>();

        public void AddEndereco(EnderecoEntrega enderecoEntrega)
        {
            if (enderecoEntrega == null)
                return;

            Enderecos.Add(enderecoEntrega);
        }

        public void AddDepartamento(UsuarioDepartamento usuarioDepartamento, Departamento departamento)
        {
            if (usuarioDepartamento == null)
                return;

            usuarioDepartamento.Departamento = departamento;

            Departamentos.Add(usuarioDepartamento);
        }
    }
}
