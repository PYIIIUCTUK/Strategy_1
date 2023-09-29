using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Strategy_1
{
    public partial class Deploy : Form
    {
        myMenu menu;
        bool openMenu = true;
        int money;

        Timer timer = new Timer();
        int interval = 70;

        int S, W, H;
        int offsetX;
        int offsetY;

        bool turnFirst = true;
        BaseUnit unit;
        Player player;
        List<Player> newPlayers = new List<Player>();
        List<Player> players = new List<Player>();
        List<List<Cell>> cells = new List<List<Cell>>();

        int costLight = 200;
        int costMedium = 450;
        int costHeavy = 950;
        int costDestroyer = 1350;
        int costSpireLight = 1600;

        public Deploy(myMenu Menu, int Money, List<Player> Players, int w, int h, int s, int offx, int offy)
        {
            InitializeComponent();

            menu = Menu;
            players = Players;
            money = Money;
            money += 550;

            H = h; W = w; S = s; offsetX = offx; offsetY = offy;
        }

        private void Deploy_Shown(object sender, EventArgs e)
        {
            rbLight.Text += $": {costLight}";
            rbMedium.Text += $": {costMedium}";
            rbHeavy.Text += $": {costHeavy}";
            rbDestroyer.Text += $": {costDestroyer}";
            rbSpireLight.Text += $": {costSpireLight}";

            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.Sizable;

            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();

            for (int i = 0; i < H; i++)
            {
                List<Cell> lCells = new List<Cell>();
                for (int j = 0; j < W; j++)
                {
                    lCells.Add(new Cell());
                }
                cells.Add(lCells);
            }

            int x, y;

            for(int j = 0; j < players.Count; j++)
            {
                for (int i = 0; i < players[j].Spires.Count; i++)
                {
                    x = (int)players[j].Spires[i].GetCenterX() / S;
                    y = (int)players[j].Spires[i].GetCenterY() / S;
                    cells[y][x].Spire = new Spire(players[j].Spires[i]);
                }
                for (int i = 0; i < players[j].Units.Count; i++)
                {
                    x = (int)players[j].Units[i].GetCenterX() / S;
                    y = (int)players[j].Units[i].GetCenterY() / S;
                    if ((players[j].Units[i] as Unit) != null)
                    {
                        cells[y][x].Units.Add(new Unit((players[j].Units[i] as Unit)));
                    }
                    else if ((players[j].Units[i] as SpireSpawn) != null)
                    {
                        cells[y][x].Units.Add(new SpireSpawn((players[j].Units[i] as SpireSpawn)));
                    }
                }
            }

            player = new Player(players[0]);
            player.Money += money;
            rbLight.Checked = true;
        }
        private void Deploy_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Stop();
            if (openMenu)
            {
                menu.Show();
            }
            else
            {
                Game myGame = new Game(menu, money, newPlayers, W, H, S, offsetX, offsetY);
                myGame.Show();
            }
        }
        private int TimeTranslation(double time)
        {
            return (int)(time / interval);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
        private void Deploy_Paint(object sender, PaintEventArgs e)
        {
            Unit tmpUnit;
            Spire tmpSpire;
            Brush tmpBrush;

            Player tmpPlayer;
            if (player.Ind == 0)
            {
                tmpBrush = Brushes.Red;
                tmpPlayer = players[1];
            }
            else
            {
                tmpBrush = Brushes.Green;
                tmpPlayer = players[0];
            }
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    if (turnFirst)
                    {
                        if ((i < H / 4 && (j < 2 || j >= W - 2)) ||
                            (i >= H / 2 && i < H - 6 && (j >= 8 && j < W - 8)) ||
                            (i <= 1 && (j < W / 3 + W / 4 && j >= W - W / 3 - W / 4)))
                        {
                            e.Graphics.FillRectangle(Brushes.LightBlue, offsetX + j * S, offsetY + i * S, S, S);
                        }
                        else
                        {
                            e.Graphics.FillRectangle(Brushes.Gray, offsetX + j * S, offsetY + i * S, S, S);
                        }
                    }
                    else
                    {
                        if ((i >= H - H / 4 && (j < 2 || j >= W - 2)) ||
                            (i < H / 2 && i > 5 && (j >= 8 && j < W - 8)) ||
                            (i >= H - 2 && (j < W / 3 + W / 4 && j >= W - W / 3 - W / 4)))
                        {
                            e.Graphics.FillRectangle(Brushes.LightBlue, offsetX + j * S, offsetY + i * S, S, S);
                        }
                        else
                        {
                            e.Graphics.FillRectangle(Brushes.Gray, offsetX + j * S, offsetY + i * S, S, S);
                        }
                    }
                    e.Graphics.DrawRectangle(Pens.Black, offsetX + j * S, offsetY + i * S, S, S);
                }
            }
            for (int j = 0; j < tmpPlayer.Units.Count; j++)
            {
                tmpUnit = (tmpPlayer.Units[j] as Unit);
                if (tmpUnit != null)
                {
                    if (tmpUnit.IndUnit < 3)
                    {
                        e.Graphics.FillRectangle(tmpBrush, (int)tmpUnit.X + offsetX,
                        (int)tmpUnit.Y + offsetY, tmpUnit.Width, tmpUnit.Height);
                    }
                    else if (tmpUnit.IndUnit == 3)
                    {
                        e.Graphics.FillEllipse(tmpBrush, (int)tmpUnit.X + offsetX,
                        (int)tmpUnit.Y + offsetY, tmpUnit.Width, tmpUnit.Height);
                    }
                }
                else if ((tmpPlayer.Units[j] as SpireSpawn) != null)
                {
                    e.Graphics.FillRectangle(tmpBrush, (int)tmpPlayer.Units[j].X + offsetX,
                    (int)tmpPlayer.Units[j].Y + offsetY, tmpPlayer.Units[j].Width, tmpPlayer.Units[j].Height);
                }
            }
            for (int j = 0; j < tmpPlayer.Spires.Count; j++)
            {
                tmpSpire = tmpPlayer.Spires[j];
                e.Graphics.FillRectangle(Brushes.Black, (int)tmpSpire.X + offsetX,
                    (int)tmpSpire.Y + offsetY, tmpSpire.Width, tmpSpire.Height);
            }

            if (player.Ind == 0)
            {
                tmpBrush = Brushes.Green;
            }
            else
            {
                tmpBrush = Brushes.Red;
            }
            for (int i = 0; i < player.Units.Count; i++)
            {
                tmpUnit = (player.Units[i] as Unit);
                if (tmpUnit != null)
                {
                    if (tmpUnit.IndUnit < 3)
                    {
                        e.Graphics.FillRectangle(tmpBrush, (int)tmpUnit.X + offsetX,
                        (int)tmpUnit.Y + offsetY, tmpUnit.Width, tmpUnit.Height);
                    }
                    else if (tmpUnit.IndUnit == 3)
                    {
                        e.Graphics.FillEllipse(tmpBrush, (int)tmpUnit.X + offsetX,
                        (int)tmpUnit.Y + offsetY, tmpUnit.Width, tmpUnit.Height);
                    }
                }
                else
                {
                    e.Graphics.FillRectangle(tmpBrush, (int)player.Units[i].X + offsetX,
                        (int)player.Units[i].Y + offsetY, player.Units[i].Width, player.Units[i].Height);
                }
            }
            for (int j = 0; j < player.Spires.Count; j++)
            {
                tmpSpire = player.Spires[j];
                e.Graphics.FillRectangle(Brushes.Black, (int)tmpSpire.X + offsetX,
                    (int)tmpSpire.Y + offsetY, tmpSpire.Width, tmpSpire.Height);
            }

            if (unit != null)
            {
                Point MousePos = PointToClient(MousePosition);
                int x = (MousePos.X - offsetX) / S;
                int y = (MousePos.Y - offsetY) / S;
                if (x >= 0 && x < W && y >= 0 && y < H)
                {
                    unit.X = x * S + (S - unit.Width) / 2;
                    unit.Y = y * S + (S - unit.Height) / 2;

                    if ((unit as Unit) != null)
                    {
                        if ((unit as Unit).IndUnit < 3)
                        {
                            e.Graphics.FillRectangle(Brushes.Black, (int)unit.X + offsetX,
                        (int)unit.Y + offsetY, unit.Width, unit.Height);
                        }
                        else if ((unit as Unit).IndUnit == 3)
                        {
                            e.Graphics.FillEllipse(Brushes.Black, (int)unit.X + offsetX,
                        (int)unit.Y + offsetY, unit.Width, unit.Height);
                        }
                    }
                    else
                    {
                        e.Graphics.FillRectangle(Brushes.Black, (int)unit.X + offsetX,
                       (int)unit.Y + offsetY, unit.Width, unit.Height);
                    }
                }
            }

            string playerS = null;
            if (turnFirst)
            {
                playerS = "Первый игрок";
            }
            else
            {
                playerS = "Второй игрок";
            }
            e.Graphics.DrawString(playerS, new Font("Times New Roman", 12, FontStyle.Bold),
                    Brushes.Black, new PointF(W * S + S / 2 + offsetX, S / 2 + offsetY));
            e.Graphics.DrawString($"Деньги: {player.Money}", new Font("Times New Roman", 12, FontStyle.Bold),
                    Brushes.Black, new PointF(W * S + S / 2 + offsetX, S / 2 + S + offsetY));
        }

        private void Deploy_MouseClick(object sender, MouseEventArgs e)
        {
            if (unit == null || unit.X < 0 || unit.Y < 0) { return; }
            int x = (int)unit.GetCenterX() / S;
            int y = (int)unit.GetCenterY() / S;
            Cell cell = cells[y][x];
            BaseUnit delUnit = null;
            if (cell.Spire != null) { return; }

            if (e.Button == MouseButtons.Left)
            {
                if (turnFirst && (int)unit.Y / S >= 0 && (int)unit.Y / S < H &&
                    (int)unit.X / S >= 0 && (int)unit.X / S < W)
                {
                    if (((int)unit.Y / S < H / 4 && ((int)unit.X / S < 2 || (int)unit.X / S >= W - 2)) ||
                        ((int)unit.Y / S >= H / 2 && (int)unit.Y / S < H - 6 && ((int)unit.X / S >= 8 && (int)unit.X / S < W - 8)) ||
                        ((int)unit.Y / S <= 1 && ((int)unit.X / S < W / 3 + W / 4 && (int)unit.X / S >= W - W / 3 - W / 4)))
                    {
                        if (cell.Units.Count > 0) { return; }
                        if (!BuyUnit(delUnit)) { return; }

                        if ((unit as Unit) != null)
                        {
                            player.Units.Add(new Unit((unit as Unit)));
                        }
                        else
                        {
                            player.Units.Add(new SpireSpawn((unit as SpireSpawn)));
                        }
                        cell.Units.Add(player.Units[player.Units.Count - 1]);
                    }
                }
                else if (!turnFirst && (int)unit.Y / S >= 0 && (int)unit.Y / S < H &&
                    (int)unit.X / S >= 0 && (int)unit.X / S < W)
                {
                    if (((int)unit.Y / S >= H - H / 4 && ((int)unit.X / S < 2 || (int)unit.X / S >= W - 2)) ||
                        ((int)unit.Y / S < H / 2 && (int)unit.Y / S > 5 && ((int)unit.X / S >= 8 && (int)unit.X / S < W - 8)) ||
                        ((int)unit.Y / S >= H - 2 && ((int)unit.X / S < W / 3 + W / 4 && (int)unit.X / S >= W - W / 3 - W / 4)))
                    {
                        if (cell.Units.Count > 0) { return; }
                        if (!BuyUnit(delUnit)) { return; }

                        if ((unit as Unit) != null)
                        {
                            player.Units.Add(new Unit((unit as Unit)));
                        }
                        else
                        {
                            player.Units.Add(new SpireSpawn((unit as SpireSpawn)));
                        }
                        cell.Units.Add(player.Units[player.Units.Count - 1]);
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (turnFirst && (int)unit.Y / S >= 0 && (int)unit.Y / S < H &&
                    (int)unit.X / S >= 0 && (int)unit.X / S < W)
                {
                    if (((int)unit.Y / S < H / 4 && ((int)unit.X / S < 2 || (int)unit.X / S >= W - 2)) ||
                        ((int)unit.Y / S >= H / 2 && (int)unit.Y / S < H - 6 && ((int)unit.X / S >= 8 && (int)unit.X / S < W - 8)) ||
                        ((int)unit.Y / S <= 1 && ((int)unit.X / S < W / 3 + W / 4 && (int)unit.X / S >= W - W / 3 - W / 4)))
                    {
                        if (cell.Units.Count > 0)
                        {
                            player.Money += SumForDelUnit(cell.Units[0]);
                            cell.Units.Clear();
                            DelUnitPlayer();
                        }
                    }
                }
                else if (!turnFirst && (int)unit.Y / S >= 0 && (int)unit.Y / S < H &&
                    (int)unit.X / S >= 0 && (int)unit.X / S < W)
                {
                    if (((int)unit.Y / S >= H - H / 4 && ((int)unit.X / S < 2 || (int)unit.X / S >= W - 2)) ||
                        ((int)unit.Y / S < H / 2 && (int)unit.Y / S > 5 && ((int)unit.X / S >= 8 && (int)unit.X / S < W - 8)) ||
                        ((int)unit.Y / S >= H - 2 && ((int)unit.X / S < W / 3 + W / 4 && (int)unit.X / S >= W - W / 3 - W / 4)))
                    {
                        if (cell.Units.Count > 0)
                        {
                            player.Money += SumForDelUnit(cell.Units[0]);
                            cell.Units.Clear();
                            DelUnitPlayer();
                        }
                    }
                }
            }

            Invalidate();
        }
        private void Deploy_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (unit == null || unit.X < 0 || unit.Y < 0) { return; }
            int x = (int)unit.GetCenterX() / S;
            int y = (int)unit.GetCenterY() / S;
            Cell cell = cells[y][x];
            BaseUnit delUnit = null;
            if (cell.Spire != null) { return; }

            if (e.Button == MouseButtons.Left)
            {
                if (turnFirst && (int)unit.Y / S >= 0 && (int)unit.Y / S < H &&
                    (int)unit.X / S >= 0 && (int)unit.X / S < W)
                {
                    if (((int)unit.Y / S < H / 4 && ((int)unit.X / S < 2 || (int)unit.X / S >= W - 2)) ||
                        ((int)unit.Y / S >= H / 2 && (int)unit.Y / S < H - 6 && ((int)unit.X / S >= 8 && (int)unit.X / S < W - 8)) ||
                        ((int)unit.Y / S <= 1 && ((int)unit.X / S < W / 3 + W / 4 && (int)unit.X / S >= W - W / 3 - W / 4)))
                    {
                        if (cell.Units.Count > 0) { delUnit = cell.Units[0]; }
                        if (!BuyUnit(delUnit)) { return; }

                        if (cell.Units.Count > 0)
                        {
                            cell.Units.Clear();
                            DelUnitPlayer();
                        }
                        if ((unit as Unit) != null)
                        {
                            player.Units.Add(new Unit((unit as Unit)));
                        }
                        else
                        {
                            player.Units.Add(new SpireSpawn((unit as SpireSpawn)));
                        }
                        cell.Units.Add(player.Units[player.Units.Count - 1]);
                    }
                }
                else if (!turnFirst && (int)unit.Y / S >= 0 && (int)unit.Y / S < H &&
                    (int)unit.X / S >= 0 && (int)unit.X / S < W)
                {
                    if (((int)unit.Y / S >= H - H / 4 && ((int)unit.X / S < 2 || (int)unit.X / S >= W - 2)) ||
                        ((int)unit.Y / S < H / 2 && (int)unit.Y / S > 5 && ((int)unit.X / S >= 8 && (int)unit.X / S < W - 8)) ||
                        ((int)unit.Y / S >= H - 2 && ((int)unit.X / S < W / 3 + W / 4 && (int)unit.X / S >= W - W / 3 - W / 4)))
                    {
                        if (cell.Units.Count > 0) { delUnit = cell.Units[0]; }
                        if (!BuyUnit(delUnit)) { return; }
                        unit.IndPlayer = player.Ind;
                        if (cell.Units.Count > 0)
                        {
                            cell.Units.Clear();
                            DelUnitPlayer();
                        }
                        if ((unit as Unit) != null)
                        {
                            player.Units.Add(new Unit((unit as Unit)));
                        }
                        else
                        {
                            player.Units.Add(new SpireSpawn((unit as SpireSpawn)));
                        }
                        cell.Units.Add(player.Units[player.Units.Count - 1]);
                    }
                }
            }
        }
        private void DelUnitPlayer()
        {
            for (int i = 0; i < player.Units.Count; i++)
            {
                if ((int)player.Units[i].X / S == (int)unit.X / S &&
                    (int)player.Units[i].Y / S == (int)unit.Y / S)
                {
                    player.Units.RemoveAt(i);
                    break;
                }
            }
        }
        private int SumForDelUnit(BaseUnit delUnit)
        {
            int sum = 0;
            if ((delUnit as Unit) != null)
            {
                if ((delUnit as Unit).IndUnit == 0)
                {
                    sum = costLight / 2;
                }
                else if ((delUnit as Unit).IndUnit == 1)
                {
                    sum = costMedium / 2;
                }
                else if ((delUnit as Unit).IndUnit == 2)
                {
                    sum = costHeavy / 2;
                }
                else if ((delUnit as Unit).IndUnit == 3)
                {
                    sum = costDestroyer / 2;
                }
            }
            else if ((delUnit as SpireSpawn) != null)
            {
                sum = costSpireLight / 2;
            }
            return sum;
        }

        private void rbLight_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLight.Checked)
            {
                int ind = 0;
                if (turnFirst)
                {
                    ind = 0;
                }
                else
                {
                    ind = 1;
                }
                int time = TimeTranslation(300);
                unit = new Unit(ind, 0, -1, -1, (int)(0.4 * S), (int)(0.4 * S), 8, 7, (int)(S * 1.25), 1, time);
            }
        }
        private void rbMedium_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMedium.Checked)
            {
                int ind = 0;
                if (turnFirst)
                {
                    ind = 0;
                }
                else
                {
                    ind = 1;
                }
                int time = TimeTranslation(550);
                unit = new Unit(ind, 1, -1, -1, (int)(0.55 * S), (int)(0.55 * S), 18, 5, S * 2, 2, time);
            }
        }
        private void rbHeavy_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHeavy.Checked)
            {
                int ind = 0;
                if (turnFirst)
                {
                    ind = 0;
                }
                else
                {
                    ind = 1;
                }
                int time = TimeTranslation(1050);
                unit = new Unit(ind, 2, -1, -1, (int)(0.7 * S), (int)(0.7 * S), 32, 4, (int)(S * 3.25), 6, time);
            }
        }
        private void rbDestroyer_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDestroyer.Checked)
            {
                int ind = 0;
                if (turnFirst)
                {
                    ind = 0;
                }
                else
                {
                    ind = 1;
                }
                int time = TimeTranslation(1400);
                unit = new Unit(ind, 3, -1, -1, (int)(0.7 * S), (int)(0.7 * S), 13, 3, (int)(S * 4.35), 17, time);
            }
        }
        private void rbSpireLight_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSpireLight.Checked)
            {
                int ind = 0;
                if (turnFirst)
                {
                    ind = 0;
                }
                else
                {
                    ind = 1;
                }
                int time = TimeTranslation(300);
                Unit tmpUnit = new Unit(ind, 0, -1, -1, (int)(0.4 * S), (int)(0.4 * S), 8, 7, (int)(S * 1.25), 1, time);

                time = TimeTranslation(1000);
                unit = new SpireSpawn(ind, -1, -1, S, S, 25, time, tmpUnit, 7);
            }
        }

        private bool BuyUnit(BaseUnit delUnit)
        {
            int sum = SumForDelUnit(delUnit);

            if (rbLight.Checked && player.Money >= costLight - sum)
            {
                player.Money += sum;
                player.Money -= costLight;
                return true;
            }
            else if (rbMedium.Checked && player.Money >= costMedium - sum)
            {
                player.Money += sum;
                player.Money -= costMedium;
                return true;
            }
            else if (rbHeavy.Checked && player.Money >= costHeavy - sum)
            {
                player.Money += sum;
                player.Money -= costHeavy;
                return true;
            }
            else if (rbDestroyer.Checked && player.Money >= costDestroyer - sum)
            {
                player.Money += sum;
                player.Money -= costDestroyer;
                return true;
            }
            else if (rbSpireLight.Checked && player.Money >= costSpireLight - sum)
            {
                player.Money += sum;
                player.Money -= costSpireLight;
                return true;
            }
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (turnFirst)
            {
                unit = null;
                rbMedium.Checked = false;
                rbHeavy.Checked = false;
                rbLight.Checked = false;

                newPlayers.Add(new Player(player));
                turnFirst = !turnFirst;
                player = new Player(players[1]);
                player.Money += money;
                rbLight.Checked = true;
            }
            else
            {
                newPlayers.Add(new Player(player));
                openMenu = false;
                Close();
            }
        }
    }
}
