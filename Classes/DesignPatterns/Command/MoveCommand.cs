using Microsoft.Xna.Framework;
using SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren;

namespace SproutLands.Classes.DesignPatterns.Command
{
    public class MoveCommand : ICommand
    {
        private Vector2 direction;
        private Player player;

        public MoveCommand(Vector2 direction, Player player)
        {
            this.direction = direction;
            this.player = player;
        }

        public void Execute()
        {
            player.Move(direction);
        }
    }
}
