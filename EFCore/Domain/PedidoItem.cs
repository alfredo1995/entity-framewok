using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Domain
{
	public class PedidoItem
	{
		public int Id { get; set; }
		public int PedidoId { get; set; }
		public Pedido Pedido { get; set; }
		public Produto Produto { get; set; }
		public int Quatidade { get; set; }
		public decimal Desconto { get; set; }
        public string Valor { get; internal set; }
        public string Quantidade { get; internal set; }
    }
}