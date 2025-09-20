using System;

namespace Proyecto_DeliveryGo.Core.Payment
{
    public class PagoTarjeta : IPago
    {
        public string Nombre => "Tarjeta";
        
        public bool Procesar(decimal monto)
        {
            Console.WriteLine("Procesando pago con tarjeta:" + monto);
            Console.WriteLine("Monto ingresado correctamente.");
            return true;
        }
    }
}