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
    public partial class myMenu : Form
    {
        int H;
        int W;
        int S;
        int offsetX;
        int offsetY;
        public myMenu()
        {
            InitializeComponent();
        }

        private void myMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Player> players = new List<Player>();

            players.Add(new Player(0, 450, 300));
            players.Add(new Player(1, 450, 300));
            players[0].Spires.Add(new Spire(0, (W / 4) * S,
                (H - H / 4 - 1) * S, S, S, 60));
            players[0].Spires.Add(new Spire(0, (W - W / 4 - 1) * S,
                (H - H / 4 - 1) * S, S, S, 60));

            players[1].Spires.Add(new Spire(1, (W / 4) * S,
                (H / 4) * S, S, S, 60));
            players[1].Spires.Add(new Spire(1, (W - W / 4 - 1) * S,
                (H / 4) * S, S, S, 60));

            Deploy deploy = new Deploy(this, 0, players, W, H, S, offsetX, offsetY);
            deploy.Show();
            Hide();
        }

        private void myMenu_Shown(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.Sizable;

            H = 32;
            W = 40;

            S = ClientSize.Height / H;
            offsetX = (ClientSize.Width - S * W) / 2;
            offsetY = (ClientSize.Height % S) / 2;
        }
    }
}
