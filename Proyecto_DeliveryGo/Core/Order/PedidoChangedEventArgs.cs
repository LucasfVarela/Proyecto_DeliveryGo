using System;

namespace Proyecto_DeliveryGo.Core.Order
{
    public class PedidoChangedEventArgs : EventArgs
    {
        public int PedidoId { get; }
        public EstadoPedido NuevoEstado { get; }
        public DateTime Cuando { get; }

        public PedidoChangedEventArgs(int pedidoId, EstadoPedido nuevoEstado, DateTime cuando) => (PedidoId, NuevoEstado, Cuando) = (pedidoId, nuevoEstado, cuando);
    }
}
