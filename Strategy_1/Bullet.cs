namespace Strategy_1
{
    public class Bullet
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }

        public Bullet(Unit unit1, BaseUnit unit2)
        {
            X1 = unit1.GetCenterX();
            Y1 = unit1.GetCenterY();

            X2 = unit2.GetCenterX();
            Y2 = unit2.GetCenterY();
        }
    }
}
