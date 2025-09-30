using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DeliveryGo.Core.Strategy
{
    public class EnvioService
    {
        private IEnvioStrategy actual;

        public void SetStrategy(IEnvioStrategy s)
        {
            actual = s;
        }

        public decimal Calcular(decimal subtotal)
        {
            if (actual == null)
                throw new InvalidOperationException("No se ha establecido una estrategia de envío");

            return actual.Calcular(subtotal);
        }

        public string NombreActual
        {
            get
            {
                if (actual == null)
                    return "Sin estrategia";
                return actual.Nombre;
            }
        }
    }
}
