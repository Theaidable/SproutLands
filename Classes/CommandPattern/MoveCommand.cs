using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SproutLands.Classes.CommandPattern
{
    public class MoveCommand : ICommand
    {
        private Player.Player _player;
        private Vector2 _direction;

        public MoveCommand(Player.Player player, Vector2 direction)
        {
            _player = player;
            _direction = direction;
        }

        public void Execute()
        {
            _player.Move(_direction);
        }
    }
}
