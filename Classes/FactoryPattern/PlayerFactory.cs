using SproutLands.Classes.ComponentPattern.Colliders;
using SproutLands.Classes.ComponentPattern.Objects;
using SproutLands.Classes.ComponentPattern;
using SproutLands.Classes.CommandPattern;
using SproutLands.Classes.ComponentPattern.Animation;
using SproutLands.Classes.Playeren;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SproutLands.Classes.UIClasses;
using System.ComponentModel.DataAnnotations;

namespace SproutLands.Classes.FactoryPattern
{
    public class PlayerFactory : Factory
    {
        //Oprettelse af Singleton for PlayerFactory
        private static PlayerFactory instance;
        public static PlayerFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayerFactory();
                }
                return instance;
            }
        }

        private PlayerFactory() { }

        public override GameObject Create(Vector2 position)
        {
            var playerObject = new GameObject();
            playerObject.Transform.Position = position;

            var sr = playerObject.AddComponent<SpriteRenderer>();
            var animator = playerObject.AddComponent<Animator>();
            var playerSheet = GameWorld.Instance.Content.Load<Texture2D>("Assets/Sprites/Characters/Charakter");
            var useToolSheet = GameWorld.Instance.Content.Load<Texture2D>("Assets/Sprites/Characters/Charakter_Actions");


            int frameWidth = 128;
            int frameHeight = 128;
            var initialRect = new Rectangle(0, 0, frameWidth, frameHeight);
            sr.SetSprite(playerSheet.ToString(), initialRect);

            //IdleDown animation
            animator.AddAnimation(new Animation(
                PlayerState.IdleDown.ToString(),
                playerSheet,
                new Rectangle[]
                    {
                    new Rectangle(0 * frameWidth, 0 * frameHeight, frameWidth, frameHeight),
                    new Rectangle(1 * frameWidth, 0 * frameHeight, frameWidth, frameHeight),
                    },
                2.5f
                ));

            //WalkingDown Animation
            animator.AddAnimation(new Animation(
                PlayerState.WalkingDown.ToString(),
                playerSheet,
                new Rectangle[]
                    {
                    new Rectangle(2 * frameWidth, 0 * frameHeight, frameWidth, frameHeight),
                    new Rectangle(3 * frameWidth, 0 * frameHeight, frameWidth, frameHeight),
                    },
                2.5f
                ));

            //IdleUp Animation
            animator.AddAnimation(new Animation(
                PlayerState.IdleUp.ToString(),
                playerSheet,
                new Rectangle[]
                    {
                    new Rectangle(0 * frameWidth, 1 * frameHeight, frameWidth, frameHeight),
                    new Rectangle(1 * frameWidth, 1 * frameHeight, frameWidth, frameHeight),
                    },
                2.5f
                ));

            //WalkingUp Animation
            animator.AddAnimation(new Animation(
                PlayerState.WalkingUp.ToString(),
                playerSheet,
                new Rectangle[]
                    {
                    new Rectangle(2 * frameWidth, 1 * frameHeight, frameWidth, frameHeight),
                    new Rectangle(3 * frameWidth, 1* frameHeight, frameWidth, frameHeight),
                    },
                2.5f
                ));

            //IdleLeft Animation
            animator.AddAnimation(new Animation(
                PlayerState.IdleLeft.ToString(),
                playerSheet,
                new Rectangle[]
                    {
                    new Rectangle(0 * frameWidth, 2 * frameHeight, frameWidth, frameHeight),
                    new Rectangle(1 * frameWidth, 2 * frameHeight, frameWidth, frameHeight),
                    },
                2.5f
                ));

            //WalkingLeft Animation
            animator.AddAnimation(new Animation(
                PlayerState.WalkingLeft.ToString(),
                playerSheet,
                new Rectangle[]
                    {
                    new Rectangle(3 * frameWidth, 2 * frameHeight, frameWidth, frameHeight),
                    new Rectangle(2 * frameWidth, 2 * frameHeight, frameWidth, frameHeight),
                    },
                2.5f
                ));

            //IdleRight Animation
            animator.AddAnimation(new Animation(
                PlayerState.IdleRight.ToString(),
                playerSheet,
                new Rectangle[]
                    {
                    new Rectangle(0 * frameWidth, 3 * frameHeight, frameWidth, frameHeight),
                    new Rectangle(1 * frameWidth, 3 * frameHeight, frameWidth, frameHeight),
                    },
                2.5f
                ));

            //WalkingRight Animation
            animator.AddAnimation(new Animation(
                PlayerState.WalkingRight.ToString(),
                playerSheet,
                new Rectangle[]
                    {
                    new Rectangle(3 * frameWidth, 3 * frameHeight, frameWidth, frameHeight),
                    new Rectangle(2 * frameWidth, 3 * frameHeight, frameWidth, frameHeight),
                    },
                2.5f
                ));

            //Use AxeUp Animation
            animator.AddAnimation(new Animation(
                PlayerState.UseAxeUp.ToString(),
                useToolSheet,
                new Rectangle[]
                {
                    new Rectangle(0 * frameWidth, 5 * frameHeight, frameWidth, frameHeight),
                    new Rectangle(1 * frameWidth, 5 * frameHeight, frameWidth, frameHeight),
                },
                2.5f
                ));

            //Use AxeDown
            animator.AddAnimation(new Animation(
                PlayerState.UseAxeUp.ToString(),
                useToolSheet,
                new Rectangle[]
                {
                    new Rectangle(0 * frameWidth, 4 * frameHeight, frameWidth, frameHeight),
                    new Rectangle(1 * frameWidth, 4 * frameHeight, frameWidth, frameHeight),
                },
                2.5f
                ));

            //Use AxeLeft Animation
            animator.AddAnimation(new Animation(
                PlayerState.UseAxeUp.ToString(),
                useToolSheet,
                new Rectangle[]
                {
                    new Rectangle(0 * frameWidth, 6 * frameHeight, frameWidth, frameHeight),
                    new Rectangle(1 * frameWidth, 6 * frameHeight, frameWidth, frameHeight),
                },
                2.5f
                ));

            //Use AxeUp Animation
            animator.AddAnimation(new Animation(
                PlayerState.UseAxeUp.ToString(),
                useToolSheet,
                new Rectangle[]
                {
                    new Rectangle(0 * frameWidth, 7 * frameHeight, frameWidth, frameHeight),
                    new Rectangle(1 * frameWidth, 7 * frameHeight, frameWidth, frameHeight),
                },
                2.5f
                ));

            animator.PlayAnimation(PlayerState.IdleUp.ToString());
            var playerCollider = playerObject.AddComponent<Collider>();
            var playerComp = playerObject.AddComponent<Player>();
            var ui = playerObject.AddComponent<UI>(playerComp);

            //Bind MoveCommands
            InputHandler.Instance.AddUpdateCommand(Keys.W, new MoveCommand(playerComp, new Vector2(0, -1)));
            InputHandler.Instance.AddUpdateCommand(Keys.S, new MoveCommand(playerComp, new Vector2(0, 1)));
            InputHandler.Instance.AddUpdateCommand(Keys.A, new MoveCommand(playerComp, new Vector2(-1, 0)));
            InputHandler.Instance.AddUpdateCommand(Keys.D, new MoveCommand(playerComp, new Vector2(1, 0)));

            //Bind IdleCommands
            InputHandler.Instance.AddButtonUpCommand(Keys.W, new IdleCommand(playerComp, PlayerState.IdleUp));
            InputHandler.Instance.AddButtonUpCommand(Keys.S, new IdleCommand(playerComp, PlayerState.IdleDown));
            InputHandler.Instance.AddButtonUpCommand(Keys.A, new IdleCommand(playerComp, PlayerState.IdleLeft));
            InputHandler.Instance.AddButtonUpCommand(Keys.D, new IdleCommand(playerComp, PlayerState.IdleRight));

            //Bind ButtonClicks
            InputHandler.Instance.AddButtonDownCommand(Keys.I, new OpenInventoryCommand(ui));

            playerComp.SetState(PlayerState.IdleDown);

            return playerObject;
        }
    }
}
