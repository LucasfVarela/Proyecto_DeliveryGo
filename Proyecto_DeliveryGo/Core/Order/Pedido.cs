using Proyecto_DeliveryGo.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DeliveryGo.Core.Order
{
    public class Pedido
    {
       public int Id { get; set; }
       public List<Item> Items { get; set; } = new ();
       public string Direccion { get; set; }
       public string TipoPago { get; set; }
       public EstadoPedido Estado { get; set; }
       public decimal Monto { get; set; }
    }



    public class EstadoPedido
    {
        public static EstadoPedido Recibido { get; set; }
        public static EstadoPedido Preparando { get; set; }
        public static EstadoPedido Enviado { get; set; }
        public static EstadoPedido Entregado { get; set; }
    }
}
