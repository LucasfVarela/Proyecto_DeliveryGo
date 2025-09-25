using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DeliveryGo.Core.Command
{
    public class QuitarItemCommand : ICommand
    {
        private readonly Carrito c;
        private readonly string _sku;
        private Item? _backup = null;
        public void Execute()
        {
            _backup = c.Quitar(_sku);
        }
        public void Undo()
        {
            if (_backup != null)
            {
                c.Agregar(_backup);
            }
        }
    }
}
