using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SproutLands.Classes.ComponentPattern;
using SproutLands.Classes.ComponentPattern.Objects;
using SproutLands.Classes.StatePattern.SoilState;
using SproutLands.Classes.StatePattern.SoilState.SoilStates;
using System.Collections.Generic;

namespace SproutLands;

public class GameWorld : Game
{
    //Standard fields
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    //Oprettelse af Singleton af GameWorld
    private static GameWorld instance;
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

    //Simpelt map layout
    private int[,] tileMap =
    {
        {0,0,0,0,0,0,0,0,0,0},
        {0,1,1,1,1,1,1,1,1,0},
        {0,1,1,1,1,2,2,1,1,0},
        {0,1,1,1,1,2,2,1,1,0},
        {0,1,1,1,1,1,1,1,1,0},
        {0,0,0,0,0,0,0,0,0,0}
    };
    private int tileSize = 32;

    //Liste over GameObjects
    public List<GameObject> GameObjects { get; private set; } = new List<GameObject>();

    //Private Constructor, da vi gøre brug af singleton
    private GameWorld()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        GameWorld.Instance.CreateLevel();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Handle player interaction with trees
        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            var mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }

        foreach (var obj in GameObjects)
        {
            obj.Update();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        foreach (var obj in GameObjects)
        {
            obj.Draw(_spriteBatch);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    public void CreateLevel()
    {
        int rows = tileMap.GetLength(0);
        int cols = tileMap.GetLength(1);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Vector2 position = new(x * tileSize, y * tileSize);
                int tileType = tileMap[y, x];

                switch (tileType)
                {
                    case 0: // Water
                        CreateTile(position, "Assets/Sprites/Tilesets/Water");
                        break;

                    case 1: // Soil - DirtState (grass)
                        CreateSoilTile(position, new DirtState());
                        break;

                    case 2: // Soil - PreparedState (earth)
                        CreateSoilTile(position, new PreparedState());
                        break;
                }
            }
        }
    }

    private void CreateTile(Vector2 position, string spriteName)
    {
        var tileObject = new GameObject();
        tileObject.Transform.Position = position;
        var sr = tileObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        sr.SetSprite(spriteName,new Rectangle(0,0,32,32));
        GameObjects.Add(tileObject);
    }

    private void CreateSoilTile(Vector2 position, ISoilState initialState)
    {
        var soilObject = new GameObject();
        soilObject.Transform.Position = position;
        var sr = soilObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        var soil = soilObject.AddComponent<Soil>() as Soil;
        soil.SetState(initialState);
        GameObjects.Add(soilObject);
    }
}
