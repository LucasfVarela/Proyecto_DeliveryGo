using Proyecto_DeliveryGo.Core.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DeliveryGo.Core.Strategy
{
    public class EnvioCorreo : IEnvioStrategy
    {
        public string Nombre => "Correo";

        public decimal Calcular(decimal subtotal)
        {
            if (subtotal >= ConfigManager.Instance.EnvioGratisDesde)
            {
                return 0m;
            }
            return 3500m;
        }
    }
}
