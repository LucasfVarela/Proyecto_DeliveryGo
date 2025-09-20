using System;

namespace Proyecto_DeliveryGo.Core.Payment
{
    public class PagoConImpuesto : IPago
    {
        private readonly IPago _pagoBase;
        
        public string Nombre => $"{_pagoBase.Nombre} (+IVA).";
        
        public PagoConImpuesto(IPago pagoBase)
        {
            _pagoBase = pagoBase;
        }
        
        public bool Procesar(decimal monto)
        {
            var montoConIva = monto * 1.21m;

            Console.WriteLine("Monto con iva: " + montoConIva);
            
            return _pagoBase.Procesar(montoConIva);
        }
    }
}