using SproutLands.Classes.Playeren;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace SproutLands.Classes.CommandPattern
{
    public class MoveCommand : ICommand
    {
        private Player _player;
        private Vector2 _direction;

        public MoveCommand(Player player, Vector2 direction)
        {
            this._player = player;
            this._direction = direction;
        }

        public void Execute()
        {
            _player.Move(_direction);

            Debug.WriteLine($"[UseTool] FacingDirection = {_player.FacingDirection}");

            if (_direction == new Vector2(0, -1))
            {
                _player.SetState(PlayerState.WalkingUp);
            }
            else if (_direction == new Vector2(0, 1))
            {
                _player.SetState(PlayerState.WalkingDown);
            }
            else if (_direction == new Vector2(-1, 0))
            {
                _player.SetState(PlayerState.WalkingLeft);
            }
            else if (_direction == new Vector2(1, 0))
            {
                _player.SetState(PlayerState.WalkingRight);
            }
        }
    }
}
