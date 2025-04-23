using SproutLands.Classes.ComponentPattern.Items;
using SproutLands.Classes.ObserverPattern;
using SproutLands.Classes.Playeren;
using System.Collections.Generic;


namespace SproutLands.Classes.UI
{
    public class UI : IObserver
    {
        private Player player;

        public UI(Player player)
        {
            this.player = player;
            player.Attach(this);
        }

        public void Update(ISubject subject)
        {
            if(subject is Player player)
            {
                RefreshHudbar(player.Inventory.Items);
            }
        }

        private void RefreshHudbar(List<Item> items)
        {
            //Opdatere visuals i hudbar ud fra items
        }
    }
}
