using System;

namespace Proyecto_DeliveryGo.Core.Order.Observers
{
    public class LogisticaObserver
    {
        public void Suscribir(PedidoService s) => s.EstadoCambiado += OnEstadoCambiado;
        public void Desuscribir(PedidoService s) => s.EstadoCambiado -= OnEstadoCambiado;

        private void OnEstadoCambiado(object? sender, PedidoChangedEventArgs e)
        {
            Console.WriteLine($"Pedido {e.PedidoId} => {e.NuevoEstado} ({e.Cuando:HH:mm:ss})");
        }
    }
}