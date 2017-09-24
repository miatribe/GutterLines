using System;
using System.Drawing;
using System.Security.Principal;
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

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [STAThread]
        static void Main()
        {
            try
            {
                if ((new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator))
                {
                    Application.Run(new Window());
                }
                else
                {
                    MessageBox.Show("GutterLines must be ran as Administrator.", "Privilege Error");
                    Application.Exit();
                }
            }
            catch
            {
                Application.Exit();
            }
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
            for (int i = 4; i >= 0; i--)
            {
                var gutterPosX = GetGutterLinePos(playerX, i) * gridScale;
                var gutterPosY = GetGutterLinePos(playerY, i) * gridScale;
                Pen pen = new Pen(i == 0 ? Brushes.Red : Brushes.Blue, gridScale);
                g.DrawLine(pen, new Point(gutterPosX, 0), new Point(gutterPosX, gridMax));
                //RO's 0,0 is bottom left, Winforms Picture box's 0,0 is top left. So we flip our Y
                g.DrawLine(pen, new Point(0, gridMax - gutterPosY), new Point(gridMax, gridMax - gutterPosY));
            }
            //draw players dot
            g.FillRectangle(Brushes.Black, 20 * gridScale - lineOffset, 20 * gridScale - lineOffset, gridScale, gridScale);
        }

        private int GetGutterLinePos(int playerX, int mod)
        {
            int Gutter = playerX + (40 - (playerX % 40));
            if (Gutter + mod > playerX + 20)
            {
                Gutter = playerX - (playerX % 40);
            }
            return Gutter - playerX + 20 + mod;
        }

        #region form controls
        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }
        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void NextClientBtn_Click(object sender, EventArgs e)
        {
            memRead.GetProcess();
        }
        #endregion
    }
}

