using SproutLands.Classes.Playeren;
using SproutLands.Classes.Playeren.Tools;
using SproutLands.Classes.ComponentPattern;
using SproutLands.Classes.ComponentPattern.Items;
using SproutLands.Classes.ComponentPattern.Objects;
using Microsoft.Xna.Framework;
using System.Linq;
using System;

namespace SproutLands.Classes.CommandPattern
{
    public class UseToolCommand : ICommand
    {
        private Player _player;
        private GameObject _target;

        public UseToolCommand(Player player, GameObject target)
        {
            _player = player;
            _target = target;
        }

        public void Execute()
        {
            Tree tree = _target.GetComponent<Tree>();

            if (tree == null || tree.IsChopped)
            {
                Console.WriteLine("Ingen træ fundet.");
                return;
            }

            if (_player.HasAxe())
            {
                tree.TakeDamage(50); // fx. et hug = 50 dmg
                if (tree.IsChopped)
                {
                    for (int i = 0; i < tree.ResourceAmount; i++)
                    {
                        _player.AddItemToInventory(new WoodItem());
                    }
                }
            }
            else
            {
                Console.WriteLine("Du har ikke en økse!");
            }
        }
    }
}
