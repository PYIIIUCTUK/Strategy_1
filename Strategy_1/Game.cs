using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Strategy_1
{
    public partial class Game : Form
    {
        myMenu menu;
        bool openMenu = true;
        int money;

        Timer timer = new Timer();

        int S, W, H;
        int offsetX;
        int offsetY;

        List<Player> players;
        List<List<BaseUnit>> units = new List<List<BaseUnit>>();

        List<Bullet> bullets = new List<Bullet>();

        public Game(myMenu Menu, int Money, List<Player> Players, int w, int h, int s, int offx, int offy)
        {
            InitializeComponent();

            menu = Menu;
            players = Players;
            money = Money;
            H = h; W = w; S = s; offsetX = offx; offsetY = offy;
        }

        private void Game_Shown(object sender, EventArgs e)
        {
            timer.Interval = 70;
            timer.Tick += Timer_Tick;
            timer.Start();

            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.Sizable;

            units.Add(new List<BaseUnit>());
            units.Add(new List<BaseUnit>());
            for (int j = 0; j < players.Count; j++)
            {
                for (int i = 0; i < players[j].Spires.Count; i++)
                {
                    units[j].Add(new Spire(players[j].Spires[i]));
                }
                for (int i = 0; i < players[j].Units.Count; i++)
                {
                    if ((players[j].Units[i] as Unit) != null)
                    {
                        units[j].Add(new Unit((players[j].Units[i] as Unit)));
                    }
                    else
                    {
                        units[j].Add(new SpireSpawn((players[j].Units[i] as SpireSpawn)));
                    }
                }
            }
        }
        private void Game_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Stop();
            if (openMenu)
            {
                menu.Show();
            }
            else
            {
                if (CheckEndGame())
                {
                    menu.Show();
                }
                else
                {
                    Deploy deploy = new Deploy(menu, money, players, W, H, S, offsetX, offsetY);
                    deploy.Show();
                }
            }
        }

        private void Game_Paint(object sender, PaintEventArgs e)
        {
            Unit tmpUnit;
            Brush tmpBrush;
            Brush tmpHealth;
            int heightHP = 0;
            int lengthHP = 0;

            for (int i = 0; i < units.Count; i++)
            {
                if (i == 0) { tmpBrush = Brushes.Green; }
                else { tmpBrush = Brushes.Red; }
                for (int j = 0; j < units[i].Count; j++)
                {
                    tmpHealth = Brushes.Black;

                    tmpUnit = (units[i][j] as Unit);
                    if (tmpUnit != null)
                    {
                        if (tmpUnit.IndUnit < 3)
                        {
                            e.Graphics.FillRectangle(tmpBrush, (int)units[i][j].X + offsetX,
                                   (int)units[i][j].Y + offsetY, units[i][j].Width, units[i][j].Height);
                            e.Graphics.DrawRectangle(Pens.Black, (int)units[i][j].X + offsetX,
                                       (int)units[i][j].Y + offsetY, units[i][j].Width, units[i][j].Height);
                        }
                        else if (tmpUnit.IndUnit == 3)
                        {
                            e.Graphics.FillEllipse(tmpBrush, (int)units[i][j].X + offsetX,
                                   (int)units[i][j].Y + offsetY, units[i][j].Width, units[i][j].Height);
                            e.Graphics.DrawEllipse(Pens.Black, (int)units[i][j].X + offsetX,
                                       (int)units[i][j].Y + offsetY, units[i][j].Width, units[i][j].Height);
                        }
                    }
                    else if ((units[i][j] as SpireSpawn) != null)
                    {
                        e.Graphics.FillRectangle(tmpBrush, (int)units[i][j].X + offsetX,
                                   (int)units[i][j].Y + offsetY, units[i][j].Width, units[i][j].Height);
                        e.Graphics.DrawRectangle(Pens.Black, (int)units[i][j].X + offsetX,
                                   (int)units[i][j].Y + offsetY, units[i][j].Width, units[i][j].Height);
                    }
                    else if ((units[i][j] as Spire) != null)
                    {
                        if (i == 0) { tmpHealth = Brushes.Green; }
                        else { tmpHealth = Brushes.Red; }
                        e.Graphics.FillRectangle(Brushes.Black, (int)units[i][j].X + offsetX,
                        (int)units[i][j].Y + offsetY, units[i][j].Width, units[i][j].Height);
                    }

                    heightHP = (int)(units[i][j].Height * 0.4);
                    lengthHP = (int)((units[i][j].Health / (double)units[i][j].maxHealth) * (units[i][j].Width - 1));
                    e.Graphics.FillRectangle(tmpHealth, (int)units[i][j].X + 1 + offsetX,
                                (int)units[i][j].GetCenterY() - heightHP / 2 + offsetY, lengthHP, heightHP);
                }
            }
            e.Graphics.DrawRectangle(Pens.Black, offsetX, offsetY, S * W, S * H);

            Pen pen = new Pen(Color.Orange, 5);
            for (int i = 0; i < bullets.Count; i++)
            {
                e.Graphics.DrawLine(pen, (int)bullets[i].X1 + offsetX, (int)bullets[i].Y1 + offsetY,
                     (int)bullets[i].X2 + offsetX, (int)bullets[i].Y2 + offsetY);
            }

            e.Graphics.DrawString($"Второй игрок", new Font("Times New Roman", 12, FontStyle.Bold),
                    Brushes.Black, new PointF(W * S + S / 2 + offsetX, S / 2 + S * 6 + offsetY));
            e.Graphics.DrawString($"HP: {players[1].Health}", new Font("Times New Roman", 12, FontStyle.Bold),
                    Brushes.Black, new PointF(W * S + S / 2 + offsetX, S / 2 + S * 7 + offsetY));

            e.Graphics.DrawString($"Первый игрок", new Font("Times New Roman", 12, FontStyle.Bold),
                    Brushes.Black, new PointF(W * S + S / 2 + offsetX, S / 2 + S * 12 + offsetY));
            e.Graphics.DrawString($"HP: {players[0].Health}", new Font("Times New Roman", 12, FontStyle.Bold),
                    Brushes.Black, new PointF(W * S + S / 2 + offsetX, S / 2 + S * 13 + offsetY));
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            bullets.Clear();
            List<BaseUnit> delUnits = new List<BaseUnit>();
            BaseUnit enemy;
            Unit unit;
            SpireSpawn spire;
            int indEnemy;

            for (int i = 0; i < units.Count; i++)
            {
                if (i == 0) { indEnemy = 1; }
                else { indEnemy = 0; }

                for (int j = 0; j < units[i].Count; j++)
                {
                    enemy = null;
                    unit = (units[i][j] as Unit);
                    if (unit != null)
                    {
                        unit.Reload();

                        if (unit.Enemy == null || unit.Enemy.Health <= 0)
                        {
                            enemy = NearEnemy(unit, units[indEnemy]);
                            unit.SetEnemy(enemy);
                        }

                        if (unit.AttackEnemy())
                        {
                            bullets.Add(new Bullet(unit, unit.Enemy));
                            if (unit.Enemy.Health <= 0)
                            {
                                delUnits.Add(unit.Enemy);
                                unit.Enemy = null;
                            }
                        }
                    }
                    else if ((units[i][j] as SpireSpawn) != null)
                    {
                        spire = (units[i][j] as SpireSpawn);
                        if (spire.CountSpawn <= 0)
                        {
                            delUnits.Add(spire);
                        }
                        else
                        {
                            spire.Reload();
                            if (spire.TimeSpawn <= 0)
                            {
                                enemy = NearEnemy(spire, units[indEnemy]);

                                spire.SetPoint((int)enemy.GetCenterX(), (int)enemy.GetCenterY());
                                Unit spawnUnit = spire.Spawn();
                                if (spawnUnit != null)
                                {
                                    BaseUnit collUnit = CheckCollision(spawnUnit, -1);
                                    if (collUnit != null)
                                    {
                                        spire.ChangePoint();
                                        spawnUnit = spire.Spawn();
                                        if (spawnUnit != null)
                                        {
                                            collUnit = CheckCollision(spawnUnit, -1);
                                        }
                                    }

                                    if (collUnit == null)
                                    {
                                        units[i].Add(spawnUnit);
                                        spire.CountSpawn--;
                                        spire.TimeSpawn = spire.TimeBreakSpawn;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < units.Count; i++)
            {
                if (i == 0) { indEnemy = 1; }
                else { indEnemy = 0; }

                for (int j = 0; j < units[i].Count; j++)
                {
                    unit = (units[i][j] as Unit);
                    if (unit == null) { continue; }

                    if (!unit.CheckRadiusAttack())
                    {
                        if (unit.Enemy == null) { continue; }

                        double x = unit.Enemy.GetCenterX();
                        double y = unit.Enemy.GetCenterY();

                        double preX = unit.X;
                        double preY = unit.Y;

                        unit.SetPoint((int)x, (int)y);
                        unit.Move();

                        BaseUnit collUnit = CheckCollision(unit, j);
                        if (collUnit != null)
                        {
                            if (Math.Abs(Math.Abs(preY - unit.Y) - Math.Abs(preX - unit.X)) <= 1.5)
                            {

                                if (preY - unit.Y <= 0)
                                {
                                    if (preX - unit.X <= 0)
                                    {
                                        unit.X = preX - unit.Speed / 2.0;
                                        unit.Y = preY + unit.Speed / 2.0;
                                    }
                                    else
                                    {
                                        unit.X = preX - unit.Speed / 2.0;
                                        unit.Y = preY - unit.Speed / 2.0;
                                    }
                                }
                                else
                                {
                                    if (preX - unit.X <= 0)
                                    {
                                        unit.X = preX + unit.Speed / 2.0;
                                        unit.Y = preY + unit.Speed / 2.0;
                                    }
                                    else
                                    {
                                        unit.X = preX + unit.Speed / 2.0;
                                        unit.Y = preY - unit.Speed / 2.0;
                                    }
                                }

                            }
                            else if (Math.Abs(preY - unit.Y) > Math.Abs(preX - unit.X))
                            {

                                if (preY - unit.Y >= 0)
                                {
                                    unit.X = preX + unit.Speed;
                                }
                                else
                                {
                                    unit.X = preX - unit.Speed;
                                }
                                unit.Y = preY;

                            }
                            else
                            {

                                if (preX - unit.X <= 0)
                                {
                                    unit.Y = preY + unit.Speed;
                                }
                                else
                                {
                                    unit.Y = preY - unit.Speed;
                                }
                                unit.X = preX;

                            }
                            collUnit = CheckCollision(unit, j);


                            if (collUnit != null)
                            {
                                for (int k = 0; k < 4; k++)
                                {
                                    unit.Interact(collUnit);
                                    collUnit = CheckCollision(unit, j);
                                    if (collUnit == null) { break; }
                                }
                            }

                            if (collUnit != null)
                            {
                                unit.X = preX;
                                unit.Y = preY;
                            }
                        }

                        unit.Enemy = null;
                    }
                }
            }

            for (int i = 0; i < delUnits.Count; i++)
            {
                if ((delUnits[i] as Spire) != null)
                {
                    DestroySpire(delUnits[i].IndPlayer, delUnits);
                    units[delUnits[i].IndPlayer].Remove((delUnits[i] as Spire));
                }
                else if ((delUnits[i] as Unit) != null)
                {
                    units[delUnits[i].IndPlayer].Remove((delUnits[i] as Unit));
                }
                else if ((delUnits[i] as SpireSpawn) != null)
                {
                    units[delUnits[i].IndPlayer].Remove((delUnits[i] as SpireSpawn));
                }
            }

            Invalidate();
            CheckWin();
        }

        private BaseUnit NearEnemy(BaseUnit unit, List<BaseUnit> enemies)
        {
            BaseUnit enemy = null;
            double distance;
            double x, y;

            double minDistanceE = 0;
            int indEnemy = -1;

            for (int i = 0; i < enemies.Count; i++)
            {
                x = unit.GetCenterX() - enemies[i].GetCenterX();
                y = unit.GetCenterY() - enemies[i].GetCenterY();
                distance = Math.Sqrt(x * x + y * y);

                if (i == 0 || minDistanceE > distance)
                {
                    minDistanceE = distance;
                    indEnemy = i;
                }
            }

            if (indEnemy != -1)
            {
                if ((enemies[indEnemy] as Spire) != null)
                {
                    enemy = (enemies[indEnemy] as Spire);
                }
                else if ((enemies[indEnemy] as Unit) != null)
                {
                    enemy = (enemies[indEnemy] as Unit);
                }
                else if ((enemies[indEnemy] as SpireSpawn) != null)
                {
                    enemy = (enemies[indEnemy] as SpireSpawn);
                }
            }
            return enemy;
        }
        private void DestroySpire(int ind, List<BaseUnit> delUnits)
        {
            BaseUnit unit;
            for (int i = 0; i < units[ind].Count; i++)
            {
                if ((units[ind][i] as Spire) != null) { continue; }
                unit = units[ind][i];
                if (unit.Health > 0)
                {
                    unit.Health -= 3;
                    if (unit.Health <= 0)
                    {
                        if ((unit as Unit) != null)
                        {
                            delUnits.Add((unit as Unit));
                        }
                        else if ((unit as SpireSpawn) != null)
                        {
                            delUnits.Add((unit as SpireSpawn));
                        }
                    }
                }
            }
        }

        private BaseUnit CheckCollision(BaseUnit unit, int ind)
        {
            Rectangle unitBounds;
            Rectangle myBounds = new Rectangle((int)unit.X, (int)unit.Y, unit.Width, unit.Height);
            for (int i = 0; i < units.Count; i++)
            {
                for (int j = 0; j < units[i].Count; j++)
                {
                    if (j == ind) { continue; }

                    unitBounds = new Rectangle((int)units[i][j].X, (int)units[i][j].Y,
                        units[i][j].Width, units[i][j].Height);
                    if (myBounds.IntersectsWith(unitBounds))
                    {
                        return units[i][j];
                    }
                }
            }
            return null;
        }
        private double IntersectionWidth(BaseUnit unit, BaseUnit enemy)
        {
            double intersectionLeft = Math.Max(unit.X, enemy.X);
            double intersectionRight = Math.Min(unit.X + unit.Width, enemy.X + enemy.Width);

            double intersectionWidth = Math.Max(0, intersectionRight - intersectionLeft);

            return intersectionWidth;
        }
        private double IntersectionHeight(BaseUnit unit, BaseUnit enemy)
        {
            double intersectionTop = Math.Max(unit.Y, enemy.Y);
            double intersectionBottom = Math.Min(unit.Y + unit.Height, enemy.Y + enemy.Height);

            double intersectionHeight = Math.Max(0, intersectionBottom - intersectionTop);

            return intersectionHeight;
        }

        private void CheckWin()
        {
            int countFirstSpire = 0;
            int countSecondSpire = 0;
            int count;
            for (int i = 0; i < units.Count; i++)
            {
                count = 0;
                for (int j = 0; j < units[i].Count; j++)
                {
                    if ((units[i][j] as Spire) != null)
                    {
                        count++;
                    }
                }
                if (i == 0)
                {
                    countFirstSpire = count;
                }
                else
                {
                    countSecondSpire = count;
                }
            }

            if (units[0].Count - countFirstSpire <= 0 &&
                units[1].Count - countSecondSpire <= 0)
            {
                timer.Stop();
                MessageBox.Show("Draw!!!");

                openMenu = false;
                Close();
            }
            else if (units[0].Count - countFirstSpire <= 0)
            {
                timer.Stop();
                TakeDamagePlayer(1, 0);
                players[0].Money += 200;
                MessageBox.Show("2 Win!!!");

                openMenu = false;
                Close();
            }
            else if (units[1].Count - countSecondSpire <= 0)
            {
                timer.Stop();
                TakeDamagePlayer(0, 1);
                players[1].Money += 200;
                MessageBox.Show("1 Win!!!");

                openMenu = false;
                Close();
            }
        }
        private bool CheckEndGame()
        {
            if (players[0].Health <= 0 || players[1].Health <= 0)
            {
                return true;
            }
            return false;
        }
        private void TakeDamagePlayer(int win, int lose)
        {
            for (int i = 0; i < units[win].Count; i++)
            {
                if ((units[win][i] as Unit) == null) { continue; }
                players[lose].Health -= (units[win][i] as Unit).IndUnit + 1;
            }
        }
    }
}