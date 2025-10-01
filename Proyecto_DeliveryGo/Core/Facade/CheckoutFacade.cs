using Proyecto_DeliveryGo.Core.Command;
using Proyecto_DeliveryGo.Core.Strategy;
using Proyecto_DeliveryGo.Core.Order;

namespace Proyecto_DeliveryGo.Core.Facade
{
    public class CheckoutFacade
    {
        public CheckoutFacade(ICarritoPort carrito, IEnvioStrategy envioInicial, PedidoService pedidos)
        {

        }
        private readonly ICarritoPort _carrito;
        private readonly IEnvioStrategy _envioInicial;
        private readonly PedidoService _pedidos;

        public void AgregarItem(string sku, string nombre, decimal precio, int cantidad)
        {
            
        }
    }
}