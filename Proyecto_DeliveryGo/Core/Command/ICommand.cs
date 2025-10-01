using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DeliveryGo.Core.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
