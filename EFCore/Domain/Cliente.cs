using EFCore.Obejct.EFCore.ValuesObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Domain
{

	public class Cliente
	{
		public int Id { get; set; }

		public string CodigoBarras { get; set; }
		public string Descricao { get; set; }

		public decimal Valor { get; set; }

		public TipoProduto TipoProduto { get; set; }
		public bool Ativo { get; set; }
        public string Nome { get; internal set; }
        public string Telefone { get; internal set; }
        public string CEP { get; internal set; }
        public string Cidade { get; internal set; }
        public string Estado { get; internal set; }
    }
}