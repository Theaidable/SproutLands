using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SproutLands.Classes.ComponentPattern.Objects
{
    public class Tree : Component
    {
        //Properties til at tilgå træets egenskaber
        public int Health { get; private set; }
        public bool IsChopped { get; private set; }
        public string ResourceType { get; private set; }
        public int ResourceAmount { get; private set; }

        /// <summary>
        /// Constructor med standardværdier når man opretter en instans af et træ
        /// </summary>
        /// <param name="gameObject"></param>
        public Tree(GameObject gameObject) : base(gameObject)
        {
            Health = 100;
            IsChopped = false;
            ResourceType = "Wood";
            ResourceAmount = 5;
        }

        /// <summary>
        /// Metode som tager liv fra taget når det "tager skade"
        /// </summary>
        /// <param name="amount"></param>
        public void TakeDamage(int amount)
        {
            if (IsChopped) return;

            Health -= amount;
            
            if (Health <= 0)
            {
                IsChopped = true;
                DropResources();
            }
        }

        private void DropResources()
        {
            //Her skal vi skrive loggikken for at droppe ressourcer når træet fældes
            
            //Debug line
            Debug.WriteLine($"{ResourceAmount} {ResourceType} dropped!");
        }
    }
}
