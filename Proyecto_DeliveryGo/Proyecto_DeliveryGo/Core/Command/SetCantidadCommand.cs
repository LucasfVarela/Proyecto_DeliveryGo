using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_DeliveryGo.Core.Command;

namespace Proyecto_DeliveryGo.Core.Command
{
    public class SetCantidadCommand : ICommand
    {
        private readonly Carrito c;
        private readonly string _sku;
        private readonly int _nueva;
        private int _anterior = 0;
        public void Execute()
        {
            _anterior = c.GetCantidad(_sku);
            c.SetCantidad(_sku, _nueva);
        }
        public void Undo()
        {
            c.SetCantidad(_sku, _anterior);
        }
    }
}
