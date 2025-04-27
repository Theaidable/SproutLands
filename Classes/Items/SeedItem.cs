using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.DesignPatterns.Composite;
using SproutLands.Classes.DesignPatterns.FactoryPattern.Crop;
using SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren;
using SproutLands.Classes.DesignPatterns.State.SoilState.SoilStates;
using SproutLands.Classes.World;
using SproutLands.Classes.World.Tiles;
using System.Diagnostics;

namespace SproutLands.Classes.Items
{
    public class SeedItem : Item
    {
        public SeedItem(Texture2D icon)
        {
            Icon = icon;
            DisplayName = "Seed";
        }

        public override void Use(Player player)
        {
            foreach (GameObject gameObject in GameWorld.Instance.GameObjects)
            {
                Soil soil = gameObject.GetComponent<Soil>();

                if(soil == null)
                {
                    continue;
                }

                Vector2 playerPosition = player.GameObject.Transform.Position;
                Vector2 soilPosition = gameObject.Transform.Position;

                if(Vector2.Distance(playerPosition, soilPosition) < 32)
                {
                    if (soil.CurrentState is PreparedState preparedState)
                    {
                        Debug.WriteLine("Used SeedItem");
                        GameObject crop = CropFactory.Instance.Create(soilPosition);
                        GameWorld.Instance.GameObjects.Add(crop);
                        crop.Start();
                        break; 
                    }
                }
            }
        }
    }
}
