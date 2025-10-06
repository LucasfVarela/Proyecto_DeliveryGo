# DeliveryGo

Mini-ecommerce en consola en C# para Practicas de Patrones de Diseno.

## Resumen rapido
DeliveryGo simula un flujo basico de checkout: carrito con undo/redo, calculo de envio, metodos de pago, aplicacion de cupones/IVA, confirmacion y seguimiento de pedidos con notificaciones.

## Caracteristicas
- Agregar, modificar y eliminar items del carrito
- Undo / redo de acciones sobre el carrito
- Metodos de envio con calculo dinamico de costos
- Pagos por tarjeta, transferencia y adapter para MercadoPago
- Decoradores para IVA y cupones
- Estados de pedido y observers para notificaciones
- Facade para simplificar la integracion entre componentes

## Tecnologias
- C# .NET 5.0
- Principios OOP y patrones de diseno
- Git / GitHub

## Requisitos
- .NET SDK 5.0
- Git (opcional)

## Instalacion y ejecucion
git clone https://github.com/tu-usuario/DeliveryGo.git

cd DeliveryGo

dotnet build

dotnet run

## Uso rapido (menu)
1. Agregar item al carrito  
2. Cambiar cantidad de un item  
3. Quitar item del carrito  
4. Ver subtotal y total  
5. Deshacer ultima operacion (undo)  
6. Rehacer operacion (redo)  
7. Elegir metodo de envio  
8. Procesar pago  
9. Confirmar pedido  
10. Suscribir/desuscribir observador de logistica  
0. Salir

## Patrones aplicados (resumen)
Command, Strategy, Singleton, Factory, Adapter, Decorator, Builder, Observer, Facade.

## Planes para v2
- Autenticacion y historial de usuarios  
- Persistencia en JSON o DB

## Equipo
- <img src="https://avatars.githubusercontent.com/u/179079172?v=4" width="24" height="24"> [Lautaro Vallejo](https://github.com/lautaro-vallejo) - Carrito y Command  
- <img src="https://avatars.githubusercontent.com/u/100593063?v=4" width="24" height="24"> [Lucas Varela](https://github.com/LucasfVarela) - Envio y Strategy  
- <img src="https://avatars.githubusercontent.com/u/179088507?v=4" width="24" height="24"> [Lautaro Nuccitelli](https://github.com/lautaronuccitelli) - Pagos, Builder, Observers, Facade


## UML - Diagrama de clases

```mermaid
classDiagram
    %% ========== INTERFACES ==========
    class ICommand {
        <<interface>>
        +Execute() void
        +Undo() void
    }
    
    class ICarritoPort {
        <<interface>>
        +Subtotal() decimal
        +Run(ICommand cmd) void
        +Undo() void
        +Redo() void
    }
    
    class IEnvioStrategy {
        <<interface>>
        +Calcular(decimal subtotal) decimal
        +Nombre string
    }
    
    class IPago {
        <<interface>>
        +Nombre string
        +Procesar(decimal monto) bool
    }
    
    class IPedidoBuilder {
        <<interface>>
        +ConItems(items) IPedidoBuilder
        +ConDireccion(string direccion) IPedidoBuilder
        +ConMetodoPago(string tipoPago) IPedidoBuilder
        +Build() Pedido
    }

    %% ========== ENTIDADES ==========
    class Item {
        +string Sku
        +string Nombre
        +decimal Precio
        +int Cantidad
    }
    
    class Pedido {
        +int Id
        +List~Item~ Items
        +string Direccion
        +string TipoPago
        +EstadoPedido Estado
        +decimal Monto
    }
    
    class EstadoPedido {
        <<enumeration>>
        Recibido
        Preparando
        Enviado
        Entregado
    }

    %% ========== PATRON COMMAND ==========
    class Carrito {
        -Dictionary~string,Item~ _items
        +Agregar(Item i) void
        +Quitar(string sku) Item
        +SetCantidad(string sku, int nueva) bool
        +Subtotal() decimal
    }
    
    class EditorCarrito {
        -Stack~ICommand~ _undo
        -Stack~ICommand~ _redo
        -Carrito _carrito
        +Run(ICommand cmd) void
        +Undo() void
        +Redo() void
    }
    
    class CarritoPort {
        -Carrito _carrito
        -EditorCarrito _editor
        +Subtotal() decimal
        +Run(ICommand cmd) void
        +Undo() void
        +Redo() void
    }
    
    class AgregarItemCommand {
        -Carrito _carrito
        -Item _item
        +Execute() void
        +Undo() void
    }
    
    class QuitarItemCommand {
        -Carrito _carrito
        -string _sku
        -Item _backup
        +Execute() void
        +Undo() void
    }
    
    class SetCantidadCommand {
        -Carrito _carrito
        -string _sku
        -int _nueva
        -int _anterior
        +Execute() void
        +Undo() void
    }

    %% ========== PATRON STRATEGY ==========
    class EnvioMoto {
        +Nombre string
        +Calcular(decimal subtotal) decimal
    }
    
    class EnvioCorreo {
        +Nombre string
        +Calcular(decimal subtotal) decimal
    }
    
    class RetiroEnTienda {
        +Nombre string
        +Calcular(decimal subtotal) decimal
    }
    
    class EnvioService {
        -IEnvioStrategy _actual
        +SetStrategy(IEnvioStrategy s) void
        +Calcular(decimal subtotal) decimal
        +NombreActual string
    }

    %% ========== PATRON SINGLETON ==========
    class ConfigManager {
        <<singleton>>
        -static Lazy~ConfigManager~ _inst
        +static Instance ConfigManager
        +decimal EnvioGratisDesde
        +decimal IVA
    }

    %% ========== PATRON FACTORY ==========
    class PagoFactory {
        <<factory>>
        +Create(string tipo) IPago
    }
    
    class PagoTarjeta {
        +Nombre string
        +Procesar(decimal monto) bool
    }
    
    class PagoTransfer {
        +Nombre string
        +Procesar(decimal monto) bool
    }
    
    class PagoMp {
        +Nombre string
        +Procesar(decimal monto) bool
    }

    %% ========== PATRON ADAPTER ==========
    class MpSdkFalsa {
        +Cobrar(decimal monto) bool
    }
    
    class PagoAdapterMp {
        -MpSdkFalsa _sdk
        +Nombre string
        +Procesar(decimal monto) bool
    }

    %% ========== PATRON DECORATOR ==========
    class PagoConImpuesto {
        -IPago _inner
        +Nombre string
        +Procesar(decimal monto) bool
    }
    
    class PagoConCupon {
        -IPago _inner
        -decimal _porcentaje
        +Nombre string
        +Procesar(decimal monto) bool
    }

    %% ========== PATRON BUILDER ==========
    class PedidoBuilder {
        -List~Item~ _items
        -string _direccion
        -string _tipoPago
        -decimal _monto
        +ConItems(items) IPedidoBuilder
        +ConDireccion(string) IPedidoBuilder
        +ConMetodoPago(string) IPedidoBuilder
        +ConMonto(decimal) IPedidoBuilder
        +Build() Pedido
    }

    %% ========== PATRON OBSERVER ==========
    class PedidoService {
        +event EventHandler~PedidoChangedEventArgs~ EstadoCambiado
        +CambiarEstado(int id, EstadoPedido nuevo) void
    }
    
    class PedidoChangedEventArgs {
        +int PedidoId
        +EstadoPedido NuevoEstado
        +DateTime Cuando
    }
    
    class ClienteObserver {
        +Suscribir(PedidoService s) void
        +Desuscribir(PedidoService s) void
        -OnEstadoCambiado(sender, args) void
    }
    
    class LogisticaObserver {
        +Suscribir(PedidoService s) void
        +Desuscribir(PedidoService s) void
        -OnEstadoCambiado(sender, args) void
    }
    
    class AuditoriaObserver {
        +Suscribir(PedidoService s) void
        +Desuscribir(PedidoService s) void
        -OnEstadoCambiado(sender, args) void
    }

    %% ========== PATRON FACADE ==========
    class CheckoutFacade {
        -ICarritoPort _carrito
        -IEnvioStrategy _envioActual
        -PedidoService _pedidos
        +AgregarItem(sku, nombre, precio, cant) void
        +CambiarCantidad(sku, cant) void
        +QuitarItem(sku) void
        +ElegirEnvio(IEnvioStrategy) void
        +CalcularTotal() decimal
        +Pagar(tipo, aplicarIVA, cupon) bool
        +ConfirmarPedido(direccion, tipoPago) Pedido
    }

    %% ========== RELACIONES COMMAND ==========
    ICommand <|.. AgregarItemCommand
    ICommand <|.. QuitarItemCommand
    ICommand <|.. SetCantidadCommand
    
    AgregarItemCommand --> Carrito
    QuitarItemCommand --> Carrito
    SetCantidadCommand --> Carrito
    
    EditorCarrito --> ICommand
    EditorCarrito --> Carrito
    
    CarritoPort --> Carrito
    CarritoPort --> EditorCarrito
    ICarritoPort <|.. CarritoPort
    
    Carrito --> Item

    %% ========== RELACIONES STRATEGY ==========
    IEnvioStrategy <|.. EnvioMoto
    IEnvioStrategy <|.. EnvioCorreo
    IEnvioStrategy <|.. RetiroEnTienda
    
    EnvioService --> IEnvioStrategy
    EnvioCorreo --> ConfigManager
    
    %% ========== RELACIONES FACTORY & PAGOS ==========
    IPago <|.. PagoTarjeta
    IPago <|.. PagoTransfer
    IPago <|.. PagoMp
    IPago <|.. PagoAdapterMp
    IPago <|.. PagoConImpuesto
    IPago <|.. PagoConCupon
    
    PagoFactory ..> PagoTarjeta : creates
    PagoFactory ..> PagoTransfer : creates
    PagoFactory ..> PagoMp : creates
    
    PagoAdapterMp --> MpSdkFalsa
    
    PagoConImpuesto --> IPago
    PagoConImpuesto --> ConfigManager
    PagoConCupon --> IPago

    %% ========== RELACIONES BUILDER ==========
    IPedidoBuilder <|.. PedidoBuilder
    PedidoBuilder ..> Pedido : builds
    Pedido --> EstadoPedido
    Pedido --> Item

    %% ========== RELACIONES OBSERVER ==========
    PedidoService --> PedidoChangedEventArgs
    ClienteObserver --> PedidoService
    LogisticaObserver --> PedidoService
    AuditoriaObserver --> PedidoService

    %% ========== RELACIONES FACADE ==========
    CheckoutFacade --> ICarritoPort
    CheckoutFacade --> IEnvioStrategy
    CheckoutFacade --> PedidoService
    CheckoutFacade --> PagoFactory
    CheckoutFacade --> IPedidoBuilder
    CheckoutFacade --> IPago
