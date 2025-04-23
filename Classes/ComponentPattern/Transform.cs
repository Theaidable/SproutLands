﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SproutLands.Classes.ComponentPattern
{
    public class Transform : Component
    {
        /// <summary>
        /// Properties til at tilgå værdier for transformer
        /// </summary>
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; } = Vector2.One;
        public float Rotation { get; set; }

        /// <summary>
        /// Tom constructor som er nødvendig på grund af nedarvning
        /// </summary>
        /// <param name="gameObject"></param>
        public Transform(GameObject gameObject) : base(gameObject) { }

        /// <summary>
        /// Metode til at translate position for objektet
        /// </summary>
        /// <param name="translation"></param>
        public void Translate(Vector2 translation)
        {
            Position += translation;
        }
    }
}
