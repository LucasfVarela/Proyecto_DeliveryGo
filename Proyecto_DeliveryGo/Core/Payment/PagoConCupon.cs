using System;

namespace Proyecto_DeliveryGo.Core.Payment
{
    public class PagoConCupon : IPago
    {
        private readonly IPago _pagoBase;
        private readonly decimal _descuento;
        
        public string Nombre => $"{_pagoBase.Nombre} (- {_descuento})";
    
        public PagoConCupon(IPago pagoBase, decimal porcentajeDescuento)
        {
            _pagoBase = pagoBase;
            _descuento = porcentajeDescuento;
        }
    
        public bool Procesar(decimal monto)
        {
            var montoConDescuento = monto * (1 - _descuento);
        
            Console.WriteLine($"Aplicando cupon ({_descuento}): ${monto} = ${montoConDescuento}");
        
            return _pagoBase.Procesar(montoConDescuento);
        }
    }
}