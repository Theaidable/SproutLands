using SproutLands.Classes.ComponentPattern;
using Microsoft.Xna.Framework;

namespace SproutLands.Classes.FactoryPattern
{
    public abstract class Factory
    {
        //Metode som alle factories skal bruge
        public abstract GameObject Create(Vector2 position);
    }
}
