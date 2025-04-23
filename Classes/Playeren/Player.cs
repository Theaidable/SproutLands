using SproutLands.Classes.ComponentPattern;
using SproutLands.Classes.ObserverPattern;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SproutLands.Classes.ComponentPattern.Items;
using SproutLands.Classes.ComponentPattern.Animation;
using SproutLands.Classes.CommandPattern;
using Microsoft.Xna.Framework.Input;


namespace SproutLands.Classes.Playeren
{
    public enum PlayerState
    {
        IdleUp,
        IdleDown,
        IdleRight,
        IdleLeft,
        WalkingUp,
        WalkingDown,
        WalkingLeft,
        WalkingRight
    }

    public class Player : Component, ISubject
    {
        private Animator _animator;
        private PlayerState _currentState;
        private List<IObserver> observers = new List<IObserver>();
        public Inventory Inventory { get; private set; }

        public Player(GameObject gameObject) : base(gameObject)
        {
            Inventory = new Inventory();
            _animator = gameObject.GetComponent<Animator>();
            _currentState = PlayerState.IdleDown;
            _animator.PlayAnimation(_currentState.ToString());
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

        public void Move(Vector2 direction)
        {
            GameObject.Transform.Translate(direction * 4);
        }

        public void SetState(PlayerState newState)
        {
            if (newState != _currentState)
            {
                _currentState = newState;
                _animator.PlayAnimation(newState.ToString());
            }
        }
    }

}
