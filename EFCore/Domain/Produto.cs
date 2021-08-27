using EFCore.Obejct.EFCore.ValuesObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Domain
{
	public class Produto
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Telefone { get; set; }
		public string CEP { get; set; }
		public string Estado { get; set; }
		public string cidade { get; set; }
        public string CodigoBarras { get; internal set; }
        public string Descricao { get; internal set; }
        public string Valor { get; internal set; }
        public bool Ativo { get; internal set; }
        public TipoProduto TipoProduto { get; internal set; }
    }
}