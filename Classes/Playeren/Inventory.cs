
using SproutLands.Classes.ObserverPattern;
using System.Collections.Generic;
using SproutLands.Classes.ComponentPattern.Items;

namespace SproutLands.Classes.Playeren
{
    public class Inventory : IObserver
    {
        //Vi skal bruge en liste over items
        public List<Item> Items { get;private set; } = new List<Item>();

        /// <summary>
        /// Metode til at tilføje en item til inventory
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void Update(ISubject subject)
        {
            if(subject is Player player)
            {
                Items = player.Inventory.Items;
            }
        }
    }
}
