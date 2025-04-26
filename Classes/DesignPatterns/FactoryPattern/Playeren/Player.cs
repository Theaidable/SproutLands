using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SproutLands.Classes.DesignPatterns.Command;
using SproutLands.Classes.DesignPatterns.Composite;
using SproutLands.Classes.DesignPatterns.Composite.Components;


namespace SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren
{
    public class Player : Component
    {
        public float MovementSpeed { get; private set; }
        private Vector2 moveDirection;
        private Animator animator;

        //Animationer arrays
        private Texture2D[] walkUpFrames;
        private Texture2D[] walkDownFrames;
        private Texture2D[] walkLeftFrames;
        private Texture2D[] walkRightFrames;

        private Texture2D[] idleUpFrames;
        private Texture2D[] idleDownFrames;
        private Texture2D[] idleLeftFrames;
        private Texture2D[] idleRightFrames;

        public Player(GameObject gameObject): base(gameObject)
        {
            MovementSpeed = 200f;
            moveDirection = Vector2.Zero;
        }

        public override void Start()
        {
            animator = GameObject.GetComponent<Animator>();
            AddAnimations();
            BindCommands();
        }

        public void Move(Vector2 direction)
        {
            moveDirection = direction;
            GameObject.Transform.Translate(direction * MovementSpeed * GameWorld.Instance.DeltaTime);
            PlayWalkAnimation(direction);
        }

        private void AddAnimations()
        {
            // Walking
            walkUpFrames = LoadFrames("Assets/Sprites/Character_Cut/WalkingSprites/Walking_", 2);
            walkDownFrames = LoadFrames("Assets/Sprites/Character_Cut/WalkDown_", 2);
            walkLeftFrames = LoadFrames("Assets/Sprites/Character_Cut/WalkLeft_", 2);
            walkRightFrames = LoadFrames("Assets/Sprites/Character_Cut/WalkRight_", 2);

            // Idle
            idleUpFrames = LoadFrames("Assets/Sprites/Character_Cut/IdleUp_", 2);
            idleDownFrames = LoadFrames("Assets/Sprites/Character_Cut/IdleDown_", 2);
            idleLeftFrames = LoadFrames("Assets/Sprites/Character_Cut/IdleLeft_", 2);
            idleRightFrames = LoadFrames("Assets/Sprites/Character_Cut/IdleRight_", 2);

            // Tilføj animationer til animator
            animator.AddAnimation(new Animation("WalkUp", 8f, true, walkUpFrames));
            animator.AddAnimation(new Animation("WalkDown", 8f, true, walkDownFrames));
            animator.AddAnimation(new Animation("WalkLeft", 8f, true, walkLeftFrames));
            animator.AddAnimation(new Animation("WalkRight", 8f, true, walkRightFrames));

            animator.AddAnimation(new Animation("IdleUp", 1f, true, idleUpFrames));
            animator.AddAnimation(new Animation("IdleDown", 1f, true, idleDownFrames));
            animator.AddAnimation(new Animation("IdleLeft", 1f, true, idleLeftFrames));
            animator.AddAnimation(new Animation("IdleRight", 1f, true, idleRightFrames));
        }

        private Texture2D[] LoadFrames(string basePath, int frameCount)
        {
            Texture2D[] frames = new Texture2D[frameCount];
            for (int i = 0; i < frameCount; i++)
            {
                frames[i] = GameWorld.Instance.Content.Load<Texture2D>($"{basePath}{i}");
            }
            return frames;
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

        private void BindCommands()
        {
            InputHandler.Instance.AddUpdateCommand(Keys.W, new MoveCommand(new Vector2(0, -1), this));
            InputHandler.Instance.AddUpdateCommand(Keys.A, new MoveCommand(new Vector2(-1, 0), this));
            InputHandler.Instance.AddUpdateCommand(Keys.S, new MoveCommand(new Vector2(0, 1), this));
            InputHandler.Instance.AddUpdateCommand(Keys.D, new MoveCommand(new Vector2(1, 0), this));
        }
    }
}
