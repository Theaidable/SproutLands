using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using SproutLands.Classes.DesignPatterns.Composite;
using SproutLands.Classes.DesignPatterns.Observer;
using SproutLands.Classes.DesignPatterns.Composite.Components;
using SproutLands.Classes.DesignPatterns.FactoryPattern.Trees;
using SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren;
using SproutLands.Classes.DesignPatterns.Command;
using SproutLands.Classes.World.Tiles;
using SproutLands.Classes.World.Tiles.SoilStates;
using SproutLands.Classes.UI;
using SproutLands.Classes.Items;

namespace SproutLands.Classes.World;

public class GameWorld : Game, ISubject
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
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

    //Map Layout

    private int[,] tileMap =
    {
        {0 ,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0 ,2,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,5,0},
        {0,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,10,1,1,8,0},
        {0,6,1,1,1,1,1,1,1,1,1,1,10,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8,0},
        {0,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,10,1,1,1,1,8,0},
        {0,6,1,10,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8,0},
        {0,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,10,8,0},
        {0,6,1,1,1,1,1,1,1,1,10,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8,0},
        {0,6,10,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8,0},
        {0,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,10,1,1,1,1,1,8,0},
        {0,6,1,1,1,1,10,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8,0},
        {0,6,1,1,1,1,1,1,1,1,10,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8,0},
        {0,6,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,10,1,1,1,1,1,1,1,1,1,1,8,0},
        {0,6,1,1,1,1,1,1,1,1,1,1,10,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8,0},
        {0,6,1,1,10,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8,0},
        {0 ,3,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,4,0},
        {0 ,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0 ,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
    };

    private int tileSize = 64;

    //Lister
    public List<GameObject> GameObjects { get; private set; } = new List<GameObject>();
    private readonly List<IObserver> listeners = new List<IObserver>();

    //Deltatime property
    public float DeltaTime { get; private set; }

    //Private Constructor, da vi gøre brug af singleton
    private GameWorld()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        int cols = tileMap.GetLength(1);
        int rows = tileMap.GetLength(0);

        _graphics.PreferredBackBufferWidth = cols * tileSize;
        _graphics.PreferredBackBufferHeight = rows * tileSize;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        foreach (GameObject gameObject in GameObjects)
        {
            gameObject.Awake();
        }

        InputHandler.Instance.AddButtonDownCommand(Keys.K, new ToggleColliderDrawingCommand(GameObjects));

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        CreateLevel();
        SpawnTrees();

        //Spawn Player
        GameObject playerObject = PlayerFactory.Instance.Create(new Vector2(15 * 64, 13 * 64));
        GameObjects.Add(playerObject);

        foreach (GameObject gameObject in GameObjects)
        {
            gameObject.Start();
        }

        Hudbar hudbar = playerObject.GetComponent<Hudbar>();
        if (hudbar != null)
        {
            Texture2D axeIcon = Content.Load<Texture2D>("Assets/ItemSprites/Axe/Axe");
            Axe axe = new Axe(axeIcon);
            hudbar.AddItemToHud(axe);
        }
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        InputHandler.Instance.Execute();

        foreach (GameObject gameObject in GameObjects)
        {
            gameObject.Update();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(155, 212, 195));

        _spriteBatch.Begin();

        foreach (GameObject gameObject in GameObjects)
        {
            gameObject.Draw(_spriteBatch);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void CreateLevel()
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
                        CreateWaterTile(position);
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
                    default:
                        throw new Exception($"Ukendt tileType {tileType} ved ({x},{y})");
                }
            }
        }
    }

    /// <summary>
    /// Metoden bruges til at oprette en tile
    /// </summary>
    /// <param name="position"></param>
    /// <param name="spriteName"></param>
    private void CreateWaterTile(Vector2 position)
    {
        var waterObject = new GameObject();
        var spriteRenderer = waterObject.AddComponent<SpriteRenderer>();
        var waterAnimation = waterObject.AddComponent<Animator>();

        waterObject.Transform.Position = position;
        spriteRenderer.Sprite = Content.Load<Texture2D>("Assets/Sprites/Tilesets/Water");
        spriteRenderer.SourceRectangle = new Rectangle(0, 0, tileSize, tileSize);

        Rectangle[] frames = new Rectangle[4];
        for (int i = 0; i < frames.Length; i++)
        {
            frames[i] = new Rectangle(i * tileSize, 0, tileSize, tileSize);
        }

        waterAnimation.AddAnimation(new Animation("Flow", 4, true, null, frames));
        waterAnimation.PlayAnimation("Flow");
        GameObjects.Add(waterObject);
    }

    /// <summary>
    /// Metoden bruges til at oprette forskellige typer af jord tiles
    /// </summary>
    /// <param name="position"></param>
    /// <param name="initialState"></param>
    private void CreateSoilTile(Vector2 position, ISoilState initialState)
    {
        var soilObject = new GameObject();
        var soilState = soilObject.AddComponent<Soil>();
        soilObject.AddComponent<SpriteRenderer>();
        soilObject.Transform.Position = position;
        soilState.SetState(initialState);
        GameObjects.Add(soilObject);
    }

    private void SpawnTrees()
    {
        //Instanser af træer i højre hjørne:
        GameObjects.Add(TreeFactory.Instance.Create(new Vector2(25 * 64, 2 * 64), TreeType.Tree1));
        GameObjects.Add(TreeFactory.Instance.Create(new Vector2(26 * 64, 3 * 64), TreeType.Tree1));
        GameObjects.Add(TreeFactory.Instance.Create(new Vector2(24 * 64, 3 * 64), TreeType.Tree1));

        //Instanser af træer i venstre hjørne:
        GameObjects.Add(TreeFactory.Instance.Create(new Vector2(3 * 64, 12 * 64), TreeType.Tree2));
        GameObjects.Add(TreeFactory.Instance.Create(new Vector2(4 * 64, 13 * 64), TreeType.Tree2));

        //Instans af træ i midten:
        GameObjects.Add(TreeFactory.Instance.Create(new Vector2(15 * 64, 8 * 64), TreeType.Tree3));
    }

    public void Attach(IObserver observer)
    {
        listeners.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        listeners.Remove(observer);
    }

    public void Notify()
    {
        foreach (IObserver observer in listeners)
        {
            observer.Updated();
        }
    }
}
