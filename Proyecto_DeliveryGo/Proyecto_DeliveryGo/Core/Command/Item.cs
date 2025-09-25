using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DeliveryGo.Core.Command
{
    public class Item
    {
        public Item(string nombre, string sku, decimal precio, int cantidad)
        {
            Nombre=nombre;
            Sku=sku;
            Precio=precio;
            Cantidad=cantidad;
        }
        public string Nombre { get; init; } = "";
        public string Sku { get; init; } = "";
        public decimal Precio { get; init; }
        public int Cantidad { get; set; }
    }
}
  