using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_DeliveryGo.Core.Command;

namespace Proyecto_DeliveryGo.Core.Command
{
    public interface ICarritoPort
    {
        decimal SubTotal();
        void Run(ICommand command);
        void Undo();
        void Redo();
        

    }
}
