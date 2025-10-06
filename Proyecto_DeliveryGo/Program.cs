
using Proyecto_DeliveryGo;
using Proyecto_DeliveryGo.Core.Command;
using Proyecto_DeliveryGo.Core.Facade;
using Proyecto_DeliveryGo.Core.Order;
using Proyecto_DeliveryGo.Core.Order.Observers;
using Proyecto_DeliveryGo.Core.Singleton;
using Proyecto_DeliveryGo.Core.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryGo
{
    class Program
    {

        private static CheckoutFacade _facade = null!;
        private static ICarritoPort _carrito = null!;
        private static PedidoService _pedidos = null!;
        private static LogisticaObserver _logisticaObs = null!;
        private static bool _logisticaSuscripta = true;

        static void Main(string[] args)
        {
            InicializarSistema();

            bool continuar = true;
            while (continuar)
            {
                UI.BuildMenu("Menú Principal", new List<string> {
                              "  CARRITO",
                              "  -------",
                              "  1. Agregar ítem",
                              "  2. Cambiar cantidad",
                              "  3. Quitar ítem",
                              "  4. Ver ítems",
                              "  5. Ver subtotal y total",
                              "  6. Deshacer (Undo)",
                              "  7. Rehacer (Redo)",
                              "  CONFIGURACIÓN",
                              "  -------------",
                              "  8. Elegir método de envío",
                              "  CHECKOUT",
                              "  --------",
                              "  9. Pagar",
                              "  10. Confirmar pedido",
                              "  AVANZADO",
                              "  --------",
                              "  11. (Des)Suscribir Logística",
                              "  0. Salir",
                });
                Console.Write("Seleccione una opción:");
                int opcion = Convert.ToInt16(Console.ReadLine());
                Console.WriteLine("-------------------------------------------");

                continuar = ProcesarOpcion(opcion);

                if (continuar)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }

            Console.WriteLine("\n¡Gracias por usar DeliveryGo! Hasta pronto.\n");
        }

        #region Inicialización

        static void InicializarSistema()
        {
            // Configuración global
            ConfigManager.Instance.EnvioGratisDesde = 50000m;
            ConfigManager.Instance.IVA = 0.21m;

            // Carrito
            _carrito = new CarritoPort();

            // Estrategia de envío inicial
            IEnvioStrategy envioInicial = new EnvioMoto();

            // Servicio de pedidos
            _pedidos = new PedidoService();

            // Observers
            var clienteObs = new ClienteObserver();
            _logisticaObs = new LogisticaObserver();
            var auditoriaObs = new AuditoriaObserver();

            clienteObs.Suscribir(_pedidos);
            _logisticaObs.Suscribir(_pedidos);
            auditoriaObs.Suscribir(_pedidos);

            // Facade
            _facade = new CheckoutFacade(_carrito, envioInicial, _pedidos);
        }



        #endregion

        #region Menú Principal
        static bool ProcesarOpcion(int opcion)
        {
            Console.Clear();

            var opciones = new Dictionary<int, Action>
                {
                    { 1, AgregarItem },
                    { 2, CambiarCantidad },
                    { 3, QuitarItem },
                    { 4, VerCarrito },
                    { 5, VerTotales },
                    { 6, HacerUndo },
                    { 7, HacerRedo },
                    { 8, ElegirEnvio },
                    { 9, Pagar },
                    { 10, ConfirmarPedido },
                    { 11, ToggleLogistica }
                };

            if (opcion == 0)
                return false;

            if (opciones.TryGetValue(opcion, out Action accion))
                accion();
            else
                Console.WriteLine("  Opción inválida.");

            return true;
        }
    

        #endregion

        #region Opciones del Menú

        static void AgregarItem()
        {
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            Console.WriteLine("║          AGREGAR ÍTEM AL CARRITO          ║");
            Console.WriteLine("╚═══════════════════════════════════════════╝");
            Console.WriteLine();

            string sku = SetTexto("SKU del producto");
            string nombre = SetTexto("Nombre del producto");
            decimal precio = SetPrecio("Precio unitario");
            int cantidad = SetCantidad("Cantidad");

            _facade.AgregarItem(sku, nombre, precio, cantidad);

            Console.WriteLine();
            Console.WriteLine($"   Producto '{nombre}' agregado correctamente.");
            Console.WriteLine($"   SKU: {sku} | Precio: ${precio:N2} | Cantidad: {cantidad}");
        }

        static void CambiarCantidad()
        {
            VerCarrito();
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            Console.WriteLine("║            CAMBIAR CANTIDAD               ║");
            Console.WriteLine("╚═══════════════════════════════════════════╝");
            Console.WriteLine();

            string sku = SetTexto("SKU del producto");
            int cantidad = SetCantidad("Nueva cantidad");

            _facade.CambiarCantidad(sku, cantidad);

            Console.WriteLine();
            Console.WriteLine($"Cantidad del producto '{sku}' actualizada a {cantidad}.");
        }

        static void QuitarItem()
        {

            VerCarrito();
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            Console.WriteLine("║         QUITAR ÍTEM DEL CARRITO           ║");
            Console.WriteLine("╚═══════════════════════════════════════════╝");
            Console.WriteLine();

            string sku = SetTexto("SKU del producto a quitar");

            _facade.QuitarItem(sku);

            Console.WriteLine();
            Console.WriteLine($"Producto '{sku}' quitado del carrito.");
        }

        static void VerTotales()
        {
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            Console.WriteLine("║          SUBTOTAL Y TOTAL                 ║");
            Console.WriteLine("╚═══════════════════════════════════════════╝");
            Console.WriteLine();

            decimal subtotal = _facade.SubTotal();
            decimal total = _facade.CalcularTotal();
            decimal envio = total - subtotal;

            Console.WriteLine($"  Subtotal (productos):  ${subtotal:N2}");
            Console.WriteLine($"  Costo de envío:        ${envio:N2}");
            Console.WriteLine("  -------------------------------------------");
            Console.WriteLine($"  TOTAL:                 ${total:N2}");
            Console.WriteLine();
            Console.WriteLine($"Envío gratis desde: ${ConfigManager.Instance.EnvioGratisDesde:N2}");
        }

        static void HacerUndo()
        {
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            Console.WriteLine("║               DESHACER (UNDO)             ║");
            Console.WriteLine("╚═══════════════════════════════════════════╝");
            Console.WriteLine();

            _carrito.Undo();

            Console.WriteLine("  Última acción deshecha correctamente.");
        }

        static void HacerRedo()
        {
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            Console.WriteLine("║               REHACER (REDO)              ║");
            Console.WriteLine("╚═══════════════════════════════════════════╝");
            Console.WriteLine();

            _carrito.Redo();

            Console.WriteLine("  Acción rehecha correctamente.");
        }

        static void ElegirEnvio()
        {
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            Console.WriteLine("║         ELEGIR MÉTODO DE ENVÍO            ║");
            Console.WriteLine("╚═══════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("  Opciones disponibles:");
            Console.WriteLine("  1. moto    - Envío en moto ($1,200)");
            Console.WriteLine("  2. correo  - Envío por correo ($3,500 o gratis)");
            Console.WriteLine("  3. retiro  - Retiro en tienda (Gratis)");
            Console.WriteLine();

            string tipo = SetTexto("Tipo de envío [moto/correo/retiro]").ToLower();

            IEnvioStrategy estrategia = tipo switch
            {
                "moto" => new EnvioMoto(),
                "correo" => new EnvioCorreo(),
                "retiro" => new RetiroEnTienda(),
                _ => throw new ArgumentException("Tipo de envío no válido")
            };

            _facade.ElegirEnvio(estrategia);

            Console.WriteLine();
            Console.WriteLine($"   Método de envío seleccionado: {estrategia.Nombre}");
            Console.WriteLine($"      Envío gratis desde: ${ConfigManager.Instance.EnvioGratisDesde:N2}");
        }

        static void Pagar()
        {
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            Console.WriteLine("║              PROCESAR PAGO                ║");
            Console.WriteLine("╚═══════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("  Métodos de pago disponibles:");
            Console.WriteLine("  - tarjeta");
            Console.WriteLine("  - mp");
            Console.WriteLine("  - transf");
            Console.WriteLine("  - mp-adapter (SDK externa)");
            Console.WriteLine();

            string tipoPago = SetTexto("Método de pago").ToLower();

            if (!new[] { "tarjeta", "mp", "transf", "mp-adapter" }.Contains(tipoPago))
            {
                Console.WriteLine("  Método de pago no válido.");
                return;
            }

            bool aplicarIVA = LeerSiNo(" Aplicar IVA? (s/n)");

            decimal? cupon = null;
            if (LeerSiNo(" Aplicar cupón de descuento? (s/n)"))
            {
                cupon = SetPorcentaje("Porcentaje de descuento (0.10 = 10%)", 0.01m, 0.99m);
            }

            Console.WriteLine();
            Console.WriteLine("  Procesando pago...");
            Console.WriteLine();

            bool exito = _facade.Pagar(tipoPago, aplicarIVA, cupon);

            Console.WriteLine();
            if (exito)
            {
                Console.WriteLine("  PAGO APROBADO");
                decimal total = _facade.CalcularTotal();
                if (aplicarIVA)
                    total *= (1 + ConfigManager.Instance.IVA);
                if (cupon.HasValue)
                    total *= (1 - cupon.Value);
                Console.WriteLine($"   Monto total cobrado: ${total:N2}");
            }
            else
            {
                Console.WriteLine("  PAGO RECHAZADO");
            }
        }

        static void ConfirmarPedido()
        {
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            Console.WriteLine("║            CONFIRMAR PEDIDO               ║");
            Console.WriteLine("╚═══════════════════════════════════════════╝");
            Console.WriteLine();

            if (_carrito.SubTotal() == 0)
            {
                Console.WriteLine("  El carrito está vacío. Agregue productos antes de confirmar.");
                return;
            }

            string direccion = SetTexto("Dirección de entrega");
            string tipoPago = SetTexto("Método de pago registrado [tarjeta/mp/transf]");

            Console.WriteLine();
            Console.WriteLine("  Confirmando pedido...");
            Console.WriteLine("  (Observará las notificaciones del Observer)");
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------");

            Pedido pedido = _facade.ConfirmarPedido(direccion, tipoPago);

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("  PEDIDO CONFIRMADO");
            Console.WriteLine($"   ID: {pedido.Id}");
            Console.WriteLine($"   Dirección: {pedido.Direccion}");
            Console.WriteLine($"   Método de pago: {pedido.TipoPago}");
            Console.WriteLine($"   Monto total: ${pedido.Monto:N2}");
            Console.WriteLine($"   Estado actual: {pedido.Estado}");
        }

        static void ToggleLogistica()
        {
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            Console.WriteLine("║      (DES)SUSCRIBIR LOGÍSTICA             ║");
            Console.WriteLine("╚═══════════════════════════════════════════╝");
            Console.WriteLine();

            if (_logisticaSuscripta)
            {
                _logisticaObs.Desuscribir(_pedidos);
                _logisticaSuscripta = false;
                Console.WriteLine("   Observer de Logística DESUSCRIPTO");
                Console.WriteLine("   Ya no recibirá notificaciones de cambios de estado.");
            }
            else
            {
                _logisticaObs.Suscribir(_pedidos);
                _logisticaSuscripta = true;
                Console.WriteLine("   Observer de Logística SUSCRIPTO");
                Console.WriteLine("   Recibirá notificaciones de cambios de estado.");
            }

            Console.WriteLine();
            Console.WriteLine("     Confirme un pedido para ver el efecto.");
        }

        static void VerCarrito()
        {
            Console.WriteLine("CARRITO DE COMPRAS");
            Console.WriteLine();

            var items = Carrito.Instancia.ObtenerItems();

            if (!items.Any())
            {
                Console.WriteLine("El carrito está vacío.");
                return;
            }

            Console.WriteLine("SKU       | Nombre                    | Precio    | Cantidad | Subtotal");
            Console.WriteLine("----------|---------------------------|-----------|----------|----------");

            foreach (var item in items)
            {
                decimal subtotalItem = item.Precio * item.Cantidad;
                Console.WriteLine($"{item.Sku,-10}| {item.Nombre,-26}| ${item.Precio,8:N2} | {item.Cantidad,8} | ${subtotalItem,8:N2}");
            }

            Console.WriteLine("----------|---------------------------|-----------|----------|----------");
            Console.WriteLine($"{"",10}| {"",26}| {"",10}| {"TOTAL:",8} | ${_carrito.SubTotal(),8:N2}");
        }

        #endregion

        #region Helpers de Entrada

        static string SetTexto(string prompt)
        {
            string? input;
            do
            {
                Console.Write($"  {prompt}: ");
                input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Este campo no puede estar vacío. Intente nuevamente.");
                }
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }

        static int SetCantidad(string prompt)
        {
            int valor;
            bool valido;

            do
            {
                Console.Write($"  {prompt}: ");
                string? input = Console.ReadLine();
                valido = int.TryParse(input, out valor);

                if (!valido || valor < 0)
                {
                    Console.WriteLine("Debe ingresar una cantidad válida.");
                    Console.ReadKey();
                }

            } while (!valido);

            return valor;
        }

        static decimal SetPrecio(string prompt)
        {
            decimal valor;
            bool valido;

            do
            {
                Console.Write($"  {prompt}: ");
                string? input = Console.ReadLine();
                valido = decimal.TryParse(input, out valor);

                if (!valido || valor < 0)
                {
                    Console.WriteLine("Debe ingresar un precio válido.");
                    Console.ReadKey();
                }

            } while (!valido);

            return valor;
        }
        static decimal SetPorcentaje(string prompt, decimal minimo, decimal maximo = decimal.MaxValue)
        {
            decimal valor;
            bool valido;

            do
            {
                Console.Write($"  {prompt}: ");
                string? input = Console.ReadLine();
                valido = decimal.TryParse(input, out valor);

                if (!valido)
                {
                    Console.WriteLine("Debe ingresar un porcentaje  válido.");
                }
                else if (valor < minimo || valor > maximo)
                {
                    Console.WriteLine($"El valor debe estar entre {minimo} y {maximo}.");
                    valido = false;
                }
            } while (!valido);

            return valor;
        }

        static bool LeerSiNo(string prompt)
        {
            string? respuesta;
            do
            {
                Console.Write($"  {prompt}: ");
                respuesta = Console.ReadLine()?.Trim().ToLower();

                if (respuesta != "s" && respuesta != "n")
                {
                    Console.WriteLine("Responda con 's' para sí o 'n' para no.");
                }
            } while (respuesta.ToLower() != "s" && respuesta.ToLower() != "n");

            return respuesta.ToLower() == "s";
        }

        #endregion
    }
}
