using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CurveWinForms
{
    public partial class Form1 : Form
    {
        private List<Player> players = new List<Player>();
        private Player player1 = new Player(50, 50, 45, Color.HotPink);

        public Form1()
        {
            InitializeComponent();

            Application.Idle += delegate { this.Invalidate(); };

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                true);

            players.Add(new Player(0, 0, 0, Color.Green));
            players.Add(new Player(600, 0, 90, Color.Blue));
            players.Add(new Player(600, 600, 180, Color.Red));
            players.Add(new Player(0, 600, 270, Color.Orange));
            players.Add(new Player(300, 0, 0, Color.Green));
            players.Add(new Player(600, 300, 90, Color.Blue));
            players.Add(new Player(300, 600, 180, Color.Red));
            players.Add(new Player(0, 300, 270, Color.Orange));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Enter: player1.Start(); players.ForEach(x => x.Start()); break;
                case Keys.Left: player1.TurnLeft = true; break;
                case Keys.Right: player1.TurnRight = true; break;
                case Keys.A: players.ForEach(x => x.TurnLeft = true); break;
                case Keys.D: players.ForEach(x => x.TurnRight = true); break;
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            switch (e.KeyCode)
            {
                case Keys.Left: player1.TurnLeft = false; break;
                case Keys.Right: player1.TurnRight = false; break;
                case Keys.A: players.ForEach(x => x.TurnLeft = false); break;
                case Keys.D: players.ForEach(x => x.TurnRight = false); break;
            }
        }

        DateTime last = DateTime.Now;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            TimeSpan ts = DateTime.Now - last;
            last = DateTime.Now;
            e.Graphics.DrawString(Math.Round(1 / ts.TotalSeconds).ToString("0") + " FPS", this.Font, Brushes.Black, 0, 0);

            Matrix m = new Matrix();
            m.Scale(((float)this.Height - 40f) / 600f, ((float)this.Height - 40f) / 600f);
            m.Translate(20, 20, MatrixOrder.Append);
            e.Graphics.MultiplyTransform(m);

            //player1.Draw(e.Graphics);
            players.ForEach(x => x.Draw(e.Graphics));

            e.Graphics.ResetTransform();
            e.Graphics.DrawRectangle(Pens.Blue, new Rectangle(20, 20, this.Height - 40, this.Height - 40));
        }
    }
}
