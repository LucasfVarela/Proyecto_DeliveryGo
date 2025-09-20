using System;

namespace Proyecto_DeliveryGo.Core.Payment
{
    public class PagoTransfer : IPago
    {
        public string Nombre => "Transferencia Bancaria";
        
        public bool Procesar(decimal monto)
        {
            Console.WriteLine("Procesando pago con transferencia bancaria:" + monto);
            Console.WriteLine("Monto ingresado correctamente.");
            return true;
        }
    }
}