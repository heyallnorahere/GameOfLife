using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GameOfLife
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector : IEnumerable<int>, IEquatable<(int x, int y)>, ICollection<int>
    {
        public Vector(int x, int y)
        {
            X = x;
            Y = y;
            mAddedCount = 0;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Count => 2;
        public bool IsReadOnly => false;
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
        public void Add(int item)
        {
            if (mAddedCount >= Count)
            {
                throw new IndexOutOfRangeException();
            }
            this[mAddedCount] = item;
            mAddedCount++;
        }
        public void Clear()
        {
            this = (0, 0);
        }
        public bool Contains(int item)
        {
            foreach (int value in this)
            {
                if (item == value)
                {
                    return true;
                }
            }
            return false;
        }
        public void CopyTo(int[] array, int arrayIndex)
        {
            for (int i = arrayIndex; i < Count; i++)
            {
                array[i] = this[i];
            }
        }
        public bool Remove(int item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i] == item)
                {
                    this[i] = 0;
                    return true;
                }
            }
            return false;
        }
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
        private int mAddedCount;
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
