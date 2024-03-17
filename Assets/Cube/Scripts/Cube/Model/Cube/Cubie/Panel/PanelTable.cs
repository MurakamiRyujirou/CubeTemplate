using System;
using System.Collections.Generic;

namespace MurakamiRyujirou.Cube
{
    [Serializable]
    public class PanelTable
    {
        // キュービーの面の数.Facesで定義されてはいるが、マジックナンバーの6を使わないように定義.
        public const int NUMBER_OF_FACES = 6;

        private readonly Dictionary<Faces, IPanel> Panels;

        public PanelTable(IPanel right, IPanel left, IPanel up, IPanel down, IPanel back, IPanel front)
        {
            Panels = new();
            Panels.Add(Faces.RIGHT, right);
            Panels.Add(Faces.LEFT,  left);
            Panels.Add(Faces.UP,    up);
            Panels.Add(Faces.DOWN,  down);
            Panels.Add(Faces.BACK,  back);
            Panels.Add(Faces.FRONT, front);
        }

        public PanelTable(PanelTable panels)
        {
            Panels = new();
            for (int i = 0; i < NUMBER_OF_FACES; i++)
            {
                Faces face = (Faces)Enum.ToObject(typeof(Faces), i);
                Panels.Add(face, panels.Get(face));
            }
        }

        public IPanel Get(Faces face)
        {
            return Panels[face];
        }

        public void Set(Faces face, IPanel panel)
        {
            Panels[face] = panel;
        }

        // -------- OVERRIDE --------

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (!(obj is PanelTable)) return false;
            PanelTable target = (PanelTable)obj;
            for (int i = 0; i < NUMBER_OF_FACES; i++)
            {
                Faces face = (Faces)Enum.ToObject(typeof(Faces), i);
                if (!Panels[face].Equals(target.Panels[face]))
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            for (int i = 0; i < NUMBER_OF_FACES; i++)
            {
                Faces face = (Faces)Enum.ToObject(typeof(Faces), i);
                hash += hash * 31 + Panels[face].GetHashCode();
            }
            return hash;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < NUMBER_OF_FACES; i++)
            {
                Faces face = (Faces)Enum.ToObject(typeof(Faces), i);
                s += Panels[face].ToString();
            }
            return s;
        }
    }
}
