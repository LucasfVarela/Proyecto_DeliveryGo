using System;
using System.Collections.Generic;

namespace Proyecto_DeliveryGo.Core.Order
{
    public class PedidoService
    {
        private Dictionary<int, EstadoPedido> _estados = new Dictionary<int, EstadoPedido>();

        public event EventHandler<PedidoChangedEventArgs> EstadoCambiado;

        public void CambiarEstado(int pedidoId, EstadoPedido nuevoEstado)
        {
            _estados[pedidoId] = nuevoEstado;

            Console.WriteLine($"[PEDIDO] Estado del pedido {pedidoId} cambio a: {nuevoEstado}");

            EstadoCambiado.Invoke(
                this,
                new PedidoChangedEventArgs(pedidoId, nuevoEstado, DateTime.Now)
            );
        }
    }
}
