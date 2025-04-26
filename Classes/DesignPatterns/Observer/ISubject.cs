using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutLands.Classes.DesignPatterns.Observer
{
    public interface ISubject
    {
        void Attach(IObServer observer);

        void Detach(IObServer observer);
        void Notify();
    }
}
