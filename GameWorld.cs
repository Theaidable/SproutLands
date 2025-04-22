using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SproutLands.Classes.CommandPattern;
using SproutLands.Classes.Player;
using System.Collections.Generic;

namespace SproutLands;

public class GameWorld : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Player _player;
    private InputHandler _inputHandler;

    private static GameWorld instance;

    //Oprettelse af Singleton af GameWorld
    public static GameWorld Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameWorld();
            }
            return instance;
        }
    }

    //Private Constructor, da vi gøre brug af singleton
    private GameWorld()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _player = new Player();

        // Opret InputHandler og bind taster til MoveCommand
        _inputHandler = new InputHandler();
        _inputHandler.BindKey(Keys.W, new MoveCommand(_player, new Vector2(0, -1))); // Op
        _inputHandler.BindKey(Keys.S, new MoveCommand(_player, new Vector2(0, 1)));  // Ned
        _inputHandler.BindKey(Keys.A, new MoveCommand(_player, new Vector2(-1, 0))); // Venstre
        _inputHandler.BindKey(Keys.D, new MoveCommand(_player, new Vector2(1, 0)));  // Højre

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _inputHandler.HandleInput();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
      

        base.Draw(gameTime);
    }
}
