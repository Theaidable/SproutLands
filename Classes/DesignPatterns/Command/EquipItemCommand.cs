
using SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren;
using SproutLands.Classes.UI;

namespace SproutLands.Classes.DesignPatterns.Command
{
    public class EquipItemCommand : ICommand
    {
        private readonly Player player;
        private readonly int slotIndex;

        public EquipItemCommand(Player player, int slotIndex)
        {
            this.player = player;
            this.slotIndex = slotIndex;
        }

        public void Execute()
        {
            Hudbar hudbar = player.GameObject.GetComponent<Hudbar>();

            if(hudbar != null && slotIndex >= 0 && slotIndex < hudbar.HudSlots.Count)
            {
                ItemSlot slot = hudbar.HudSlots[slotIndex];

                if(slot.StoredItem != null)
                {
                    player.EquipItem(slot.StoredItem);
                }
            }
        }
    }
}
