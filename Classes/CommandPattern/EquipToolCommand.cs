using SproutLands.Classes.Playeren;
using SproutLands.Classes.Playeren.Tools;
using SproutLands.Classes.ComponentPattern.Items;
using System.Linq;

namespace SproutLands.Classes.CommandPattern
{
    public class EquipToolCommand : ICommand
    {
        private Player _player;
        private int _inventoryIndex;

        public EquipToolCommand(Player player, int inventoryIndex)
        {
            _player = player;
            _inventoryIndex = inventoryIndex;
        }

        public void Execute()
        {
            var item = _player.Inventory.Items.ElementAtOrDefault(_inventoryIndex);
            if (item is Tool tool)
            {
                _player.EquipTool(tool);
                System.Console.WriteLine($"Equipped tool in slot {_inventoryIndex + 1}: {tool.GetType().Name}");
            }
            else
            {
                _player.EquipTool(null);
                System.Console.WriteLine($"Slot {_inventoryIndex + 1} is empty or not a tool.");
            }
        }
    }
}
