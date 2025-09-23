using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DeliveryGo.Core.Payment_posible_eliminacion_
{
    class IPagoBuilder
    {
        IPagoBuilder ConPan(string pan);
        IHamburguesaBuilder ConCarne(string carne);
        IHamburguesaBuilder ConQueso(bool queso = true);
        IHamburguesaBuilder ConLechuga(bool lechuga = true);
        IHamburguesaBuilder ConTomate(bool tomate = true);
        IHamburguesaBuilder ConCebolla(bool cebolla = true);
        IHamburguesaBuilder ConSalsa(string salsa);
        Hamburguesa Build();
    }
}
