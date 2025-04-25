using SproutLands.Classes.Playeren;
using SproutLands.Classes.Playeren.Tools;

using SproutLands.Classes.ComponentPattern.Objects;
using Microsoft.Xna.Framework;
using System.Linq;
using System;
using System.Diagnostics;
using SproutLands.Classes.ComponentPattern.Colliders;
using SproutLands.Classes.ComponentPattern;

namespace SproutLands.Classes.CommandPattern
{
    public class UseToolCommand : ICommand
    {
        private Player _player;
        private Vector2 _direction;
        private int _damage = 50;
        private bool hit;

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

            var facing = _player.FacingDirection;

            if (facing == new Vector2(0, -1))
                _player.SetState(PlayerState.UseAxeUp);
            else if (facing == new Vector2(0, 1))
                _player.SetState(PlayerState.UseAxeDown);
            else if (facing == new Vector2(-1, 0))
                _player.SetState(PlayerState.UseAxeLeft);
            else if (facing == new Vector2(1, 0))
                _player.SetState(PlayerState.UseAxeRight);

            UseAxe(facing);
        }

        public void UseAxe(Vector2 direction)
        {
            var playerCollider = _player.GameObject.GetComponent<Collider>();
            if (playerCollider == null)
            {
                Debug.WriteLine("Player has no collider!");
                return;
            }

            foreach (GameObject gameObject in GameWorld.Instance.GameObjects)
            {
                Tree tree = gameObject.GetComponent<Tree>();
                Collider treeCollider = gameObject.GetComponent<Collider>();

                if(tree == null || treeCollider == null)
                {
                    continue;
                }

                if (!playerCollider.CollisionBox.Intersects(treeCollider.CollisionBox))
                {
                    continue;
                }

                hit = playerCollider.PixelPerfectRectangles.Any(pr => treeCollider.PixelPerfectRectangles.Any(tr => pr.Rectangle.Intersects(tr.Rectangle)));

                if(hit == true)
                {
                    tree.TakeDamage(_damage);
                    Debug.WriteLine($"Tree chopped! Damage: {_damage}");
                    break;
                }
                else
                {
                    Debug.WriteLine("No tree in collision range.");
                }
            }
        }
    }
}
