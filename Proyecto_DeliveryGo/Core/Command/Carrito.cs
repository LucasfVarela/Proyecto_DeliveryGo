using Proyecto_DeliveryGo.Core.Command;
using Proyecto_DeliveryGo.Core.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DeliveryGo.Core.Command
{
    public class Carrito
    {
        private readonly Dictionary<string, Item> _items = new();

        private static Carrito? _instancia;

        private Carrito() { }

      
        public static Carrito Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new Carrito();
                }
                return _instancia;
            }
        }


        public void Agregar(Item item)
        
        {
            if (_items.ContainsKey(item.Sku))
            {
                _items[item.Sku].Cantidad += item.Cantidad;
            }
            else
            {
                _items[item.Sku] = new Item(item.Sku, item.Nombre, item.Precio, item.Cantidad);
            }
        }

     

        public int GetCantidad(string sku)
        {
            return _items.ContainsKey(sku) ? _items[sku].Cantidad : 0;
        }
        public Item? Quitar(string sku)
        {
            if (_items.TryGetValue(sku, out var item)){

                _items.Remove(sku);

                return new Item(item.Sku, item.Nombre, item.Precio, item.Cantidad);
            }


            return null;
        }

        public bool SetCantidad(string sku, int nueva)
        {
            if (_items.ContainsKey(sku))
            {
                _items[sku].Cantidad = nueva;
                return true;
            }
            return false;
        }
        public IEnumerable<Item> ObtenerItems()
        {
            return _items.Values;
        }

        public decimal Subtotal()
        {
          return  _items.Values.Sum(i => i.Precio * i.Cantidad);
        }
    }
}
