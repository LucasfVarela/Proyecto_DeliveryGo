using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DeliveryGo.Core.Command
{
    public class AgregarItemCommand : ICommand
    {
        private readonly Carrito c;
        private readonly Item i;

        public AgregarItemCommand(Carrito carrito, Item item)
        {
            c = carrito;
            i = item;
        }
        public void Execute() => c.Agregar(i);
        public void Undo() => c.Quitar(i.Sku);
    }
}
