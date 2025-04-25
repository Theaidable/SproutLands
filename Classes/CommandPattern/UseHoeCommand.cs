using SproutLands.Classes.Playeren;
using SproutLands.Classes.Playeren.Tools;
using SproutLands.Classes.StatePattern.SoilState;
using SproutLands.Classes.ComponentPattern;
using SproutLands.Classes.StatePattern.SoilState.SoilStates;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using SproutLands.Classes.ComponentPattern.Objects;

namespace SproutLands.Classes.CommandPattern
{
    public class UseHoeCommand : ICommand
    {
        private Player _player;

        public UseHoeCommand(Player player)
        {
            _player = player;
        }

        public void Execute()
        {
            if (_player.EquippedTool is not Hoe)
            {
                Debug.WriteLine("No hoe equipped.");
                return;
            }

            Vector2 facing = _player.FacingDirection;
            _player.SetState(PlayerState.UseHoeDown); // tilpas med animationer

            Vector2 frontTile = _player.GameObject.Transform.Position + facing * 64; // tile foran spilleren

            foreach (var obj in GameWorld.Instance.GameObjects)
            {
                var soil = obj.GetComponent<Soil>();
                if (soil == null)
                    continue;

                var tilePos = obj.Transform.Position;

                if (Vector2.Distance(tilePos, frontTile) < 32) // tæt nok på
                {
                    soil.SetState(new PreparedState(PreparedType.Prepared1));
                    Debug.WriteLine("Soil tilled!");
                    break;
                }
            }
        }
    }
}
