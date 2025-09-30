using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DeliveryGo.Core.Strategy
{
    public interface IEnvioStrategy
    {
        string Nombre { get; }
        decimal Calcular(decimal subtotal);
    }
}
