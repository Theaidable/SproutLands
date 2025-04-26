using Microsoft.Xna.Framework;
using SproutLands.Classes.DesignPatterns.Composite;


namespace SproutLands.Classes.DesignPatterns.FactoryPattern
{
    public abstract class Factory
    {
        public abstract GameObject Create(Vector2 position);
    }
}
