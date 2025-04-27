using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren;

namespace SproutLands.Classes.Items
{
    public abstract class Item
    {
        public Texture2D Icon { get; set; }
        public string DisplayName { get; protected set; }

        public abstract void Use(Player player);

    }

}
