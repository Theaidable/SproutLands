using SproutLands.Classes.ComponentPattern;
using SproutLands.Classes.ObserverPattern;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SproutLands.Classes.ComponentPattern.Items;
using SproutLands.Classes.ComponentPattern.Animation;
using SproutLands.Classes.CommandPattern;
using Microsoft.Xna.Framework.Input;
using SproutLands.Classes.Playeren.Tools;
using System.Linq;
using System;
using SproutLands.Classes.UIClasses;


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
        WalkingRight,
        UseAxeUp,
        UseAxeDown,
        UseAxeLeft,
        UseAxeRight,
        UseHoeUp,
        UseHoeDown,
        UseHoeLeft,
        UseHoeRight,
    }


    public class Player : Component, ISubject
    {
        private Animator _animator;
        private PlayerState _currentState;
        private List<IObserver> observers = new List<IObserver>();
        public Inventory Inventory { get; private set; }
        public Tool EquippedTool { get; private set; }
        public Vector2 FacingDirection { get; set; }

        public Player(GameObject gameObject) : base(gameObject)
        {
            Inventory = new Inventory();
            _animator = gameObject.GetComponent<Animator>();
            SetState(PlayerState.IdleDown);
        }

        public void Attach(IObserver observer) => observers.Add(observer);
        public void Detach(IObserver observer) => observers.Remove(observer);

        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Update(this);
            }
        }

        public bool HasAxe()
        {
            return Inventory.Items.Any(item => item is Axe);
        }

        public void EquipTool(Tool tool)
        {
            EquippedTool = tool;

            if (tool != null)
            {
                System.Diagnostics.Debug.WriteLine($"Equipped tool: {tool.GetType().Name}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Unequipped tool.");
            }
        }

        public void AddItemToInventory(Item item)
        {
            Inventory.AddItem(item);
            Notify();
        }

        public void AddItemToHud(int slotIndex, Item item)
        {
            if (slotIndex >= 0 && slotIndex < Inventory.HudSlots.Count)
            {
                Inventory.HudSlots[slotIndex] = item;
                Notify(); // Opdater UI
            }
        }

        public void Move(Vector2 direction)
        {
            GameObject.Transform.Translate(direction * 4);
        }

        public void SetState(PlayerState newState)
        {
            bool isUseState = newState.ToString().StartsWith("Use");

            if (newState != _currentState || isUseState)
            {
                _currentState = newState;

                // Opdatér retningen når du går eller bruger øksen:
                switch (newState)
                {
                    case PlayerState.WalkingUp: FacingDirection = new Vector2(0, -1); break;
                    case PlayerState.IdleUp: FacingDirection = new Vector2(0, -1); break;
                    case PlayerState.UseAxeUp: FacingDirection = new Vector2(0, -1); break;
                    case PlayerState.WalkingDown: FacingDirection = new Vector2(0, 1); break;
                    case PlayerState.IdleDown: FacingDirection = new Vector2(0, 1); break;
                    case PlayerState.UseAxeDown: FacingDirection = new Vector2(0, 1); break;
                    case PlayerState.WalkingLeft: FacingDirection = new Vector2(-1, 0); break;
                    case PlayerState.IdleLeft: FacingDirection = new Vector2(-1, 0); break;
                    case PlayerState.UseAxeLeft: FacingDirection = new Vector2(-1, 0); break;
                    case PlayerState.WalkingRight: FacingDirection = new Vector2(1, 0); break;
                    case PlayerState.IdleRight: FacingDirection = new Vector2(1, 0); break;
                    case PlayerState.UseAxeRight: FacingDirection=new Vector2(1, 0); break;
                }

                _animator.PlayAnimation(newState.ToString());
            }
        }
    }

}
