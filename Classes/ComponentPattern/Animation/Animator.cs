using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace SproutLands.Classes.ComponentPattern.Animation
{
    public class Animator : Component
    {
        public int CurrentIndex { get; private set; }
        private float elapsed;
        private SpriteRenderer spriteRenderer;
        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        private Animation currentAnimation;
        public Animator(GameObject gameObject) : base(gameObject)
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        public override void Update()
        {
            elapsed += GameWorld.Instance.DeltaTime;
            CurrentIndex = (int)(elapsed * currentAnimation.FPS);

            if (CurrentIndex > currentAnimation.Sprites.Length - 1)
            {
                elapsed = 0;
                CurrentIndex = 0;
            }
            spriteRenderer.Sprite = currentAnimation.Sprites[CurrentIndex];
        }
        public void AddAnimation(Animation animation)
        {
            animations.Add(animation.Name, animation);
            if (currentAnimation == null)
            {
                currentAnimation = animation;
            }
        }
        public void PlayAnimation(string animationName)
        {
            if (animationName != currentAnimation.Name)
            {
                currentAnimation = animations[animationName];
                elapsed = 0;
                CurrentIndex = 0;
            }
        }
    }
}
