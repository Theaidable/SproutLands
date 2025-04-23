using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SproutLands.Classes.ComponentPattern;
using SproutLands.Classes.ComponentPattern.Objects;
using SproutLands.Classes.FactoryPattern;
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

    //Map Layout
    private int[,] tileMap =
    {
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,2,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,5,0},
        {0,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,10,1,1,8,0},
        {0,6,1,1,1,1,1,1,1,1,1,1,10,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8,0},
        {0,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,10,1,1,1,1,8,0},
        {0,6,1,10,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8,0},
        {0,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,01,8,0},
        {0,6,1,1,1,1,1,1,1,1,10,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8,0},
        {0,6,10,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8,0},
        {0,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,10,1,1,1,1,1,8,0},
        {0,6,1,1,1,1,10,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8,0},
        {0,6,1,1,1,1,1,1,1,1,10,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8,0},
        {0,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,10,1,1,1,1,1,1,1,1,1,1,8,0},
        {0,6,1,1,1,1,1,1,1,1,1,1,10,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8,0},
        {0,6,1,1,10,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8,0},
        {0,3,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,4,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
    };
    private int tileSize = 64;

    //Liste over GameObjects
    public List<GameObject> GameObjects { get; private set; } = new List<GameObject>();

    //Deltatime property
    public float DeltaTime { get; private set; }

    //Private Constructor, da vi gøre brug af singleton
    private GameWorld()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = tileSize * 30;
        _graphics.PreferredBackBufferHeight = tileSize * 18;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        CreateLevel();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        foreach (var obj in GameObjects)
        {
            obj.Update();
        }

        base.Update(gameTime);
    }

    /// <summary>
    /// Tegner alt i GameWorld
    /// </summary>
    /// <param name="gameTime"></param>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(155,212,195));

        _spriteBatch.Begin();

        foreach (var obj in GameObjects)
        {
            obj.Draw(_spriteBatch);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    /// <summary>
    /// Metoden opretter banen ved at vi sætter nogle værdier til 2D-arrayet
    /// </summary>
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
                        CreateSoilTile(position, new DirtState(DirtType.Dirt1));
                        break;

                    case 2: // Soil - DirtState (grass)
                        CreateSoilTile(position, new DirtState(DirtType.Dirt2));
                        break;

                    case 3: // Soil - DirtState (grass)
                        CreateSoilTile(position, new DirtState(DirtType.Dirt3));
                        break;

                    case 4: // Soil - DirtState (grass)
                        CreateSoilTile(position, new DirtState(DirtType.Dirt4));
                        break;

                    case 5: // Soil - DirtState (grass)
                        CreateSoilTile(position, new DirtState(DirtType.Dirt5));
                        break;

                    case 6: // Soil - DirtState (grass)
                        CreateSoilTile(position, new DirtState(DirtType.Dirt6));
                        break;

                    case 7: // Soil - DirtState (grass)
                        CreateSoilTile(position, new DirtState(DirtType.Dirt7));
                        break;

                    case 8: // Soil - DirtState (grass)
                        CreateSoilTile(position, new DirtState(DirtType.Dirt8));
                        break;

                    case 9: // Soil - DirtState (grass)
                        CreateSoilTile(position, new DirtState(DirtType.Dirt9));
                        break;
                    case 10: // Soil - DirtState (grass)
                        CreateSoilTile(position, new DirtState(DirtType.Dirt10));
                        break;

                    case 11: // Soil - PreparedState (earth)
                        CreateSoilTile(position, new PreparedState(PreparedType.Prepared1));
                        break;

                    case 12: // Soil - PreparedState (earth)
                        CreateSoilTile(position, new PreparedState(PreparedType.Prepared2));
                        break;
                }

                //Instanser af træer i højre hjørne:
                GameObjects.Add(TreeFactory.Instance.Create(new Vector2(25 * 64, 2 * 64), TreeType.Tree1));
                GameObjects.Add(TreeFactory.Instance.Create(new Vector2(26 * 64, 3 * 64), TreeType.Tree1));
                GameObjects.Add(TreeFactory.Instance.Create(new Vector2(24 * 64, 3 * 64), TreeType.Tree1));

                //Instanser af træer i venstre hjørne:
                GameObjects.Add(TreeFactory.Instance.Create(new Vector2(3 * 64, 12 * 64), TreeType.Tree2));
                GameObjects.Add(TreeFactory.Instance.Create(new Vector2(4 * 64, 13 * 64), TreeType.Tree2));

                //Instans af træ i midten:
                GameObjects.Add(TreeFactory.Instance.Create(new Vector2(15 * 64, 8 * 64), TreeType.Tree3));

                foreach (var go in GameObjects)
                {
                    go.Awake();
                    go.Start();
                }
            }
        }
    }

    /// <summary>
    /// Metoden bruges til at oprette en tile
    /// </summary>
    /// <param name="position"></param>
    /// <param name="spriteName"></param>
    private void CreateTile(Vector2 position, string spriteName)
    {
        var tileObject = new GameObject();
        tileObject.Transform.Position = position;
        var sr = tileObject.AddComponent<SpriteRenderer>();
        sr.SetSprite(spriteName,new Rectangle(0,0,64,64));
        GameObjects.Add(tileObject);
    }

    /// <summary>
    /// Metoden bruges til at oprette forskellige typer af jord tiles
    /// </summary>
    /// <param name="position"></param>
    /// <param name="initialState"></param>
    private void CreateSoilTile(Vector2 position, ISoilState initialState)
    {
        var soilObject = new GameObject();
        soilObject.Transform.Position = position;
        var sr = soilObject.AddComponent<SpriteRenderer>();
        var soil = soilObject.AddComponent<Soil>();
        soil.SetState(initialState);
        GameObjects.Add(soilObject);
    }
}
