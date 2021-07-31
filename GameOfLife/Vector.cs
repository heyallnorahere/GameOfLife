using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GameOfLife
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector : IEnumerable<int>, IEquatable<(int x, int y)>
    {
        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int this[int index]
        {
            get
            {
                return index switch
                {
                    0 => X,
                    1 => Y,
                    _ => throw new IndexOutOfRangeException()
                };
            }
            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
        public IEnumerator<int> GetEnumerator()
        {
            return new Enumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public bool Equals((int x, int y) other)
        {
            throw new NotImplementedException();
        }
        public override bool Equals(object? obj)
        {
            if (obj is Vector vector)
            {
                return this == vector;
            }
            return false;
        }
        public override int GetHashCode() => HashCode.Combine(X, Y);
        public static implicit operator Vector((int x, int y) tuple) => new(tuple.x, tuple.y);
        public static bool operator==(Vector v1, Vector v2)
        {
            return (v1.X == v2.X) && (v1.Y == v1.Y);
        }
        public static bool operator!=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }
        public static Vector operator+(Vector v1, Vector v2)
        {
            return (v1.X + v2.X, v1.Y + v2.Y);
        }
        public static Vector operator+(Vector v, int scalar)
        {
            return v + (scalar, scalar);
        }
        public static Vector operator-(Vector v)
        {
            return (-v.X, -v.Y);
        }
        public static Vector operator-(Vector v1, Vector v2)
        {
            return v1 + -v2;
        }
        public static Vector operator-(Vector v, int scalar)
        {
            return v + -scalar;
        }
        public static Vector operator*(Vector v, int scalar)
        {
            return (v.X * scalar, v.Y * scalar);
        }
        public static Vector operator/(Vector v, int scalar)
        {
            return (v.X / scalar, v.Y / scalar);
        }
        private struct Enumerator : IEnumerator<int>
        {
            public int Current => mVector[mIndex];
            object IEnumerator.Current => Current;
            public Enumerator(Vector vector)
            {
                mVector = vector;
                mIndex = -1;
            }
            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
            public bool MoveNext()
            {
                if (mIndex < 1)
                {
                    mIndex++;
                    return true;
                }
                return false;
            }
            public void Reset()
            {
                mIndex = -1;
            }
            private Vector mVector;
            private int mIndex;
        }
    }
}
