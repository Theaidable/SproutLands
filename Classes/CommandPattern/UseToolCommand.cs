using SproutLands.Classes.Playeren;
using SproutLands.Classes.Playeren.Tools;
using SproutLands.Classes.ComponentPattern;
using SproutLands.Classes.ComponentPattern.Items;
using SproutLands.Classes.ComponentPattern.Objects;
using Microsoft.Xna.Framework;
using System.Linq;
using System;
using System.Diagnostics;

namespace SproutLands.Classes.CommandPattern
{
    public class UseToolCommand : ICommand
    {
        private Player _player;
        private float maxDistance = 100f;

        public UseToolCommand(Player player)
        {
            _player = player;
        }

        public void Execute()
        {
            if (_player.EquippedTool is not Axe)
            {
                Debug.WriteLine("No axe equipped.");
                return;
            }

            Vector2 playerCenter = _player.GameObject.Transform.Position + new Vector2(32, 32); // antager 64x64 sprite

            var tree = GameWorld.Instance.GameObjects
                .Select(go => go.GetComponent<Tree>())
                .Where(t => t != null)
                .OrderBy(t =>
                {
                    Vector2 treeCenter = t.GameObject.Transform.Position + new Vector2(32, 32);
                    return Vector2.Distance(playerCenter, treeCenter);
                })
                .FirstOrDefault(t =>
                {
                    Vector2 treeCenter = t.GameObject.Transform.Position + new Vector2(32, 32);
                    float distance = Vector2.Distance(playerCenter, treeCenter);
                    Debug.WriteLine($"Distance to tree: {distance}");
                    return distance <= maxDistance;
                });

            if (tree != null)
            {
                tree.TakeDamage(50);
                Debug.WriteLine("Tree chopped!");
            }
            else
            {
                Debug.WriteLine("No tree in range.");
            }
        }
    }
}
