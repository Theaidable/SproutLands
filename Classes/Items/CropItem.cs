using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren;

namespace SproutLands.Classes.Items
{
    public class CropItem : Item
    {
        public CropItem(Texture2D icon)
        {
            Icon = icon;
        }

        public override void Use(Player player)
        {

        }
    }
}
