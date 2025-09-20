using System;

namespace Proyecto_DeliveryGo.Core.Payment
{
    public class PagoAdapterMp : IPago
    {
        private readonly MpSdkFalsa _sdk;
    
        public string Nombre => "Mercado Pago";
    
        public PagoAdapterMp(MpSdkFalsa sdk)
        {
            _sdk = sdk;
        }
    
        public bool Procesar(decimal monto)
        {
            Console.WriteLine($"Adapter: Conectando con MP...");
            return _sdk.Cobrar(monto);
        }
    }
}