using System;

namespace MurakamiRyujirou.Cube
{
    /// キューブ上のキュービーの座標を示すバリューオブジェクト.
    [Serializable]
    public class Position
    {
        public static Position ZERO = new(0, 0, 0);
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }

        /// Constructor with fields.
        public Position(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// Copy constructor.
        public Position(Position other)
        {
            X = other.X;
            Y = other.Y;
            Z = other.Z;
        }

        // -------- OVERRIDE --------

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (obj.GetType() != this.GetType()) return false;
            Position target = (Position)obj;
            return X == target.X && Y == target.Y && Z == target.Z;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            hash += hash * 31 + X;
            hash += hash * 31 + Y;
            hash += hash * 31 + Z;
            return hash;
        }

        public override string ToString()
        {
            return X.ToString() + "," + Y.ToString() + "," + Z.ToString();
        }
    }
}