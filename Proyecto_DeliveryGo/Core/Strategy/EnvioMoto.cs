﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DeliveryGo.Core.Strategy
{
    public class EnvioMoto : IEnvioStrategy
    {
        public string Nombre => "Moto";

        public decimal Calcular(decimal subtotal)
        {
            return 1200m;

        }
    }
}
