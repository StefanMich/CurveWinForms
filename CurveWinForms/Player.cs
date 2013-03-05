using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CurveWinForms
{
    public class Player
    {
        private const float STANDARD_ANGLE = 210;
        private const float STANDARD_SPEED = 80;
        private List<PointF> points = new List<PointF>();

        private PointF current;
        private float angle = 0;

        private float speed = 80;//pixels pr second
        private float angleAdd = 0;

        private Color color;

        public Player(PointF start, float direction, Color color)
        {
            this.current = start;
            this.points.Add(current);
            this.angle = direction;
            this.color = color;
        }
        public Player(float x, float y, float direction, Color color)
            : this(new PointF(x, y), direction, color)
        {
        }

        public void Start()
        {
            lastTime = DateTime.Now;
        }

        private DateTime lastTime = DateTime.MinValue;
        public void Draw(Graphics graphics)
        {

            if (lastTime == DateTime.MinValue)
                return;

            DateTime currentTime = DateTime.Now;
            TimeSpan diffTime = currentTime - lastTime;
            lastTime = currentTime;

            float len = (float)(speed * diffTime.TotalSeconds);

            angle += (float)(angleAdd * diffTime.TotalSeconds);
            current = new PointF(
                current.X + len * (float)Math.Cos((angle * Math.PI) / 180f),
                current.Y + len * (float)Math.Sin((angle * Math.PI) / 180f));

            if (points.Count < 2 || angleAdd != 0)
                points.Add(current);
            else
                points[points.Count - 1] = current;

            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            if (points.Count >= 2)
                using (Pen pen = new Pen(this.color, 6)
                {
                    StartCap = System.Drawing.Drawing2D.LineCap.Round,
                    LineJoin = System.Drawing.Drawing2D.LineJoin.Round,
                    EndCap = System.Drawing.Drawing2D.LineCap.Round
                })
                    graphics.DrawLines(pen, points.ToArray());
            else
                using (SolidBrush brush = new SolidBrush(this.color))
                    graphics.FillEllipse(brush, current.X - 3, current.Y - 3, 6, 6);

            using (Pen pen = new Pen(Color.FromArgb(128, Color.DodgerBlue), 2))
            {
                graphics.DrawArc(pen, current.X - 5.5f, current.Y - 5.5f, 11, 11, 0, 350);
                graphics.DrawArc(pen, current.X - 9f, current.Y - 9f, 18, 18, 0, 300);
                graphics.DrawArc(pen, current.X - 12.5f, current.Y - 12.5f, 25, 25, 0, 300);
            }
        }

        public bool TurnLeft
        {
            get { return angleAdd < 0; }
            set
            {
                if (value)
                    angleAdd = -STANDARD_ANGLE;
                else if (angleAdd < 0)
                    angleAdd = 0;
            }
        }
        public bool TurnRight
        {
            get { return angleAdd > 0; }
            set
            {
                if (value)
                    angleAdd = STANDARD_ANGLE;
                else if (angleAdd > 0)
                    angleAdd = 0;
            }
        }
    }
}
