using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_1
{
    public class BaseUnit
    {
        public int IndPlayer { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int maxHealth { get; set; }
        public int Health { get; set; }

        public BaseUnit(int indPlayer, double x, double y, int width, int height, int health)
        {
            IndPlayer = indPlayer;

            X = x;
            Y = y;
            Width = width;
            Height = height;

            Health = health;
            maxHealth = health;
        }
        public BaseUnit(BaseUnit unit)
        {
            IndPlayer = unit.IndPlayer;

            X = unit.X;
            Y = unit.Y;
            Width = unit.Width;
            Height = unit.Height;

            Health = unit.Health;
            maxHealth = unit.maxHealth;
        }

        public double GetCenterX() { return X + Width / 2; }
        public double GetCenterY() { return Y + Height / 2; }
    }
}
