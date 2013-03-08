using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CurveWinForms
{
    public struct VectorF : IEquatable<VectorF>
    {
        private float x, y;
        private double len;

        public VectorF(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.len = CalculateLength(x, y);
        }

        public float X
        {
            get { return x; }
            set { x = value; this.len = CalculateLength(this); }
        }
        public float Y
        {
            get { return y; }
            set { y = value; this.len = CalculateLength(this); }
        }

        public double Length
        {
            get { return len; }
        }

        private static double CalculateLength(float x, float y)
        {
            return Math.Sqrt(x * x + y * y);
        }
        private static double CalculateLength(VectorF a)
        {
            return CalculateLength(a.x, a.y);
        }

        public static VectorF operator +(VectorF a, VectorF b)
        {
            return new VectorF(a.x + b.x, a.y + b.y);
        }
        public static VectorF operator -(VectorF a, VectorF b)
        {
            return new VectorF(a.x - b.x, a.y - b.y);
        }
        public static float operator *(VectorF a, VectorF b)
        {
            return a.x * b.x + a.y * b.y;
        }
        public static VectorF operator *(VectorF a, float b)
        {
            return new VectorF(a.x * b, a.y * b);
        }
        public static VectorF operator ~(VectorF a)
        {
            return OrthogonalCounterClockwise(a);
        }

        public static bool operator ==(VectorF a, VectorF b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(VectorF a, VectorF b)
        {
            return !a.Equals(b);
        }

        public static VectorF OrthogonalCounterClockwise(VectorF a)
        {
            return new VectorF(-a.y, a.x);
        }
        public static VectorF OrthogonalClockwise(VectorF a)
        {
            return new VectorF(a.y, -a.x);
        }

        public static implicit operator PointF(VectorF a)
        {
            return new PointF(a.x, a.y);
        }
        public static explicit operator SizeF(VectorF a)
        {
            return new SizeF(a.x, a.y);
        }
        public static explicit operator VectorF(PointF a)
        {
            return new VectorF(a.X, a.Y);
        }
        public static explicit operator VectorF(SizeF a)
        {
            return new VectorF(a.Width, a.Height);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            else if (obj is VectorF)
                return Equals((VectorF)obj);
            else
                return false;
        }
        public bool Equals(VectorF other)
        {
            return this.x == other.x && this.y == other.y;
        }
        public override int GetHashCode()
        {
            return BitConverter.ToInt32(BitConverter.GetBytes(x), 0) ^ BitConverter.ToInt32(BitConverter.GetBytes(y), 0);
        }
    }
}