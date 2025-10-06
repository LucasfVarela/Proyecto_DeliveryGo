using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DeliveryGo.Core.Command
{
    public class AgregarItemCommand : ICommand
    {
        private readonly Carrito _carrito;
        private readonly Item _Item;

        public AgregarItemCommand(Carrito carrito, Item item)
        {
            _carrito = carrito;
            _Item = item;
        }
        public void Execute() => _carrito.Agregar(_Item); //.Agregar(i);
        public void Undo() => _carrito.Quitar(_Item.Sku);
    }
}
