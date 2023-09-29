using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_1
{
    public class SpireSpawn : BaseUnit
    {
        public int TimeBreakSpawn { get; set; }
        public int TimeSpawn { get; set; } = 0;
        public Unit SpawnUnit { get; set; }
        public int CountSpawn { get; set; }

        public int SpawnX { get; set; }
        public int SpawnY { get; set; }

        public double dX { get; set; }
        public double dY { get; set; }

        public SpireSpawn(int indPlayer, int x, int y, int width, int height, int health,
            int tSpawn, Unit unit, int countSpawn)
            : base(indPlayer, x, y, width, height, health)
        {
            IndPlayer = indPlayer;
            SpawnUnit = unit;
            TimeBreakSpawn = tSpawn;
            CountSpawn = countSpawn;
        }
        public SpireSpawn(SpireSpawn spire) : base((spire as BaseUnit))
        {
            IndPlayer = spire.IndPlayer;
            SpawnUnit = new Unit(spire.SpawnUnit);
            TimeBreakSpawn = spire.TimeBreakSpawn;
            CountSpawn = spire.CountSpawn;
            dX = spire.dX;
            dY = spire.dY;
        }

        public void SetPoint(int x, int y)
        {
            dX = x - GetCenterX();
            dY = y - GetCenterY();
            SetSide();
        }
        public void ChangePoint()
        {
            dX = -dX;
            dY = -dY;
            SetSide();
        }
        public void SetSide()
        {
            if (Math.Abs(dY) >= Math.Abs(dX))
            {
                if (dY <= 0)
                {
                    SpawnY = (int)Y - 1 - SpawnUnit.Height;
                }
                else
                {
                    SpawnY = (int)Y + Height + 1 + SpawnUnit.Height;
                }
                SpawnX = (int)GetCenterX() - SpawnUnit.Width / 2;
            }
            else
            {
                if (dX <= 0)
                {
                    SpawnX = (int)X - 1 - SpawnUnit.Width;
                }
                else
                {
                    SpawnX = (int)X + Width + 1 + SpawnUnit.Width;
                }
                SpawnY = (int)GetCenterY() + SpawnUnit.Height / 2;
            }
        }

        public Unit Spawn()
        {
            if (TimeSpawn > 0) { return null; }

            SpawnUnit.X = SpawnX;
            SpawnUnit.Y = SpawnY;
            return new Unit(SpawnUnit);
        }
        public void Reload()
        {
            if (TimeSpawn > 0)
            {
                TimeSpawn--;
            }
        }
    }
}
