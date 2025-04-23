using SproutLands.Classes.Playeren;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutLands.Classes.CommandPattern
{
    public class IdleCommand : ICommand
    {
        private readonly Player _player;
        private readonly PlayerState _idleState;

        public IdleCommand(Player player, PlayerState idleState)
        {
            _player = player;
            _idleState = idleState;
        }

        public void Execute()
        {
            _player.SetState(_idleState);
        }
    }
}
