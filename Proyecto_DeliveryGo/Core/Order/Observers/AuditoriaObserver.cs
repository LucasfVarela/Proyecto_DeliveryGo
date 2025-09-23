using System;

namespace Proyecto_DeliveryGo.Core.Order.Observers
{
    public class AuditoriaObserver
    {
        public void Suscribir(PedidoService s) => s.EstadoCambiado += OnEstadoCambiado;
        public void Desuscribir(PedidoService s) => s.EstadoCambiado -= OnEstadoCambiado;

        private void OnEstadoCambiado(object? sender, PedidoChangedEventArgs e)
        {
            Console.WriteLine($"{e.Cuando:HH:mm:ss} -> Pedido {e.PedidoId} = {e.NuevoEstado}");
        }
    }
}