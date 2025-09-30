using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DeliveryGo.Core.Strategy
{
    public class RetiroEnTienda : IEnvioStrategy
    {
        public string Nombre => "Retiro";

        public decimal Calcular(decimal subtotal)
        {
            return 0m;
        }
    }
}
