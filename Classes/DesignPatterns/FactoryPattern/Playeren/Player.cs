using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SproutLands.Classes.DesignPatterns.Command;
using SproutLands.Classes.DesignPatterns.Composite;
using SproutLands.Classes.DesignPatterns.Composite.Components;
using SproutLands.Classes.World;


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
            animator.PlayAnimation("IdleDown");
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
            walkUpFrames = LoadFrames("Assets/CharacterSprites/WalkingSprites/WalkingUp", 2);
            walkDownFrames = LoadFrames("Assets/CharacterSprites/WalkingSprites/WalkingDown", 2);
            walkLeftFrames = LoadFrames("Assets/CharacterSprites/WalkingSprites/WalkingLeft", 2);
            walkRightFrames = LoadFrames("Assets/CharacterSprites/WalkingSprites/WalkingRight", 2);

            // Idle
            idleUpFrames = LoadFrames("Assets/CharacterSprites/IdleSprites/IdleUp", 2);
            idleDownFrames = LoadFrames("Assets/CharacterSprites/IdleSprites/IdleDown", 2);
            idleLeftFrames = LoadFrames("Assets/CharacterSprites/IdleSprites/IdleLeft", 2);
            idleRightFrames = LoadFrames("Assets/CharacterSprites/IdleSprites/IdleRight", 2);

            // Tilføj animationer til animator
            animator.AddAnimation(new Animation("WalkUp", 2.5f, true, walkUpFrames));
            animator.AddAnimation(new Animation("WalkDown", 2.5f, true, walkDownFrames));
            animator.AddAnimation(new Animation("WalkLeft", 2.5f, true, walkLeftFrames));
            animator.AddAnimation(new Animation("WalkRight", 2.5f, true, walkRightFrames));

            animator.AddAnimation(new Animation("IdleUp", 2.5f, true, idleUpFrames));
            animator.AddAnimation(new Animation("IdleDown", 2.5f, true, idleDownFrames));
            animator.AddAnimation(new Animation("IdleLeft", 2.5f, true, idleLeftFrames));
            animator.AddAnimation(new Animation("IdleRight", 2.5f, true, idleRightFrames));
        }

        private Texture2D[] LoadFrames(string basePath, int frameCount)
        {
            Texture2D[] frames = new Texture2D[frameCount];
            for (int i = 0; i < frameCount; i++)
            {
                frames[i] = GameWorld.Instance.Content.Load<Texture2D>($"{basePath}_00{i}");
            }
            return frames;
        }

        private void PlayWalkAnimation(Vector2 direction)
        {
            if(direction.Y < 0)
            {
                animator.PlayAnimation("WalkUp");
            }
            else if(direction.Y > 0)
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
            else if(direction.Y > 0)
            {
                animator.PlayAnimation("IdleDown");
            }
            else if(direction.X < 0)
            {
                animator.PlayAnimation("IdleLeft");
            }
            else if(direction.X > 0)
            {
                animator.PlayAnimation("IdleRight");
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

            InputHandler.Instance.AddButtonUpCommand(Keys.W, new StopCommand(this));
            InputHandler.Instance.AddButtonUpCommand(Keys.A, new StopCommand(this));
            InputHandler.Instance.AddButtonUpCommand(Keys.S, new StopCommand(this));
            InputHandler.Instance.AddButtonUpCommand(Keys.D, new StopCommand(this));
        }
    }
}
