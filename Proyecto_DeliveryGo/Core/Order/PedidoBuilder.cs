using System;
using System.Collections.Generic;
using Proyecto_DeliveryGo.Core.Command;

namespace Proyecto_DeliveryGo.Core.Order
{
    public class PedidoBuilder : IPedidoBuilder
    {
        private Pedido _pedido;
        private static int _proximoId = 1;

        public PedidoBuilder()
        {
            _pedido = new Pedido();
            _pedido.Items = new List<Item>();
        }


        public IPedidoBuilder AddItems(IEnumerable<Item> items)
        {
            foreach (var i in items)
            {
                _pedido.Items.Add(i);
            }
            return this;
        }



        public IPedidoBuilder ConDireccion(string direccion)
        {
            _pedido.Direccion = direccion;
            return this;
        }

        public IPedidoBuilder ConMetodoPago(string tipoPago)
        {
            _pedido.TipoPago = tipoPago;
            return this;
        }

        public IPedidoBuilder ConMonto(decimal monto)
        {
            _pedido.Monto = monto;
            return this;
        }

        public Pedido Build()
        {
            if (_pedido.Items.Count == 0)
                throw new InvalidOperationException("El pedido debe tener items");
                
            if (string.IsNullOrEmpty(_pedido.Direccion))
                throw new InvalidOperationException("Debes ingresar la direccion");

            _pedido.Id = _proximoId;
            _proximoId++;
            _pedido.Estado = EstadoPedido.Recibido;
            
            var resultado = _pedido;
            _pedido = new Pedido();
            _pedido.Items = new List<Item>();


            
            return resultado;
        }
    }
}