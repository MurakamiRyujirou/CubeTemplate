using System;

namespace MurakamiRyujirou.Cube
{
    /// キューブを構成する１つのブロックをキュービーと呼ぶ.
    [Serializable]
    public class Cubie : RotateObject, ICubie
    {
        // キュービーの面の数.Facesで定義されてはいるが、マジックナンバーの6を使わないように定義.
        // public const int NUMBER_OF_FACES = 6;

        public Position InitialPosition { get; }
        public Position CurrentPosition { get; set; }

        public PanelTable InitialPanels { get; }
        public PanelTable CurrentPanels { get { return base.Panels; } }

        public Cubie() : base(new(ColorPanel.R, ColorPanel.O, ColorPanel.W, ColorPanel.Y, ColorPanel.B, ColorPanel.G))
        {
            PanelTable panels = new(ColorPanel.R, ColorPanel.O, ColorPanel.W, ColorPanel.Y, ColorPanel.B, ColorPanel.G);
            InitialPosition = new(Position.ZERO);
            CurrentPosition = new(Position.ZERO);
            InitialPanels = new(panels);
        }

        public Cubie(Position initialPosition, PanelTable panels) : base(panels)
        {
            InitialPosition = new(initialPosition);
            CurrentPosition = new(initialPosition);
            InitialPanels = new(panels);
        }

        public PanelColors GetColor(Faces face)
        {
            ColorPanel panel = (ColorPanel)CurrentPanels.Get(face);
            return panel.Color;
        }

        // -------- OVERRIDE --------

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (!(obj is Cubie)) return false;
            Cubie target = (Cubie)obj;
            return InitialPosition.Equals(target.InitialPosition) &&
                   InitialPanels.Equals(target.InitialPanels) &&
                   CurrentPosition.Equals(target.CurrentPosition) &&
                   CurrentPanels.Equals(target.CurrentPanels);
        }

        public override int GetHashCode()
        {
            int hash = 0;
            hash += hash * 31 + InitialPosition.GetHashCode();
            hash += hash * 31 + InitialPanels.GetHashCode();
            hash += hash * 31 + CurrentPosition.GetHashCode();
            hash += hash * 31 + CurrentPanels.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return InitialPosition.ToString() + "," +
                   InitialPanels.ToString() + "," +
                   CurrentPosition.ToString() + "," +
                   CurrentPanels.ToString();
        }
    }
}