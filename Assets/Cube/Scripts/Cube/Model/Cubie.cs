using System;

namespace MurakamiRyujirou.Cube
{
    /// キューブを構成する１つのブロックをキュービーと呼ぶ.
    [Serializable]
    public class Cubie
    {
        // キュービーの面の数.Facesで定義されてはいるが、マジックナンバーの6を使わないように定義.
        public const int NUMBER_OF_FACES = 6;

        // 現在の姿勢(回転角度)での配色.
        public ColorScheme Colors { get; private set; }

        // もともと在った位置.これがないとCubieViewが対応付くものが見つからないので保持.
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }

        /// default constructor.
        public Cubie()
        {
            Colors = new ColorScheme();
            X = 0;
            Y = 0;
            Z = 0;
        }

        /// copy constructor.
        public Cubie(Cubie cubie)
        {
            Colors = new ColorScheme(cubie.Colors);
            X = cubie.X;
            Y = cubie.Y;
            Z = cubie.Z;
        }

        /// constructor with fields.
        public Cubie(ColorScheme c, int x, int y, int z)
        {
            Colors = new ColorScheme(c);
            X = x;
            Y = y;
            Z = z;
        }

        /// キュービーの現在の姿勢(Rx,Ry,Rzで回転後の状態)で指定面の色を返す.
        public PanelColors GetColor(Faces face)
        {
            return Colors.GetColor(face);
        }

        /// 回転軸と時計回りか否かの指示に併せてキュービーを回転させる.
        /// <param name="axis">回転軸.</param>
        /// <param name="isClockwise">時計回りか否か.</param>
        public void Rotate(Axes axis, bool isClockwise)
        {
            Colors.Rotate(axis, isClockwise);
        }

        // -------- OVERRIDE --------

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (obj.GetType() != this.GetType()) return false;
            Cubie target = (Cubie)obj;
            return target.X  == X  && target.Y  == Y  && target.Z  == Z  &&
                   target.Colors.Equals(Colors);
        }

        public override int GetHashCode()
        {
            int hash = 0;
            hash += hash * 31 + X;
            hash += hash * 31 + Y;
            hash += hash * 31 + Z;
            hash += hash * 31 + Colors.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return "x="  + X  + ",y="  + Y  + ",z="  + Z  +
                   ",color=" + Colors.ToString();
        }
    }
}