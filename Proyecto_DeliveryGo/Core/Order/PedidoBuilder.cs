using System;
using System.Collections.Generic;
using Proyecto_DeliveryGo.Core.Payment;

namespace Proyecto_DeliveryGo.Core.Order
{
    public class PedidoBuilder: IPedidoBuilder
    {
        private Pedido _pedido;

        public PedidoBuilder()
        {
            _pedido = new Pedido();
        }

        /*public IPedidoBuilder ConItems(Item Items)
        {
            _pedido.Items = Items;
            return this;
        }*/

        public IPedidoBuilder ConDireccion(string Direccion)
        {
            _pedido.Direccion = Direccion;
            return this;
        }

        public IPedidoBuilder ConMonto(decimal Monto)
        {
            _pedido.Monto = Monto;
            return this;
        }

        public Pedido Build()
        {
            if (string.IsNullOrEmpty(_pedido.Direccion))
                throw new InvalidOperationException("Debes ingresar la direccion");

            var resultado = _pedido;

            _pedido = new Pedido();

            return resultado;
        }
    }
}