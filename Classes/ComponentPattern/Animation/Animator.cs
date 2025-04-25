using System.Collections.Generic;

namespace SproutLands.Classes.ComponentPattern.Animation
{
    public class Animator : Component
    {
        public int CurrentIndex { get; private set; }
        public Animation CurrentAnimation { get => currentAnimation; set => currentAnimation = value; }

        private float elapsed;
        private SpriteRenderer spriteRenderer;
        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        private Animation currentAnimation;
        public Animator(GameObject gameObject) : base(gameObject)
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        public override void Start()
        {
            if (CurrentAnimation != null)
            {
                elapsed = 0f;
                CurrentIndex = 0;
                spriteRenderer.Sprite = CurrentAnimation.SpriteSheet;
                spriteRenderer.SourceRectangle = CurrentAnimation.Frames[0];
            }
        }

        public override void Update()
        {
            if(CurrentAnimation == null)
            {
                return;
            }

            elapsed += GameWorld.Instance.DeltaTime;

            CurrentIndex = (int)(elapsed * CurrentAnimation.FPS % CurrentAnimation.Frames.Length);

            spriteRenderer.SourceRectangle = CurrentAnimation.Frames[CurrentIndex];
        }
        public void AddAnimation(Animation animation)
        {
            animations[animation.Name] = animation;
            if(CurrentAnimation == null)
            {
                CurrentAnimation = animation;
            }
        }
        public void PlayAnimation(string animationName)
        {
            if(CurrentAnimation?.Name == animationName)
            {
                return;
            }

            CurrentAnimation = animations[animationName];
            spriteRenderer.Sprite = CurrentAnimation.SpriteSheet;
            elapsed = 0;
        }
    }
}
