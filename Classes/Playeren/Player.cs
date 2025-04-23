using SproutLands.Classes.ComponentPattern;
using SproutLands.Classes.ObserverPattern;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SproutLands.Classes.ComponentPattern.Items;


namespace SproutLands.Classes.Playeren
{
    public class Player : Component, ISubject
    {
        private List<IObserver> observers = new();
        public Inventory Inventory { get; private set; }

        public Player(GameObject gameObject) : base(gameObject)
        {
            Inventory = new Inventory();
        }

        public void Attach(IObserver observer) => observers.Add(observer);
        public void Detach(IObserver observer) => observers.Remove(observer);

        public void Notify()
        {
            foreach (var observer in observers)
                observer.Update(this);
        }

        public void AddItemToInventory(Item item)
        {
            Inventory.AddItem(item);
            Notify();
        }
    }

}
