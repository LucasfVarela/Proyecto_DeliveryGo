
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DeliveryGo.Core.Command
{
    public class CarritoPort : ICarritoPort
    {
        //private Carrito _carrito= new();
        private EditorCarrito _editor = new();

        public CarritoPort()
        {
            //_carrito = new Carrito();
            _editor = new EditorCarrito();
        }
        public decimal SubTotal() => Carrito.Instancia.Subtotal();
        public void Run(ICommand command)
        {
            _editor.Run(command);
        }

        public void Undo()
        {
            _editor.Undo();
        }

        public void Redo()
        {
            _editor.Redo();
        }

    }
}