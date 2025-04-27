using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SproutLands.Classes.DesignPatterns.Command;
using SproutLands.Classes.DesignPatterns.Composite;
using SproutLands.Classes.DesignPatterns.Composite.Components;
using SproutLands.Classes.Items;
using SproutLands.Classes.UI;
using SproutLands.Classes.World;
using System.Diagnostics;


namespace SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren
{
    public class Player : Component
    {
        private Animator animator;
        private Inventory inventory;
        private Vector2 moveDirection;
        private float toolCooldown = 0.1f;
        private float cooldownTimer = 0f;
        private bool isUsingItem = false;

        //Walk animation frames
        private Texture2D[] walkUpFrames;
        private Texture2D[] walkDownFrames;
        private Texture2D[] walkLeftFrames;
        private Texture2D[] walkRightFrames;

        //Idle animation frames
        private Texture2D[] idleUpFrames;
        private Texture2D[] idleDownFrames;
        private Texture2D[] idleLeftFrames;
        private Texture2D[] idleRightFrames;

        //Use axe animation frames
        private Texture2D[] useAxeUpFrames;
        private Texture2D[] useAxeDownFrames;
        private Texture2D[] useAxeLeftFrames;
        private Texture2D[] useAxeRightFrames;

        //Use hoe animation frames
        private Texture2D[] useHoeUpFrames;
        private Texture2D[] useHoeDownFrames;
        private Texture2D[] useHoeLeftFrames;
        private Texture2D[] useHoeRightFrames;

        public float MovementSpeed { get; private set; }
        public Item EquippedItem { get; private set; }

        public Player(GameObject gameObject): base(gameObject)
        {
            MovementSpeed = 200f;
            moveDirection = Vector2.One;
        }

        public override void Start()
        {
            animator = GameObject.GetComponent<Animator>();
            inventory = GameObject.GetComponent<Inventory>();
            AddAnimations();
            BindCommands();
            animator.PlayAnimation("IdleDown");
        }

        public override void Update()
        {
            if(cooldownTimer > 0)
            {
                cooldownTimer -= GameWorld.Instance.DeltaTime;
            }
        }

        public void Move(Vector2 direction)
        {
            if (isUsingItem)
            {
                Debug.WriteLine("Annullerer brug af item pga. bevægelse.");
                isUsingItem = false;
                animator.ClearOnAnimationComplete();
                Stop();
            }

            moveDirection = direction;
            GameObject.Transform.Translate(direction * MovementSpeed * GameWorld.Instance.DeltaTime);
            PlayWalkAnimation(direction);
        }

        public void EquipItem(Item item)
        {
            if (isUsingItem)
            {
                Debug.WriteLine("Kan ikke skifte item mens vi bruger et item!");
                return;
            }

            EquippedItem = item;
            Debug.WriteLine($"Equipped item: {item.GetType().Name}");
        }

        public bool CanUseTool()
        {
            return cooldownTimer <= 0f;
        }

        public void UseEquippedItem()
        {
            if (EquippedItem != null && cooldownTimer <= 0f && !isUsingItem)
            {
                animator.ClearOnAnimationComplete();
                isUsingItem = true;

                if (EquippedItem is AxeItem)
                {
                    PlayUseAxeAnimation();
                }
                else if (EquippedItem is HoeItem)
                {
                    PlayUseHoeAnimation();
                }
                else
                {
                    EquippedItem.Use(this);
                    cooldownTimer = toolCooldown;
                    isUsingItem = false;
                    return;
                }

                animator.OnAnimationComplete = () =>
                {
                    EquippedItem.Use(this);
                    cooldownTimer = toolCooldown;
                    Stop();
                    isUsingItem = false;
                };
            }
        }
            

        private void AddAnimations()
        {
            //Walking
            walkUpFrames = LoadFrames("Assets/CharacterSprites/WalkingSprites/WalkingUp", 2);
            walkDownFrames = LoadFrames("Assets/CharacterSprites/WalkingSprites/WalkingDown", 2);
            walkLeftFrames = LoadFrames("Assets/CharacterSprites/WalkingSprites/WalkingLeft", 2);
            walkRightFrames = LoadFrames("Assets/CharacterSprites/WalkingSprites/WalkingRight", 2);

            //Idle
            idleUpFrames = LoadFrames("Assets/CharacterSprites/IdleSprites/IdleUp", 2);
            idleDownFrames = LoadFrames("Assets/CharacterSprites/IdleSprites/IdleDown", 2);
            idleLeftFrames = LoadFrames("Assets/CharacterSprites/IdleSprites/IdleLeft", 2);
            idleRightFrames = LoadFrames("Assets/CharacterSprites/IdleSprites/IdleRight", 2);

            //Use Axe
            useAxeUpFrames = LoadFrames("Assets/CharacterSprites/ActionSprites/Axe/AxeUp", 2);
            useAxeDownFrames = LoadFrames("Assets/CharacterSprites/ActionSprites/Axe/AxeDown", 2);
            useAxeLeftFrames = LoadFrames("Assets/CharacterSprites/ActionSprites/Axe/AxeLeft", 2);
            useAxeRightFrames = LoadFrames("Assets/CharacterSprites/ActionSprites/Axe/AxeRight", 2);

            //Use Hoe
            useHoeUpFrames = LoadFrames("Assets/CharacterSprites/ActionSprites/Hoe/HoeUp", 2);
            useHoeDownFrames = LoadFrames("Assets/CharacterSprites/ActionSprites/Hoe/HoeDown", 2);
            useHoeLeftFrames = LoadFrames("Assets/CharacterSprites/ActionSprites/Hoe/HoeLeft", 2);
            useHoeRightFrames = LoadFrames("Assets/CharacterSprites/ActionSprites/Hoe/HoeRight", 2);

            //Tilføj walking animationer
            animator.AddAnimation(new Animation("WalkUp", 2.5f, true, walkUpFrames));
            animator.AddAnimation(new Animation("WalkDown", 2.5f, true, walkDownFrames));
            animator.AddAnimation(new Animation("WalkLeft", 2.5f, true, walkLeftFrames));
            animator.AddAnimation(new Animation("WalkRight", 2.5f, true, walkRightFrames));

            //Tilføj idle animationer
            animator.AddAnimation(new Animation("IdleUp", 2.5f, true, idleUpFrames));
            animator.AddAnimation(new Animation("IdleDown", 2.5f, true, idleDownFrames));
            animator.AddAnimation(new Animation("IdleLeft", 2.5f, true, idleLeftFrames));
            animator.AddAnimation(new Animation("IdleRight", 2.5f, true, idleRightFrames));

            //Tilføj use axe animationer
            animator.AddAnimation(new Animation("UseAxeUp", 5f, false, useAxeUpFrames));
            animator.AddAnimation(new Animation("UseAxeDown", 5f, false, useAxeDownFrames));
            animator.AddAnimation(new Animation("UseAxeLeft", 5f, false, useAxeLeftFrames));
            animator.AddAnimation(new Animation("UseAxeRight", 5f, false, useAxeRightFrames));

            //Tilføj use hoe animationer
            animator.AddAnimation(new Animation("UseHoeUp", 5f, false, useHoeUpFrames));
            animator.AddAnimation(new Animation("UseHoeDown", 5f, false, useHoeDownFrames));
            animator.AddAnimation(new Animation("UseHoeLeft", 5f, false, useHoeLeftFrames));
            animator.AddAnimation(new Animation("UseHoeRight", 5f, false, useHoeRightFrames));
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

        private void PlayUseToolAnimation(string baseAnimationName)
        {
            animator.ClearOnAnimationComplete();

            if (moveDirection.Y < 0)
            {
                animator.PlayAnimation($"{baseAnimationName}Up");
            }
            else if (moveDirection.Y > 0)
            {
                animator.PlayAnimation($"{baseAnimationName}Down");
            }
            else if (moveDirection.X < 0)
            {
                animator.PlayAnimation($"{baseAnimationName}Left");
            }
            else if (moveDirection.X > 0)
            {
                animator.PlayAnimation($"{baseAnimationName}Right");
            }

            animator.OnAnimationComplete = Stop;
        }

        public void PlayUseAxeAnimation()
        {
            PlayUseToolAnimation("UseAxe");
        }

        public void PlayUseHoeAnimation()
        {
            PlayUseToolAnimation("UseHoe");
        }

        public void Stop()
        {
            PlayIdleAnimation(moveDirection);
        }

        private void BindCommands()
        {
            //Bevæg spilleren
            InputHandler.Instance.AddUpdateCommand(Keys.W, new MoveCommand(new Vector2(0, -1), this));
            InputHandler.Instance.AddUpdateCommand(Keys.A, new MoveCommand(new Vector2(-1, 0), this));
            InputHandler.Instance.AddUpdateCommand(Keys.S, new MoveCommand(new Vector2(0, 1), this));
            InputHandler.Instance.AddUpdateCommand(Keys.D, new MoveCommand(new Vector2(1, 0), this));

            //Gå til idle hvis man stopper med at gå
            InputHandler.Instance.AddButtonUpCommand(Keys.W, new StopCommand(this));
            InputHandler.Instance.AddButtonUpCommand(Keys.A, new StopCommand(this));
            InputHandler.Instance.AddButtonUpCommand(Keys.S, new StopCommand(this));
            InputHandler.Instance.AddButtonUpCommand(Keys.D, new StopCommand(this));

            //Åben og luk inventory
            InputHandler.Instance.AddButtonDownCommand(Keys.I, new OpenInventoryCommand(inventory));

            //Equip items fra hudbar
            InputHandler.Instance.AddButtonDownCommand(Keys.D1, new EquipItemCommand(this, 0));
            InputHandler.Instance.AddButtonDownCommand(Keys.D2, new EquipItemCommand(this, 1));
            InputHandler.Instance.AddButtonDownCommand(Keys.D3, new EquipItemCommand(this, 2));
            InputHandler.Instance.AddButtonDownCommand(Keys.D4, new EquipItemCommand(this, 3));
            InputHandler.Instance.AddButtonDownCommand(Keys.D5, new EquipItemCommand(this, 4));
            InputHandler.Instance.AddButtonDownCommand(Keys.D6, new EquipItemCommand(this, 5));
            InputHandler.Instance.AddButtonDownCommand(Keys.D7, new EquipItemCommand(this, 6));
            InputHandler.Instance.AddButtonDownCommand(Keys.D8, new EquipItemCommand(this, 7));
            InputHandler.Instance.AddButtonDownCommand(Keys.D9, new EquipItemCommand(this, 8));

            //Brug item
            InputHandler.Instance.AddMouseButtonDownCommand(MouseButton.Left, new UseToolCommand(this));
        }
    }
}
