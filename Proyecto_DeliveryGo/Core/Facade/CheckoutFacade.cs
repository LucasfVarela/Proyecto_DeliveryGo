using System;
using System.Collections.Generic;
using System.Linq;
using Proyecto_DeliveryGo.Core.Command;
using Proyecto_DeliveryGo.Core.Strategy;
using Proyecto_DeliveryGo.Core.Order;
using Proyecto_DeliveryGo.Core.Payment;
using Item = Proyecto_DeliveryGo.Core.Command.Item;

namespace Proyecto_DeliveryGo.Core.Facade
{
    public class CheckoutFacade
    {
        private readonly ICarritoPort _carrito;
        private readonly Carrito _carritoInterno;
        private IEnvioStrategy _envioActual;
        private readonly PedidoService _pedidos;

        public CheckoutFacade(ICarritoPort carrito, Carrito carritoInterno, IEnvioStrategy envioInicial, PedidoService pedidos)
        {
            _carrito = carrito;
            _carritoInterno = carritoInterno;
            _envioActual = envioInicial;
            _pedidos = pedidos;
        }

        public void AgregarItem(string sku, string nombre, decimal precio, int cantidad)
        {
            var item = new Item(sku, nombre, precio, cantidad);
            var comando = new AgregarItemCommand(_carritoInterno, item);
            _carrito.Run(comando);
        }

        public void CambiarCantidad(string sku, int cantidad)
        {
            var comando = new SetCantidadCommand(_carritoInterno, sku, cantidad);
            _carrito.Run(comando);
        }

        public void QuitarItem(string sku)
        {
            var comando = new QuitarItemCommand(_carritoInterno, sku);
            _carrito.Run(comando);
        }

        public void ElegirEnvio(IEnvioStrategy estrategia)
        {
            _envioActual = estrategia;
        }

        public decimal CalcularTotal()
        {
            var subtotal = _carrito.SubTotal();
            var costoEnvio = _envioActual.Calcular(subtotal);
            return subtotal + costoEnvio;
        }

        public bool Pagar(string tipoPago, bool aplicarIVA, decimal? cupon = null)
        {
            IPago pago;

            if (tipoPago == "mp-adapter")
            {
                pago = new PagoAdapterMp(new MpSdkFalsa());
            }
            else
            {
                pago = PagoFactory.Create(tipoPago);
            }

            if (aplicarIVA)
            {
                pago = new PagoConImpuesto(pago);
            }

            if (cupon.HasValue)
            {
                pago = new PagoConCupon(pago, cupon.Value);
            }

            var monto = CalcularTotal();
            return pago.Procesar(monto);
        }

        public Pedido ConfirmarPedido(string direccion, string tipoPago)
        {
            var items = new List<(string sku, string nombre, decimal precio, int cantidad)>();

            var builder = new PedidoBuilder();
            var pedido = builder
                .ConItems(items)
                .ConDireccion(direccion)
                //.ConMetodoPago(tipoPago)
                .ConMonto(CalcularTotal())
                .Build();

            _pedidos.CambiarEstado(pedido.Id, EstadoPedido.Recibido);
            _pedidos.CambiarEstado(pedido.Id, EstadoPedido.Preparando);
            _pedidos.CambiarEstado(pedido.Id, EstadoPedido.Enviado);
            _pedidos.CambiarEstado(pedido.Id, EstadoPedido.Entregado);

            return pedido;
        }
    }
}