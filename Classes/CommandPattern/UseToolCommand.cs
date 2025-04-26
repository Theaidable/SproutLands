using SproutLands.Classes.Playeren;
using SproutLands.Classes.Playeren.Tools;

using SproutLands.Classes.ComponentPattern.Objects;
using Microsoft.Xna.Framework;
using System.Linq;
using System;
using System.Diagnostics;
using SproutLands.Classes.ComponentPattern.Colliders;
using SproutLands.Classes.ComponentPattern;

namespace SproutLands.Classes.CommandPattern
{
    public class UseToolCommand : ICommand
    {
        private Player _player;

        public UseToolCommand(Player player)
        {
            _player = player;
        }

        public void Execute()
        {
            if (_player.EquippedTool == null)
            {
                Debug.WriteLine("No tool equipped.");
                return;
            }

            _player.EquippedTool.Use(_player);
        }
    }
}
