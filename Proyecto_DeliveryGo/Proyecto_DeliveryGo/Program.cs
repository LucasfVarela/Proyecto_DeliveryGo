using Proyecto_DeliveryGo.Core.Command;
using System;


namespace Proyecto_DeliveryGo
    {
        internal class Program
        {
        static void Main(string[] args)
        {

           Console.WriteLine("=== Prueba del Sistema de Carrito ===");

            var carritoPort = new CarritoPort();

            var agregarCmd = new AgregarItemCommand();
                carritoPort.Run(agregarCmd);
                Console.WriteLine("Después de agregar A001");

                var setCantidadCmd = new SetCantidadCommand();
                carritoPort.Run(setCantidadCmd);
                Console.WriteLine("Después de SetCantidad a 3");

                var quitarCmd = new QuitarItemCommand();
                carritoPort.Run(quitarCmd);
                Console.WriteLine("Después de quitar A001");

                carritoPort.Undo();
                Console.WriteLine("Después de Undo");

                carritoPort.Redo();
                Console.WriteLine("Después de Redo");

                Console.WriteLine("\n=== Fin de la prueba ==="); 
             }
        }
 }

    

