using SproutLands.Classes.ComponentPattern;
using Microsoft.Xna.Framework;

namespace SproutLands.Classes.FactoryPattern
{
    public abstract class Factory
    {
        public abstract GameObject Create(Vector2 position);
    }
}
