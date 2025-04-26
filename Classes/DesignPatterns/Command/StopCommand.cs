using SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren;

namespace SproutLands.Classes.DesignPatterns.Command
{
    public class StopCommand : ICommand
    {
        private Player player;

        public StopCommand(Player player)
        {
            this.player = player;
        }

        public void Execute()
        {
            player.Stop();
        }
    }
}
