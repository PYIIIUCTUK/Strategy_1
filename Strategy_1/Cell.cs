using System.Collections.Generic;

namespace Strategy_1
{
    public class Cell
    {
        public List<BaseUnit> Units = new List<BaseUnit>();
        public Spire Spire = null;
        public Cell() { }
    }
}
