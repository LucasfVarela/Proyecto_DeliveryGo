using System;

namespace Proyecto_DeliveryGo.Core.Payment
{
    public class PagoMp: IPago
    {
        public string Nombre => "Mercado Pago";
        
        public bool Procesar(decimal monto)
        {
            Console.WriteLine("Procesando pago con Mercado Pago:" + monto);
            Console.WriteLine("Monto ingresado correctamente.");
            return true;
        }
    }
    
}