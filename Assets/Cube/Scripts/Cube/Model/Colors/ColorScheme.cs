using System;

namespace MurakamiRyujirou.Cube
{
    /// 各面の配色情報.
    [Serializable]
    public class ColorScheme
    {
        public PanelColors[] Colors { get; private set; } = new PanelColors[Cubie.NUMBER_OF_FACES];

        /// default constructor.
        public ColorScheme()
        {
            for (int i = 0; i < Cubie.NUMBER_OF_FACES; i++)
            {
                Colors[i] = (PanelColors)Enum.ToObject(typeof(PanelColors), i);
            }
        }

        /// copy constructor.
        public ColorScheme(ColorScheme c)
        {
            for (int i = 0; i < Cubie.NUMBER_OF_FACES; i++)
            {
                Colors[i] = c.Colors[i];
            }
        }

        /// constructor with fields.
        public ColorScheme(PanelColors right, PanelColors left, PanelColors up, PanelColors down, PanelColors back, PanelColors front)
        {
            Colors[0] = right;
            Colors[1] = left;
            Colors[2] = up;
            Colors[3] = down;
            Colors[4] = back;
            Colors[5] = front;
        }

        /// 指定した面の配色を取得する.
        public PanelColors GetColor(Faces face)
        {
            if (face == Faces.NONE)
                return PanelColors.NONE;
            return Colors[(int)face];
        }

        /// 指定した色を表す文字(1文字)を返す.
        public static string ColorInitial(PanelColors color)
        {
            return color switch
            {
                PanelColors.WHITE  => "W",
                PanelColors.YELLOW => "Y",
                PanelColors.RED    => "R",
                PanelColors.ORANGE => "O",
                PanelColors.BLUE   => "B",
                PanelColors.GREEN  => "G",
                PanelColors.NONE   => " ",
                _ => "*",
            };
        }

        /// 回転軸と時計回りか否かのパラメータで回転させる.
        public void Rotate(Axes axis, bool isClockwise)
        {
            switch (axis)
            {
                case Axes.X:
                    SwapColor(Faces.FRONT, Faces.DOWN, Faces.BACK, Faces.UP, isClockwise);
                    break;
                case Axes.Y:
                    SwapColor(Faces.FRONT, Faces.RIGHT, Faces.BACK, Faces.LEFT, isClockwise);
                    break;
                case Axes.Z:
                    SwapColor(Faces.UP, Faces.RIGHT, Faces.DOWN, Faces.LEFT, isClockwise);
                    break;
                default:
                    break;
            }
        }

        /// 回転後の状態を得るために、配列の位置を入れ替える.
        private void SwapColor(Faces f1, Faces f2, Faces f3, Faces f4, bool clockwise)
        {
            if (f1 == Faces.NONE || f2 == Faces.NONE || f3 == Faces.NONE || f4 == Faces.NONE)
                throw new ArgumentException("Illegal argument exception.");
            if (clockwise)
            {
                PanelColors temp = Colors[(int)f1];
                Colors[(int)f1] = Colors[(int)f2];
                Colors[(int)f2] = Colors[(int)f3];
                Colors[(int)f3] = Colors[(int)f4];
                Colors[(int)f4] = temp;
            }
            else
            {
                PanelColors temp = Colors[(int)f1];
                Colors[(int)f1] = Colors[(int)f4];
                Colors[(int)f4] = Colors[(int)f3];
                Colors[(int)f3] = Colors[(int)f2];
                Colors[(int)f2] = temp;
            }
        }

        // ------- OVERRIDE --------

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (obj.GetType() != this.GetType()) return false;
            ColorScheme target = (ColorScheme)obj;
            if (Colors.Length != target.Colors.Length) return false;
            for (int i = 0; i < Colors.Length; i++)
                if (Colors[i] != target.Colors[i]) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            for (int i = 0; i < Colors.Length; i++)
            {
                hash += hash * 31 + Colors[i].GetHashCode();
            }
            return hash;
        }

        public override string ToString()
        {
            string ret = "";
            for (int index = 0; index < Cubie.NUMBER_OF_FACES; index++)
            {
                ret += ColorScheme.ColorInitial(Colors[index]);
            }
            return ret;
        }
    }
}