using SproutLands.Classes.ComponentPattern.Objects;
using SproutLands.Classes.StatePattern.SoilState.SoilStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutLands.Classes.Playeren.Tools
{
    public class Hoe : Tool
    {
        public Hoe(Texture2D icon)
        {
            this.Icon = icon;
        }

        public override void Use(Player player)
        {
            Vector2 facing = player.FacingDirection;
            player.SetState(PlayerState.UseHoeDown); // Tilpas efter retning

            Vector2 frontTile = player.GameObject.Transform.Position + facing * 64;

            foreach (var obj in GameWorld.Instance.GameObjects)
            {
                var soil = obj.GetComponent<Soil>();
                if (soil == null)
                    continue;

                var tilePos = obj.Transform.Position;

                if (Vector2.Distance(tilePos, frontTile) < 32)
                {
                    soil.SetState(new PreparedState(PreparedType.Prepared1));
                    Debug.WriteLine("Soil tilled!");
                    break;
                }
            }
        }
    }

}
