using EFCore.Obejct;
using EFCore.Obejct.EFCore.ValuesObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Domain
{
	public class Pedido
	{
		public int Id { get; set; }
		public int ClienteId { get; set; }
		public Cliente Cliente { get; set; }
		public DateTime IniciadoEm { get; set; }
		public DateTime FizalizadoEm { get; set; }
		public TipoFrete TipoFrete { get; set; }
		public StatusPedido Status { get; set; }
		public string Observacao { get; set; }
		public ICollection<PedidoItem> Intes { get; set; }
        public DateTime FinalizadoEM { get; internal set; }
        public string Nome { get; internal set; }
    }
}