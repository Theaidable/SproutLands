﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.World;
using System;


namespace SproutLands.Classes.DesignPatterns.Composite.Components
{
    public enum SpriteType
    {
        Sprite,
        Rectangle
    }

    public class SpriteRenderer : Component
    {
        public Vector2 Origin { get; set; }
        public Texture2D Sprite { get; set; }
        public Color Color { get; set; }
        public Rectangle? SourceRectangle { get; set; }
        public event Action OnSpriteChanged;

        public SpriteRenderer(GameObject gameObject): base(gameObject)
        {
            Color = Color.White;
        }

        /// <summary>
        /// Sæt sprite af et object, kan gøre brug af et spriteSheet hvis nødvendigt
        /// </summary>
        /// <param name="spriteName"></param>
        public void SetSprite(string spriteName, Rectangle? sourceRectangle = null)
        {
            Sprite = GameWorld.Instance.Content.Load<Texture2D>(spriteName);
            SourceRectangle = sourceRectangle;
            OnSpriteChanged?.Invoke();
        }

        /// <summary>
        /// Sæt origin til midten af valgt sprite
        /// </summary>
        public override void Start()
        {
            if (SourceRectangle.HasValue)
            {
                Origin = new Vector2(SourceRectangle.Value.Width / 2f, SourceRectangle.Value.Height / 2f);
            }
            else
            {
                Origin = new Vector2(Sprite.Width / 2f, Sprite.Height / 2f);
            }
        }

        public void InvokeOnSpriteChanged()
        {
            OnSpriteChanged?.Invoke();
        }

        /// <summary>
        /// Tegn sprite af object
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, GameObject.Transform.Position, SourceRectangle, Color, GameObject.Transform.Rotation, Origin, GameObject.Transform.Scale, SpriteEffects.None, 0);
        }
    }
}
