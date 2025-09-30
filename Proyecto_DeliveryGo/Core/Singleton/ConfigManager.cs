using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DeliveryGo.Core.Singleton
{
    internal class ConfigManager
    {
        private static ConfigManager _instance;
        private static readonly object _lock = new object();

        private ConfigManager() { }

        public static ConfigManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConfigManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public decimal EnvioGratisDesde { get; set; }
    }
}

