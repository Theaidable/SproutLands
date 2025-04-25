using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace SproutLands.Classes.ComponentPattern.Colliders
{
    public class Collider : Component
    {
        private Texture2D pixel;
        private SpriteRenderer spriteRenderer;
        private bool shouldDraw;
        private Lazy<List<RectangleData>> pixelPerfectRectangles;
        private Rectangle spriteSourceRectangle;
        private Texture2D spriteTexture;
        public List<RectangleData> PixelPerfectRectangles { get => pixelPerfectRectangles.Value; }
        public Collider(GameObject gameObject) : base(gameObject) { }

        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>();
            spriteTexture = spriteRenderer.Sprite;
            spriteSourceRectangle = spriteRenderer.SourceRectangle ?? new Rectangle(0, 0, spriteTexture.Width, spriteTexture.Height);
            pixel = GameWorld.Instance.Content.Load<Texture2D>("Assets/Collider/Pixel");
            pixelPerfectRectangles = new Lazy<List<RectangleData>>(() => CreateRectangles());
            UpdatePixelCollider();
        }
        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)(GameObject.Transform.Position.X - spriteSourceRectangle.Width / 2),
                    (int)(GameObject.Transform.Position.Y - spriteSourceRectangle.Height / 2),
                    spriteSourceRectangle.Width,
                    spriteSourceRectangle.Height);
            }
        }

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

        public override void Update()
        {
            if (pixelPerfectRectangles.IsValueCreated)
            {
                UpdatePixelCollider();
            }
        }

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

        private void UpdatePixelCollider()
        {
            foreach (RectangleData rectangleData in pixelPerfectRectangles.Value)
            {
                rectangleData.UpdatePosition(GameObject, spriteSourceRectangle);
            }
        }

        public void ToggleDrawing(bool shouldDraw)
        {
            this.shouldDraw = shouldDraw;
        }

        private List<RectangleData> CreateRectangles()
        {
            List<Color[]> lines = new List<Color[]>();

            for (int y = 0; y < spriteSourceRectangle.Height; y++)
            {
                Color[] colors = new Color[spriteSourceRectangle.Width];
                spriteTexture.GetData(0,new Rectangle(spriteSourceRectangle.X, spriteSourceRectangle.Y + y, spriteSourceRectangle.Width, 1), colors, 0,spriteSourceRectangle.Width);
                lines.Add(colors);
            }

            var result = new List<RectangleData>();
            for (int y = 0; y < spriteSourceRectangle.Height; y++)
            {
                for (int x = 0; x < spriteSourceRectangle.Width; x++)
                {
                    if (lines[y][x].A == 0) continue;

                    bool edge =
                        x == 0 || x == spriteSourceRectangle.Width - 1
                     || y == 0 || y == spriteSourceRectangle.Height - 1
                     || lines[y][x - 1].A == 0
                     || lines[y][x + 1].A == 0
                     || lines[y - 1][x].A == 0
                     || lines[y + 1][x].A == 0;

                    if (edge)
                        result.Add(new RectangleData(x, y));
                }
            }

            return result;
        }
    }
}
