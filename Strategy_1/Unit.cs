using System;
using System.Drawing;
namespace Strategy_1
{
    public class Unit : BaseUnit
    {
        public BaseUnit Enemy { get; set; } = null;
        public int IndUnit { get; set; }

        public int Speed { get; set; }
        public int RadiusAttack { get; set; }
        public int Damage { get; set; }
        public int TimeBreakShot { get; set; }
        public int BreakShot { get; set; } = 0;

        public double MoveX { get; set; } = 0;
        public double MoveY { get; set; } = 0;
        private double interactionForce { get; set; } = 0.35;

        public int PointX { get; set; }
        public int PointY { get; set; }

        public Unit(int indPlayer, int indUnit, double x, double y, int width, int height, int health, int speed,
            int radiusAttack, int damage, int tbreakShot) : base(indPlayer, x, y, width, height, health)
        {
            IndUnit = indUnit;

            Speed = speed;
            RadiusAttack = radiusAttack + (Width < Height ? Height / 2 : Width / 2);
            Damage = damage;
            TimeBreakShot = tbreakShot;
        }
        public Unit(Unit unit) : base((unit as BaseUnit))
        {
            IndUnit = unit.IndUnit;

            Speed = unit.Speed;
            RadiusAttack = unit.RadiusAttack;
            Damage = unit.Damage;
            TimeBreakShot = unit.TimeBreakShot;

            PointX = unit.PointX;
            PointY = unit.PointY;

            MoveX = unit.MoveX;
            MoveY = unit.MoveY;
        }

        public void Interact(BaseUnit otherUnit)
        {
            double directionX = X - otherUnit.X;
            double directionY = Y - otherUnit.Y;

            X += directionX * interactionForce;
            Y += directionY * interactionForce;
        }
        public void Move()
        {
            if (MoveX != 0 || MoveY != 0)
            {
                if (Collision())
                {
                    MoveX = 0;
                    MoveX = 0;
                }
                else
                {
                    X += MoveX;
                    Y += MoveY;
                }
            }
        }
        private void ChangeMove()
        {
            double dX = PointX - GetCenterX();
            double dY = PointY - GetCenterY();

            double sX = Math.Abs(dX);
            double sY = Math.Abs(dY);
            double s = sX + sY;
            sX = sX / s;
            sY = 1 - sX;

            if (dX > 0)
            {
                MoveX = Speed * sX;
            }
            else
            {
                MoveX = -Speed * sX;
            }

            if (dY > 0)
            {
                MoveY = Speed * sY;
            }
            else
            {
                MoveY = -Speed * sY;
            }
        }
        public void SetPoint(int x, int y)
        {
            PointX = x;
            PointY = y;

            ChangeMove();
        }
        private bool Collision()
        {
            Rectangle pointBounds = new Rectangle(PointX, PointY, 1, 1);
            Rectangle unitBounds = new Rectangle((int)X, (int)Y, Width, Height);
            return unitBounds.IntersectsWith(pointBounds);
        }

        public void SetEnemy(BaseUnit enemy)
        {
            if ((enemy as Spire) != null)
            {
                Enemy = (enemy as Spire);
            }
            else if ((enemy as Unit) != null)
            {
                Enemy = (enemy as Unit);
            }
            else if ((enemy as SpireSpawn) != null)
            {
                Enemy = (enemy as SpireSpawn);
            }
        }
        public bool CheckRadiusAttack()
        {
            if (Enemy == null) { return false; }

            double distance;
            double x, y;

            x = GetCenterX() - Enemy.GetCenterX();
            y = GetCenterY() - Enemy.GetCenterY();
            distance = Math.Sqrt(x * x + y * y);

            if (distance <= RadiusAttack)
            {
                return true;
            }
            return false;
        }
        public bool AttackEnemy()
        {
            if (BreakShot > 0) { return false; }
            if (!CheckRadiusAttack()) { return false; }

            if (Enemy.Health > 0)
            {
                Enemy.Health -= Damage;
                BreakShot = TimeBreakShot;
                return true;
            }
            return false;
        }
        public void Reload()
        {
            if (BreakShot > 0)
            {
                BreakShot--;
            }
        }
    }
}
