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
        private readonly string sku;
        private readonly int nueva;
        private int _anterior = 0;



        public SetCantidadCommand(Carrito carrito, string sku, int nueva)
        {
            c = carrito;
            this.sku = sku;
            this.nueva = nueva;
        }
        public void Execute()
        {
            _anterior = c.GetCantidad(sku);
            c.SetCantidad(sku, nueva);
        }
        public void Undo()
        {
            c.SetCantidad(sku, _anterior);
        }
    }
}
