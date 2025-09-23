using System;
using Proyecto_DeliveryGo.Core.Order;

namespace Proyecto_DeliveryGo.Core.Order.Observers
{
    public class ClienteObserver
    {
        public void Suscribir(PedidoService s) => s.EstadoCambiado += OnEstadoCambiado;
        public void Desuscribir(PedidoService s) => s.EstadoCambiado -= OnEstadoCambiado;

        private void OnEstadoCambiado(object? sender, PedidoChangedEventArgs e)
        {
            Console.WriteLine($"Tu pedido {e.PedidoId} ahora esta: {e.NuevoEstado}");
        }
    }
}