using SproutLands.Classes.UI;

namespace SproutLands.Classes.DesignPatterns.Command
{
    public class OpenInventoryCommand : ICommand
    {
        private readonly Inventory inventory;
        private readonly Hudbar hudbar;

        public OpenInventoryCommand(Inventory inventory)
        {
            this.inventory = inventory;
        }

        public void Execute()
        {
            if (inventory != null)
            {
                inventory.ToggleInventory();
            }
        }
    }
}
