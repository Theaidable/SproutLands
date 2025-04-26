using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.DesignPatterns.Composite;
using SproutLands.Classes.DesignPatterns.Composite.Components;


namespace SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren
{
    public class Player : Component
    {
        public float MovementSpeed { get; private set; }
        private Vector2 moveDirection;
        private Animator animator;

        public Player(GameObject gameObject): base(gameObject)
        {
            MovementSpeed = 200f;
            moveDirection = Vector2.Zero;
        }

        public override void Start()
        {
            animator = GameObject.GetComponent<Animator>();
            AddAnimations();
           
        }

        public void Move(Vector2 direction)
        {
            moveDirection = direction;
            GameObject.Transform.Translate(direction * MovementSpeed * GameWorld.Instance.DeltaTime);
            PlayWalkAnimation(direction);
        }

        private void AddAnimations()
        {
            Texture2D[] walkingFrames = new Texture2D[8];
            Texture2D[] idleFrames = new Texture2D[8];

            for (int i = 0; i < walkingFrames.Length; i++)
            {
                walkingFrames[i] = GameWorld.Instance.Content.Load<Texture2D>($"Assets/Sprites/Character_Cut/");
            }

            animator.AddAnimation(new Animation("WalkUp", 2.5f, true, walkingFrames));
        }

        private void PlayWalkAnimation(Vector2 direction)
        {
            if(direction.Y < 0)
            {
                animator.PlayAnimation("WalkUp");
            }
            else if(direction.Y > 1)
            {
                animator.PlayAnimation("WalkDown");
            }
            else if(direction.X < 0)
            {
                animator.PlayAnimation("WalkLeft");
            }
            else if(direction.X > 0)
            {
                animator.PlayAnimation("WalkRight");
            }
        }

        private void PlayIdleAnimation(Vector2 direction)
        {
            if (direction.Y < 0)
            {
                animator.PlayAnimation("IdleUp");
            }
            else if(direction.Y > 1)
            {
                animator.PlayAnimation("IdleDown");
            }
            else if(direction.X < 0)
            {
                animator.PlayAnimation("IdleLeft");
            }
            else if(direction.X > 0)
            {
                animator.PlayAnimation("ÍdleRight");
            }
        }

        public void Stop()
        {
            PlayIdleAnimation(moveDirection);
        }
    }
}
