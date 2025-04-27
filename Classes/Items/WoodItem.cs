using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren;
using System;

namespace SproutLands.Classes.Items
{
    public class WoodItem : Item
    {
        public WoodItem(Texture2D icon)
        {
            Icon = icon;
            DisplayName = "Wood";
        }

        public override void Use(Player player)
        {
            
        }
    }
}
