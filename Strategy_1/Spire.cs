namespace Strategy_1
{
    public class Spire : BaseUnit
    {
        public Spire(int indPlayer, int x, int y, int width, int height, int health)
            : base(indPlayer, x, y, width, height, health)
        {}
        public Spire(Spire spire) : base((spire as BaseUnit))
        {}
    }
}
