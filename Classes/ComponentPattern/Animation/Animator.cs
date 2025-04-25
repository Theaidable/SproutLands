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
            if(currentAnimation == null)
            {
                return;
            }

            elapsed += GameWorld.Instance.DeltaTime;

            CurrentIndex = (int)(elapsed * currentAnimation.FPS % currentAnimation.Frames.Length);

            spriteRenderer.SourceRectangle = currentAnimation.Frames[CurrentIndex];
        }
        public void AddAnimation(Animation animation)
        {
            animations[animation.Name] = animation;
            if(currentAnimation == null)
            {
                currentAnimation = animation;
            }
        }
        public void PlayAnimation(string animationName)
        {
            if(currentAnimation?.Name == animationName)
            {
                return;
            }

            currentAnimation = animations[animationName];
            spriteRenderer.Sprite = currentAnimation.SpriteSheet;
            elapsed = 0;
        }
    }
}
