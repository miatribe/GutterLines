using System;
using System.Drawing;
using System.Windows.Forms;

namespace GutterLines
{
    public partial class Window : Form
    {
        private MemRead memRead;
        private int lat;
        private int lon;
        private const int gridScale = 4;
        private const int lineOffset = gridScale / 2;
        private const int gridMax = gridScale * 40;
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [STAThread]
        static void Main()
        {
            Application.Run(new Window());
        }

        public Window()
        {
            InitializeComponent();
            memRead = new MemRead();
            memRead.GetProcess();
            var Timer = new Timer()
            {
                Interval = (250)
            };
            Timer.Tick += new EventHandler(UpdateWindow);
            Timer.Start();
        }

        private void UpdateWindow(object sender, EventArgs e)
        {
            var gi = memRead.GetValues();
            if (gi != null)
            {
                LatLonLbl.Text = $"{gi.Name} @ {gi.Lat},{gi.Lon}";
                if (lat != gi.Lat || lon != gi.Lon)
                {
                    lat = gi.Lat;
                    lon = gi.Lon;
                    DrawGutters(gi.Lat, gi.Lon);
                }
            }
            else
            {
                LatLonLbl.Text = "Unable to find data";
            }
        }

        private void DrawGutters(int playerX, int playerY)
        {
            Graphics g = gridMap.CreateGraphics();
            g.Clear(gridMap.BackColor);
            //get xGutter
            int xGutterGridR1 = GetGutterLinePosX(playerX, 0) * gridScale;
            int xGutterGridB1 = GetGutterLinePosX(playerX, 1) * gridScale;
            int xGutterGridB2 = GetGutterLinePosX(playerX, 2) * gridScale;
            int xGutterGridB3 = GetGutterLinePosX(playerX, 3) * gridScale;
            int xGutterGridB4 = GetGutterLinePosX(playerX, 4) * gridScale;
            //get yGutter
            int yGutterGridR1 = GetGutterLinePosY(playerY, 0) * gridScale;
            int yGutterGridB1 = GetGutterLinePosY(playerY, -1) * gridScale;
            int yGutterGridB2 = GetGutterLinePosY(playerY, -2) * gridScale;
            int yGutterGridB3 = GetGutterLinePosY(playerY, -3) * gridScale;
            int yGutterGridB4 = GetGutterLinePosY(playerY, -4) * gridScale;
            //draw x blue gutters
            g.DrawLine(new Pen(Brushes.Blue, gridScale), xGutterGridB1, 0, xGutterGridB1, gridMax);
            g.DrawLine(new Pen(Brushes.Blue, gridScale), xGutterGridB2, 0, xGutterGridB2, gridMax);
            g.DrawLine(new Pen(Brushes.Blue, gridScale), xGutterGridB3, 0, xGutterGridB3, gridMax);
            g.DrawLine(new Pen(Brushes.Blue, gridScale), xGutterGridB4, 0, xGutterGridB4, gridMax);
            //draw y blue gutters
            g.DrawLine(new Pen(Brushes.Blue, gridScale), 0, yGutterGridB1, gridMax, yGutterGridB1);
            g.DrawLine(new Pen(Brushes.Blue, gridScale), 0, yGutterGridB2, gridMax, yGutterGridB2);
            g.DrawLine(new Pen(Brushes.Blue, gridScale), 0, yGutterGridB3, gridMax, yGutterGridB3);
            g.DrawLine(new Pen(Brushes.Blue, gridScale), 0, yGutterGridB4, gridMax, yGutterGridB4);
            //draw x red gutter
            g.DrawLine(new Pen(Brushes.Red, gridScale), xGutterGridR1, 0, xGutterGridR1, gridMax);
            //draw y red gutter
            g.DrawLine(new Pen(Brushes.Red, gridScale), 0, yGutterGridR1, gridMax, yGutterGridR1);
            //draw players dot
            g.FillRectangle(Brushes.Black, 20 * gridScale - lineOffset, 20 * gridScale - lineOffset, gridScale, gridScale);
        }

        private int GetGutterLinePosX(int playerX, int mod)
        {
            int Gutter = playerX + (40 - (playerX % 40));
            if (Gutter + mod > playerX + 20)
            {
                Gutter = playerX - (playerX % 40);
            }
            return Gutter - playerX + 20 + mod;
        }

        private int GetGutterLinePosY(int playerY, int mod)
        {
            int Gutter = playerY + (40 - (playerY % 40));
            if (Gutter - mod > playerY + 20)
            {
                Gutter = playerY - (playerY % 40);
            }
            return playerY - Gutter + 20 + mod;
        }

        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NextClientBtn_Click(object sender, EventArgs e)
        {
            memRead.GetProcess();
        }
    }
}

