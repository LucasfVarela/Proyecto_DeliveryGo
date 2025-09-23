using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DeliveryGo.Core.Order
{
    public interface IPedidoBuilder
    {
        IPedidoBuilder ConMonto(decimal Monto);
        //IPedidoBuilder ConItems(Item Items);
        IPedidoBuilder ConDireccion(string Direccion);
        Pedido Build();
    }
}
