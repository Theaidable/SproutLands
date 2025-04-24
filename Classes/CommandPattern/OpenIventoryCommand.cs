
using SproutLands.Classes.UIClasses;

namespace SproutLands.Classes.CommandPattern
{
    public class OpenInventoryCommand : ICommand
    {
        private readonly UI _ui;
        public OpenInventoryCommand(UI ui) => _ui = ui;
        public void Execute()
        {
            _ui.ToggleInventory();
        }
    }
}
