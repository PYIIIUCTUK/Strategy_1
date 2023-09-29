using System.Collections.Generic;

namespace Strategy_1
{
    public class Player
    {
        public int Ind { get; set; }
        public int Money { get; set; }
        public int Health { get; set; }
        public List<BaseUnit> Units { get; set; } = new List<BaseUnit>();
        public List<Spire> Spires { get; set; } = new List<Spire>();

        public Player(int ind, int money, int health)
        {
            Ind = ind;
            Money = money;
            Health = health;
        }
        public Player(Player player)
        {
            Ind = player.Ind;
            Money = player.Money;
            Health = player.Health;
            for (int i = 0; i < player.Units.Count; i++)
            {
                if ((player.Units[i] as Unit) != null)
                {
                    Units.Add(new Unit((player.Units[i] as Unit)));
                }
                else
                {
                    Units.Add(new SpireSpawn((player.Units[i] as SpireSpawn)));
                }
            }
            for (int i = 0; i < player.Spires.Count; i++)
            {
                Spires.Add(new Spire(player.Spires[i]));
            }
        }
    }
}
