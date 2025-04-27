using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.DesignPatterns.Composite;
using SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren;
using SproutLands.Classes.World;
using SproutLands.Classes.World.Tiles;
using SproutLands.Classes.World.Tiles.SoilStates;
using System.Diagnostics;


namespace SproutLands.Classes.Items
{
    public class Hoe : Item
    {
        public Hoe(Texture2D icon)
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
        }
    }
}
