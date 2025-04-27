
using SharpDX.Direct2D1;
using SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren;

namespace SproutLands.Classes.DesignPatterns.Command
{
    public class UseToolCommand : ICommand
    {
        private readonly Player player;

        public UseToolCommand(Player player)
        {
            this.player = player;
        }

        public void Execute()
        {
            if (player.CanUseTool())
            {
                player.UseEquippedItem();
            }
        }
    }
}
