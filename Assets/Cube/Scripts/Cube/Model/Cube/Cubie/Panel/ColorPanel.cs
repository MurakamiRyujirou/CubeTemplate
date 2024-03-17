using System;

namespace MurakamiRyujirou.Cube
{
    [Serializable]
    public class ColorPanel : IPanel
    {
        public static ColorPanel N = new(PanelColors.NONE);
        public static ColorPanel R = new(PanelColors.RED);
        public static ColorPanel O = new(PanelColors.ORANGE);
        public static ColorPanel W = new(PanelColors.WHITE);
        public static ColorPanel Y = new(PanelColors.YELLOW);
        public static ColorPanel B = new(PanelColors.BLUE);
        public static ColorPanel G = new(PanelColors.GREEN);

        public PanelColors Color { get; private set; }

        /// default constructor.
        public ColorPanel(PanelColors color)
        {
            this.Color = color;
        }

        /// copy constructor.
        public ColorPanel(ColorPanel panel)
        {
            this.Color = panel.Color;
        }

        // -------- OVERRIDE --------

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (!(obj is ColorPanel)) return false;
            ColorPanel target = (ColorPanel)obj;
            return Color == target.Color;
        }

        public override int GetHashCode()
        {
            return Color.GetHashCode();
        }

        public override string ToString()
        {
            return Color switch
            {
                PanelColors.NONE   => " ",
                PanelColors.RED    => "R",
                PanelColors.ORANGE => "O",
                PanelColors.WHITE  => "W",
                PanelColors.YELLOW => "Y",
                PanelColors.BLUE   => "B",
                PanelColors.GREEN  => "G",
                _ => "_"
            };
        }
    }
}
