using Microsoft.Win32;
using System;
using System.ComponentModel;
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
        private MenuItem alertToggle;
        private bool flashAlert;

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
            CreateContextMenu();
            StartPosition = FormStartPosition.Manual;
            SetWindowPos();
            BackColor = Color.Pink;
            TransparencyKey = Color.Pink;
            memRead = new MemRead();
            memRead.GetProcess();
            var Timer = new Timer()
            {
                Interval = (250)
            };
            Timer.Tick += new EventHandler(UpdateWindow);
            Timer.Start();
        }

        private void CreateContextMenu()
        {
            var notifyIcon = new NotifyIcon(components = new Container())
            {
                Icon = Properties.Resources.icon,
                ContextMenu = new ContextMenu(new[]
                {
                    new MenuItem("Reset Window Position", ResetWindowPos),
                    //alertToggle = new MenuItem($"Show When in Gutter (Current:{GetAlertToggleBool()})", ToggleAlert),
                    new MenuItem("-"),
                    new MenuItem("Exit GutterLines", ExitBtn_Click)
                }),
                Text = "GutterLines",
                Visible = true
            };
        }

        private void ToggleAlert(object sender, EventArgs e)
        {
            flashAlert = !flashAlert;
            using (var key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\ByTribe\GutterLines"))
            {
                try
                {
                    key?.SetValue("alertToggle", flashAlert);
                }
                catch {/* If we cant write to the registry do nothing*/}
            }
            alertToggle.Text = $"Show When in Gutter (Current:{flashAlert})";
        }

        private bool GetAlertToggleBool()
        {
            using (var key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\ByTribe\GutterLines"))
            {
                try
                {
                    return Convert.ToBoolean(key?.GetValue("alertToggle"));
                }
                catch { return true; }
            }
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

        private int GetGutterLinePos(int playerAxisPos, int mod)
        {
            int Gutter = playerAxisPos + (40 - (playerAxisPos % 40));
            if (Gutter + mod > playerAxisPos + 20)
            {
                Gutter = playerAxisPos - (playerAxisPos % 40);
            }
            return Gutter - playerAxisPos + 20 + mod;
        }



        private void SetWindowPos()
        {
            using (var key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\ByTribe\GutterLines"))
            {
                try
                {
                    Location = new Point((int)key?.GetValue("winX"), (int)key?.GetValue("winY"));
                }
                catch (Exception)
                {
                    Location = new Point(10, 10);
                }
            }
        }

        private void ResetWindowPos(object sender, EventArgs e)
        {
            SaveWindowPos(10, 10);
            SetWindowPos();
        }

        private void SaveWindowPos(int winX, int winY)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\ByTribe\GutterLines"))
            {
                try
                {
                    key?.SetValue("winX", winX);
                    key?.SetValue("winY", winY);
                }
                catch {/* If we cant write to the registry do nothing*/}
            }
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
        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveWindowPos(Location.X, Location.Y);
        }
        #endregion


    }
}

