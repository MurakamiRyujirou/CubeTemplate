using System;
using System.Collections.Generic;
using UnityEngine;

namespace MurakamiRyujirou.Cube
{
    /// キューブモデルクラス.
    /// キューブは複数のキュービー(Cubie,Size x Size x Size個存在)で構成される.
    [Serializable]
    public class Cube : CubeBase, ICube
    {
        /// Constructor.
        public Cube()
        {
            Size = 3;
            rotator = new CubeRotator();
            cubies = new Cubie[Size, Size, Size];

            for (int z = 0; z < Size; z++)
            {
                for (int y = 0; y < Size; y++)
                {
                    for (int x = 0; x < Size; x++)
                    {
                        Position position = new(x, y, z);
                        PanelTable panels = REAL_CUBE[x * Size * Size + y * Size + z];
                        // PanelTable panels = new PanelTable(ColorPanel.R, ColorPanel.O, ColorPanel.W, ColorPanel.Y, ColorPanel.B, ColorPanel.G);
                        cubies[x, y, z] = new Cubie(position, panels);
                    }
                }
            }
        }

        public new Cubie GetCubie(Position pos)
        {
            return (Cubie)base.GetCubie(pos);
        }

        public PanelColors GetColor(Position pos, Faces face)
        {
            return ((ColorPanel) GetPanel(pos, face)).Color;
        }

        private readonly PanelTable[] REAL_CUBE = new PanelTable[]
        {
            // 000 -> 001 -> 002 -> 010 -> ... -> 222
            new PanelTable(ColorPanel.N, ColorPanel.O, ColorPanel.N, ColorPanel.Y, ColorPanel.N, ColorPanel.G),
            new PanelTable(ColorPanel.N, ColorPanel.O, ColorPanel.N, ColorPanel.Y, ColorPanel.N, ColorPanel.N),
            new PanelTable(ColorPanel.N, ColorPanel.O, ColorPanel.N, ColorPanel.Y, ColorPanel.B, ColorPanel.N),
            new PanelTable(ColorPanel.N, ColorPanel.O, ColorPanel.N, ColorPanel.N, ColorPanel.N, ColorPanel.G),
            new PanelTable(ColorPanel.N, ColorPanel.O, ColorPanel.N, ColorPanel.N, ColorPanel.N, ColorPanel.N),
            new PanelTable(ColorPanel.N, ColorPanel.O, ColorPanel.N, ColorPanel.N, ColorPanel.B, ColorPanel.N),
            new PanelTable(ColorPanel.N, ColorPanel.O, ColorPanel.W, ColorPanel.N, ColorPanel.N, ColorPanel.G),
            new PanelTable(ColorPanel.N, ColorPanel.O, ColorPanel.W, ColorPanel.N, ColorPanel.N, ColorPanel.N),
            new PanelTable(ColorPanel.N, ColorPanel.O, ColorPanel.W, ColorPanel.N, ColorPanel.B, ColorPanel.N),

            new PanelTable(ColorPanel.N, ColorPanel.N, ColorPanel.N, ColorPanel.Y, ColorPanel.N, ColorPanel.G),
            new PanelTable(ColorPanel.N, ColorPanel.N, ColorPanel.N, ColorPanel.Y, ColorPanel.N, ColorPanel.N),
            new PanelTable(ColorPanel.N, ColorPanel.N, ColorPanel.N, ColorPanel.Y, ColorPanel.B, ColorPanel.N),
            new PanelTable(ColorPanel.N, ColorPanel.N, ColorPanel.N, ColorPanel.N, ColorPanel.N, ColorPanel.G),
            new PanelTable(ColorPanel.N, ColorPanel.N, ColorPanel.N, ColorPanel.N, ColorPanel.N, ColorPanel.N),
            new PanelTable(ColorPanel.N, ColorPanel.N, ColorPanel.N, ColorPanel.N, ColorPanel.B, ColorPanel.N),
            new PanelTable(ColorPanel.N, ColorPanel.N, ColorPanel.W, ColorPanel.N, ColorPanel.N, ColorPanel.G),
            new PanelTable(ColorPanel.N, ColorPanel.N, ColorPanel.W, ColorPanel.N, ColorPanel.N, ColorPanel.N),
            new PanelTable(ColorPanel.N, ColorPanel.N, ColorPanel.W, ColorPanel.N, ColorPanel.B, ColorPanel.N),

            new PanelTable(ColorPanel.R, ColorPanel.N, ColorPanel.N, ColorPanel.Y, ColorPanel.N, ColorPanel.G),
            new PanelTable(ColorPanel.R, ColorPanel.N, ColorPanel.N, ColorPanel.Y, ColorPanel.N, ColorPanel.N),
            new PanelTable(ColorPanel.R, ColorPanel.N, ColorPanel.N, ColorPanel.Y, ColorPanel.B, ColorPanel.N),
            new PanelTable(ColorPanel.R, ColorPanel.N, ColorPanel.N, ColorPanel.N, ColorPanel.N, ColorPanel.G),
            new PanelTable(ColorPanel.R, ColorPanel.N, ColorPanel.N, ColorPanel.N, ColorPanel.N, ColorPanel.N),
            new PanelTable(ColorPanel.R, ColorPanel.N, ColorPanel.N, ColorPanel.N, ColorPanel.B, ColorPanel.N),
            new PanelTable(ColorPanel.R, ColorPanel.N, ColorPanel.W, ColorPanel.N, ColorPanel.N, ColorPanel.G),
            new PanelTable(ColorPanel.R, ColorPanel.N, ColorPanel.W, ColorPanel.N, ColorPanel.N, ColorPanel.N),
            new PanelTable(ColorPanel.R, ColorPanel.N, ColorPanel.W, ColorPanel.N, ColorPanel.B, ColorPanel.N)
        };

        // -------- OVERRIDE --------

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (obj.GetType() != this.GetType()) return false;
            Cube target = (Cube)obj;
            if (target.cubies.GetLength(0) != cubies.GetLength(0) ||
                target.cubies.GetLength(1) != cubies.GetLength(1) ||
                target.cubies.GetLength(2) != cubies.GetLength(2)) return false;
            for (int x = 0; x < cubies.GetLength(0); x++)
                for (int y = 0; y < cubies.GetLength(1); y++)
                    for (int z = 0; z < cubies.GetLength(2); z++)
                        if (!cubies[x, y, z].Equals(target.cubies[x, y, z]))
                            return false;
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            for (int x = 0; x < cubies.GetLength(0); x++)
                for (int y = 0; y < cubies.GetLength(1); y++)
                    for (int z = 0; z < cubies.GetLength(2); z++)
                        hash = hash * 31 + cubies[x, y, z].GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            if (Size == 3) return ToStringx3();
            return "";
        }

        private string ToStringx3()
        {
            Dictionary<PanelPositions, string> s = new();
            foreach (PanelPositions p in Enum.GetValues(typeof(PanelPositions)))
            {
                s.Add(p, ColorInitial(PanelPosition.GetPanelPosition(p)));
            }
            string UBL = s[PanelPositions.UBL];
            string UB = s[PanelPositions.UB];
            string UBR = s[PanelPositions.UBR];
            string UL = s[PanelPositions.UL];
            string U = s[PanelPositions.U];
            string UR = s[PanelPositions.UR];
            string UFL = s[PanelPositions.UFL];
            string UF = s[PanelPositions.UF];
            string UFR = s[PanelPositions.UFR];
            string LBU = s[PanelPositions.LBU];
            string LU = s[PanelPositions.LU];
            string LFU = s[PanelPositions.LFU];
            string FLU = s[PanelPositions.FLU];
            string FU = s[PanelPositions.FU];
            string FRU = s[PanelPositions.FRU];
            string RFU = s[PanelPositions.RFU];
            string RU = s[PanelPositions.RU];
            string RBU = s[PanelPositions.RBU];
            string BRU = s[PanelPositions.BRU];
            string BU = s[PanelPositions.BU];
            string BLU = s[PanelPositions.BLU];
            string LB = s[PanelPositions.LB];
            string L = s[PanelPositions.L];
            string LF = s[PanelPositions.LF];
            string FL = s[PanelPositions.FL];
            string F = s[PanelPositions.F];
            string FR = s[PanelPositions.FR];
            string RF = s[PanelPositions.RF];
            string R = s[PanelPositions.R];
            string RB = s[PanelPositions.RB];
            string BR = s[PanelPositions.BR];
            string B = s[PanelPositions.B];
            string BL = s[PanelPositions.BL];
            string LBD = s[PanelPositions.LBD];
            string LD = s[PanelPositions.LD];
            string LDF = s[PanelPositions.LDF];
            string FDL = s[PanelPositions.FDL];
            string FD = s[PanelPositions.FD];
            string FDR = s[PanelPositions.FDR];
            string RDF = s[PanelPositions.RDF];
            string RD = s[PanelPositions.RD];
            string RBD = s[PanelPositions.RBD];
            string BDR = s[PanelPositions.BDR];
            string BD = s[PanelPositions.BD];
            string BDL = s[PanelPositions.BDL];
            string DFL = s[PanelPositions.DFL];
            string DF = s[PanelPositions.DF];
            string DFR = s[PanelPositions.DFR];
            string DL = s[PanelPositions.DL];
            string D = s[PanelPositions.D];
            string DR = s[PanelPositions.DR];
            string DBL = s[PanelPositions.DBL];
            string DB = s[PanelPositions.DB];
            string DBR = s[PanelPositions.DBR];
            string str = "";
            str += "   " + UBL + UB + UBR + "\n";
            str += "   " + UL + U + UR + "\n";
            str += "   " + UFL + UF + UFR + "\n";
            str += LBU + LU + LFU + FLU + FU + FRU + RFU + RU + RBU + BRU + BU + BLU + "\n";
            str += LB + L + LF + FL + F + FR + RF + R + RB + BR + B + BL + "\n";
            str += LBD + LD + LDF + FDL + FD + FDR + RDF + RD + RBD + BDR + BD + BDL + "\n";
            str += "   " + DFL + DF + DFR + "\n";
            str += "   " + DL + D + DR + "\n";
            str += "   " + DBL + DB + DBR + "\n";
            return str;
        }

        private string ColorInitial(PanelPosition p)
        {
            return GetColor(new Position(p.X, p.Y, p.Z), p.Face).ToString();
        }

    }
}
