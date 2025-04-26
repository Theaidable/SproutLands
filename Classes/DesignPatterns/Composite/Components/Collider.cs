

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.World;
using System;
using System.Collections.Generic;

namespace SproutLands.Classes.DesignPatterns.Composite.Components
{
    public class Collider : Component
    {
        private Texture2D pixel;
        private SpriteRenderer spriteRenderer;
        private bool shouldDraw;
        private Lazy<List<RectangleData>> pixelPerfectRectangles;

        public List<RectangleData> PixelPerfectRectangles { get => pixelPerfectRectangles.Value; }

        //Tom Constructor på grund af nedarvning
        public Collider(GameObject gameObject) : base(gameObject) { }

        /// <summary>
        /// Lav pixelPerfectRectangles til objektet
        /// </summary>
        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>();
            pixel = GameWorld.Instance.Content.Load<Texture2D>("Assets/Collider/Pixel");
            pixelPerfectRectangles = new Lazy<List<RectangleData>>(() => CreateRectangles());
        }

        /// <summary>
        /// CollisonBox for object
        /// </summary>
        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)(GameObject.Transform.Position.X - spriteRenderer.Sprite.Width / 2),
                    (int)(GameObject.Transform.Position.Y - spriteRenderer.Sprite.Height / 2),
                    spriteRenderer.Sprite.Width,
                    spriteRenderer.Sprite.Height);
            }
        }

        /// <summary>
        /// Update pixelcollider
        /// </summary>
        public override void Update()
        {
            if (pixelPerfectRectangles.IsValueCreated)
            {
                UpdatePixelCollider();
            }
        }

        /// <summary>
        /// Hjælpemetode til at opdatere vores pixelcollider
        /// </summary>
        private void UpdatePixelCollider()
        {
            for (int i = 0; i < pixelPerfectRectangles.Value.Count; i++)
            {
                pixelPerfectRectangles.Value[i].UpdatePosition(GameObject, spriteRenderer.Sprite.Width, spriteRenderer.Sprite.Height);
            }
        }

        /// <summary>
        /// Draw vores rectangler hvis de toggles
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (shouldDraw)
            {
                DrawRectangle(CollisionBox, spriteBatch);
                if (pixelPerfectRectangles.IsValueCreated)
                {
                    foreach (var rect in pixelPerfectRectangles.Value)
                    {
                        DrawRectangle(rect.Rectangle, spriteBatch);
                    }
                }
            }
        }

        /// <summary>
        /// Tegner collider rectangles
        /// </summary>
        /// <param name="collisionBox"></param>
        /// <param name="spriteBatch"></param>
        private void DrawRectangle(Rectangle collisionBox, SpriteBatch spriteBatch)
        {
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, 1, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, 1, collisionBox.Height);

            spriteBatch.Draw(pixel, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(pixel, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(pixel, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(pixel, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        /// <summary>
        /// Bruges til at toggle drawing af og på
        /// </summary>
        /// <param name="shouldDraw"></param>
        public void ToggleDrawing(bool shouldDraw)
        {
            this.shouldDraw = shouldDraw;
        }

        /// <summary>
        /// Hjælpemetode til at oprette rectanglerne
        /// </summary>
        /// <returns></returns>
        private List<RectangleData> CreateRectangles()
        {
            List<Color[]> lines = new List<Color[]>();

            for (int y = 0; y < spriteRenderer.Sprite.Height; y++)
            {
                Color[] colors = new Color[spriteRenderer.Sprite.Width];
                spriteRenderer.Sprite.GetData(0, new Rectangle(0, y, spriteRenderer.Sprite.Width, 1), colors, 0, spriteRenderer.Sprite.Width);
                lines.Add(colors);
            }
            List<RectangleData> returnListOfRectangles = new List<RectangleData>();
            for (int y = 0; y < lines.Count; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x].A != 0)
                    {
                        if ((x == 0)
                            || (x == lines[y].Length)
                            || (x > 0 && lines[y][x - 1].A == 0)
                            || (x < lines[y].Length - 1 && lines[y][x + 1].A == 0)
                            || (y == 0)
                            || (y > 0 && lines[y - 1][x].A == 0)
                            || (y < lines.Count - 1 && lines[y + 1][x].A == 0))
                        {

                            RectangleData rd = new RectangleData(x, y);

                            returnListOfRectangles.Add(rd);
                        }
                    }
                }
            }
            return returnListOfRectangles;
        }
    }

    /// <summary>
    /// Hjælpeklasse der holder styr på rectanglernes data
    /// </summary>
    public class RectangleData
    {
        public Rectangle Rectangle { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public RectangleData(int x, int y)
        {
            this.Rectangle = new Rectangle();
            this.X = x;
            this.Y = y;
        }

        public void UpdatePosition(GameObject gameObject, int width, int height)
        {
            Rectangle = new Rectangle((int)gameObject.Transform.Position.X + X - width / 2, (int)gameObject.Transform.Position.Y + Y - height / 2, 1, 1);
        }
    }
}
