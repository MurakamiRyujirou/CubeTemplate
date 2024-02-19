using System;

namespace MurakamiRyujirou.Cube
{
    /// パネル座標.
    /// パネルの座標名　→　座標（x,y,z)と面(faces)の取得、
    /// パネル名　→　キュービーの座標名の取得
    /// などを行う.
    [Serializable]
    public class PanelPosition
    {
        public CubiePosition Position { get; set; }
        public Faces Face { get; set; }
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        /// Constructor.
        /// <param name="x">X座標.</param>
        /// <param name="y">Y座標.</param>
        /// <param name="z">Z座標.</param>
        /// <param name="face">面.</param>
        public PanelPosition(int x, int y, int z, Faces face)
        {
            Position = new(x, y, z);
            Face = face;
            X = x;
            Y = y;
            Z = z;
        }

        /// パネル名に対応したパネル座標を返す.
        /// <param name="p">パネル名(UBLなど).</param>
        /// <returns>パネル座標.</returns>
        public static PanelPosition GetPanelPosition(PanelPositions p)
        {
            return p switch
            {
                PanelPositions.UBL => new PanelPosition(0, 2, 2, Faces.UP),
                PanelPositions.UBR => new PanelPosition(2, 2, 2, Faces.UP),
                PanelPositions.UFR => new PanelPosition(2, 2, 0, Faces.UP),
                PanelPositions.UFL => new PanelPosition(0, 2, 0, Faces.UP),
                PanelPositions.FLU => new PanelPosition(0, 2, 0, Faces.FRONT),
                PanelPositions.FRU => new PanelPosition(2, 2, 0, Faces.FRONT),
                PanelPositions.FDR => new PanelPosition(2, 0, 0, Faces.FRONT),
                PanelPositions.FDL => new PanelPosition(0, 0, 0, Faces.FRONT),
                PanelPositions.RFU => new PanelPosition(2, 2, 0, Faces.RIGHT),
                PanelPositions.RBU => new PanelPosition(2, 2, 2, Faces.RIGHT),
                PanelPositions.RBD => new PanelPosition(2, 0, 2, Faces.RIGHT),
                PanelPositions.RDF => new PanelPosition(2, 0, 0, Faces.RIGHT),
                PanelPositions.BRU => new PanelPosition(2, 2, 2, Faces.BACK),
                PanelPositions.BLU => new PanelPosition(0, 2, 2, Faces.BACK),
                PanelPositions.BDL => new PanelPosition(0, 0, 2, Faces.BACK),
                PanelPositions.BDR => new PanelPosition(2, 0, 2, Faces.BACK),
                PanelPositions.LBU => new PanelPosition(0, 2, 2, Faces.LEFT),
                PanelPositions.LFU => new PanelPosition(0, 2, 0, Faces.LEFT),
                PanelPositions.LDF => new PanelPosition(0, 0, 0, Faces.LEFT),
                PanelPositions.LBD => new PanelPosition(0, 0, 2, Faces.LEFT),
                PanelPositions.DFL => new PanelPosition(0, 0, 0, Faces.DOWN),
                PanelPositions.DFR => new PanelPosition(2, 0, 0, Faces.DOWN),
                PanelPositions.DBR => new PanelPosition(2, 0, 2, Faces.DOWN),
                PanelPositions.DBL => new PanelPosition(0, 0, 2, Faces.DOWN),
                PanelPositions.UB => new PanelPosition(1, 2, 2, Faces.UP),
                PanelPositions.UR => new PanelPosition(2, 2, 1, Faces.UP),
                PanelPositions.UF => new PanelPosition(1, 2, 0, Faces.UP),
                PanelPositions.UL => new PanelPosition(0, 2, 1, Faces.UP),
                PanelPositions.FU => new PanelPosition(1, 2, 0, Faces.FRONT),
                PanelPositions.FR => new PanelPosition(2, 1, 0, Faces.FRONT),
                PanelPositions.FD => new PanelPosition(1, 0, 0, Faces.FRONT),
                PanelPositions.FL => new PanelPosition(0, 1, 0, Faces.FRONT),
                PanelPositions.RU => new PanelPosition(2, 2, 1, Faces.RIGHT),
                PanelPositions.RB => new PanelPosition(2, 1, 2, Faces.RIGHT),
                PanelPositions.RD => new PanelPosition(2, 0, 1, Faces.RIGHT),
                PanelPositions.RF => new PanelPosition(2, 1, 0, Faces.RIGHT),
                PanelPositions.BU => new PanelPosition(1, 2, 2, Faces.BACK),
                PanelPositions.BL => new PanelPosition(0, 1, 2, Faces.BACK),
                PanelPositions.BD => new PanelPosition(1, 0, 2, Faces.BACK),
                PanelPositions.BR => new PanelPosition(2, 1, 2, Faces.BACK),
                PanelPositions.LU => new PanelPosition(0, 2, 1, Faces.LEFT),
                PanelPositions.LF => new PanelPosition(0, 1, 0, Faces.LEFT),
                PanelPositions.LD => new PanelPosition(0, 0, 1, Faces.LEFT),
                PanelPositions.LB => new PanelPosition(0, 1, 2, Faces.LEFT),
                PanelPositions.DF => new PanelPosition(1, 0, 0, Faces.DOWN),
                PanelPositions.DR => new PanelPosition(2, 0, 1, Faces.DOWN),
                PanelPositions.DB => new PanelPosition(1, 0, 2, Faces.DOWN),
                PanelPositions.DL => new PanelPosition(0, 0, 1, Faces.DOWN),
                PanelPositions.U => new PanelPosition(1, 2, 1, Faces.UP),
                PanelPositions.F => new PanelPosition(1, 1, 0, Faces.FRONT),
                PanelPositions.R => new PanelPosition(2, 1, 1, Faces.RIGHT),
                PanelPositions.B => new PanelPosition(1, 1, 2, Faces.BACK),
                PanelPositions.L => new PanelPosition(0, 1, 1, Faces.LEFT),
                PanelPositions.D => new PanelPosition(1, 0, 1, Faces.DOWN),
                _ => throw new NotImplementedException(),
            };
        }


        /// キュービーポジション名からパネルポジション名の配列を取得する.
        /// コーナーのキュービーの場合は3、
        /// エッジのキュービーの場合は2面、
        /// センターのキュービーの場合は1面を返す.
        /// <param name="p">キュービーポジション名.</param>
        /// <returns>パネルポジション名の配列.</returns>
        public static PanelPositions[] GetPanelPositions(CubiePositions p)
        {
            return p switch
            {
                CubiePositions.UBL => new PanelPositions[] { PanelPositions.UBL, PanelPositions.BLU, PanelPositions.BLU },
                CubiePositions.UBR => new PanelPositions[] { PanelPositions.UBR, PanelPositions.BRU, PanelPositions.RBU },
                CubiePositions.UFR => new PanelPositions[] { PanelPositions.UFR, PanelPositions.FRU, PanelPositions.RFU },
                CubiePositions.UFL => new PanelPositions[] { PanelPositions.UFL, PanelPositions.FLU, PanelPositions.LFU },
                CubiePositions.DFL => new PanelPositions[] { PanelPositions.DFL, PanelPositions.FDL, PanelPositions.LDF },
                CubiePositions.DFR => new PanelPositions[] { PanelPositions.DFR, PanelPositions.FDR, PanelPositions.RDF },
                CubiePositions.DBR => new PanelPositions[] { PanelPositions.DBR, PanelPositions.BDR, PanelPositions.RBD },
                CubiePositions.DBL => new PanelPositions[] { PanelPositions.DBL, PanelPositions.BDL, PanelPositions.LBD },
                CubiePositions.UB => new PanelPositions[] { PanelPositions.UB, PanelPositions.BU, },
                CubiePositions.UR => new PanelPositions[] { PanelPositions.UR, PanelPositions.RU, },
                CubiePositions.UF => new PanelPositions[] { PanelPositions.UF, PanelPositions.FU, },
                CubiePositions.UL => new PanelPositions[] { PanelPositions.UL, PanelPositions.LU, },
                CubiePositions.FR => new PanelPositions[] { PanelPositions.FR, PanelPositions.RF, },
                CubiePositions.FL => new PanelPositions[] { PanelPositions.FL, PanelPositions.LF, },
                CubiePositions.BL => new PanelPositions[] { PanelPositions.BL, PanelPositions.LB, },
                CubiePositions.BR => new PanelPositions[] { PanelPositions.BR, PanelPositions.RB, },
                CubiePositions.DF => new PanelPositions[] { PanelPositions.DF, PanelPositions.FD, },
                CubiePositions.DR => new PanelPositions[] { PanelPositions.DR, PanelPositions.RD, },
                CubiePositions.DB => new PanelPositions[] { PanelPositions.DB, PanelPositions.BD, },
                CubiePositions.DL => new PanelPositions[] { PanelPositions.DL, PanelPositions.LD, },
                CubiePositions.U => new PanelPositions[] { PanelPositions.U },
                CubiePositions.F => new PanelPositions[] { PanelPositions.F },
                CubiePositions.R => new PanelPositions[] { PanelPositions.R },
                CubiePositions.B => new PanelPositions[] { PanelPositions.B },
                CubiePositions.L => new PanelPositions[] { PanelPositions.L },
                CubiePositions.D => new PanelPositions[] { PanelPositions.D },
                CubiePositions.C => new PanelPositions[] { },

                _ => throw new NotImplementedException(),
            };
        }

        // -------- OVERRIDE --------

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (obj.GetType() != this.GetType()) return false;
            PanelPosition target = (PanelPosition)obj;
            return Position.Equals(target.Position) && Face.Equals(target.Face);
        }

        public override int GetHashCode()
        {
            int hash = 0;
            hash += hash * 31 + Position.GetHashCode();
            hash += hash * 31 + Face.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return Position.ToString() + "," + Face.ToString();
        }
    }
}
