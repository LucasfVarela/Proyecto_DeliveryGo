using System;

namespace Proyecto_DeliveryGo.Core.Payment
{
    public class MpSdkFalsa
    {
        public bool Cobrar(decimal monto)
        {
            Console.WriteLine("Cobrando monto...");
            bool exito = monto > 0;
            Console.WriteLine(exito ? "Cobro exitoso" : "Error en cobro");
            return exito;
        }
    }
}