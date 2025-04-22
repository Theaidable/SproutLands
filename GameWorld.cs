using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SproutLands.Classes.ComponentPattern.Objects;
using System.Collections.Generic;

namespace SproutLands;

public class GameWorld : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private static GameWorld instance;
    private List<Tree> _trees;
    private Texture2D _treeTexture;

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
        _trees = new List<Tree>
    {
        new Tree(null, new Vector2(100, 100), new Rectangle(0, 0, 32, 32)), // Example frame
        new Tree(null, new Vector2(200, 150), new Rectangle(32, 0, 32, 32)) // Another frame
    };

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Load the tree texture
        _treeTexture = Content.Load<Texture2D>("/Assets/Sprites/Objects/Basic_Grass_Biom_things");

        // Assign the texture to each tree
        foreach (var tree in _trees)
        {
            tree.SetTexture(_treeTexture);
        }
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Handle player interaction with trees
        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            var mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            foreach (var tree in _trees)
            {
                if (tree.ContainsPoint(mousePosition))
                {
                    tree.Interact();
                }
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        // Draw all trees
        foreach (var tree in _trees)
        {
            tree.Draw(_spriteBatch);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
