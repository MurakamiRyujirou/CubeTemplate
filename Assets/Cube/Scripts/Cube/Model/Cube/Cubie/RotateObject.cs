using System;

namespace MurakamiRyujirou.Cube
{
    /// IRotatableインタフェースを実装した抽象クラス.
    /// 6面のパネルを回転操作により位置を入れ替えする.
    public abstract class RotateObject : IRotatable
    {
        /// 六面のパネル.
        protected PanelTable Panels;

        public RotateObject(PanelTable panels)
        {
            Panels = panels;
        }

        /// キュービーの現在の姿勢(Rx,Ry,Rzで回転後の状態)で指定面のカードを返す.
        public IPanel GetPanel(Faces face)
        {
            return Panels.Get(face);
        }

        /// 回転軸と時計回りか否かの指示に併せてキュービーを回転させる.
        /// 回転後の状態を得るために、配列の位置を入れ替える.
        /// <param name="axis">回転軸.</param>
        /// <param name="isClockwise">時計回りか否か.</param>
        public void Rotate(Axes axis, bool isClockwise)
        {
            switch (axis)
            {
                case Axes.X:
                    Swap(Faces.FRONT, Faces.DOWN, Faces.BACK, Faces.UP, isClockwise);
                    break;
                case Axes.Y:
                    Swap(Faces.FRONT, Faces.RIGHT, Faces.BACK, Faces.LEFT, isClockwise);
                    break;
                case Axes.Z:
                    Swap(Faces.UP, Faces.RIGHT, Faces.DOWN, Faces.LEFT, isClockwise);
                    break;
                default:
                    break;
            }
        }

        /// パネル位置の入れ替え操作.
        private void Swap(Faces f1, Faces f2, Faces f3, Faces f4, bool clockwise)
        {
            if (f1 == Faces.NONE || f2 == Faces.NONE || f3 == Faces.NONE || f4 == Faces.NONE)
                throw new ArgumentException("Illegal argument exception.");
            if (clockwise)
            {
                IPanel temp = Panels.Get(f1);
                Panels.Set(f1, Panels.Get(f2));
                Panels.Set(f2, Panels.Get(f3));
                Panels.Set(f3, Panels.Get(f4));
                Panels.Set(f4, temp);
            }
            else
            {
                IPanel temp = Panels.Get(f1);
                Panels.Set(f1, Panels.Get(f4));
                Panels.Set(f4, Panels.Get(f3));
                Panels.Set(f3, Panels.Get(f2));
                Panels.Set(f2, temp);
            }
        }
    }
}