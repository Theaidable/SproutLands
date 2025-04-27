using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.DesignPatterns.Composite;
using SproutLands.Classes.DesignPatterns.FactoryPattern.Crop;
using SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren;
using SproutLands.Classes.DesignPatterns.State.CropState.CropStates;
using SproutLands.Classes.DesignPatterns.State.SoilState.SoilStates;
using SproutLands.Classes.World;
using SproutLands.Classes.World.Tiles;
using System.Diagnostics;


namespace SproutLands.Classes.Items
{
    public class HoeItem : Item
    {
        public HoeItem(Texture2D icon)
        {
            Icon = icon;
        }

        public override void Use(Player player)
        {
            player.PlayUseHoeAnimation();

            foreach (GameObject gameObject in GameWorld.Instance.GameObjects)
            {
                var soil = gameObject.GetComponent<Soil>();

                if (soil == null)
                {
                    continue;
                }

                Vector2 playerPosition = player.GameObject.Transform.Position;
                Vector2 tilePosition = gameObject.Transform.Position;

                if(Vector2.Distance(tilePosition,playerPosition) < 32)
                {
                    soil.SetState(new PreparedState(PreparedType.Prepared1));
                    Debug.WriteLine("Soil tilled");
                    break;
                }
            }

            foreach (GameObject gameObject in GameWorld.Instance.GameObjects)
            {
                var crop = gameObject.GetComponent<Crop>();

                if (crop == null)
                {
                    continue;
                }

                Vector2 playerPosition = player.GameObject.Transform.Position;
                Vector2 cropPosition = gameObject.Transform.Position;

                if (Vector2.Distance(cropPosition, playerPosition) < 32)
                {
                    if (crop.CurrentState is HarvestableState)
                    {
                        crop.Harvest();
                        break; // kun høst én plante ad gangen
                    }
                }
            }
        }
    }
}
