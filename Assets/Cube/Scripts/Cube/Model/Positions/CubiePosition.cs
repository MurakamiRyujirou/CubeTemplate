using System;

namespace MurakamiRyujirou.Cube
{
    /// キュービーの座標.
    [Serializable]
    public class CubiePosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        /// Constructor.
        /// <param name="x">X座標.</param>
        /// <param name="y">Y座標.</param>
        /// <param name="z">Z座標.</param>
        public CubiePosition(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// ポジション名に対応した座標を返す.
        /// <param name="p">ポジション名(UBLなど).</param>
        /// <returns>キュービーポジション座標.</returns>
        public static CubiePosition GetCubiePosition(CubiePositions p)
        {
            return p switch
            {
                CubiePositions.UBL => new CubiePosition(0, 2, 2),
                CubiePositions.UBR => new CubiePosition(2, 2, 2),
                CubiePositions.UFR => new CubiePosition(2, 2, 0),
                CubiePositions.UFL => new CubiePosition(0, 2, 0),
                CubiePositions.DFL => new CubiePosition(0, 0, 0),
                CubiePositions.DFR => new CubiePosition(2, 0, 0),
                CubiePositions.DBR => new CubiePosition(2, 0, 2),
                CubiePositions.DBL => new CubiePosition(0, 0, 2),

                CubiePositions.UB => new CubiePosition(1, 2, 2),
                CubiePositions.UR => new CubiePosition(2, 2, 1),
                CubiePositions.UF => new CubiePosition(1, 2, 0),
                CubiePositions.UL => new CubiePosition(0, 2, 1),
                CubiePositions.FR => new CubiePosition(2, 1, 0),
                CubiePositions.FL => new CubiePosition(0, 1, 0),
                CubiePositions.BL => new CubiePosition(0, 1, 2),
                CubiePositions.BR => new CubiePosition(2, 1, 2),
                CubiePositions.DF => new CubiePosition(1, 0, 0),
                CubiePositions.DR => new CubiePosition(2, 0, 1),
                CubiePositions.DB => new CubiePosition(1, 0, 2),
                CubiePositions.DL => new CubiePosition(0, 0, 1),

                CubiePositions.U => new CubiePosition(1, 2, 1),
                CubiePositions.F => new CubiePosition(1, 1, 0),
                CubiePositions.R => new CubiePosition(2, 1, 1),
                CubiePositions.B => new CubiePosition(1, 1, 2),
                CubiePositions.L => new CubiePosition(0, 1, 1),
                CubiePositions.D => new CubiePosition(1, 0, 1),

                CubiePositions.C => new CubiePosition(1, 1, 1),

                _ => throw new NotImplementedException(),
            };
        }

        /// パネル名から、そのパネルが存在すべきポジション名を返す.
        /// UBLやBLU->UBL.
        /// <param name="p">パネル名.</param>
        /// <returns>ポジション名.</returns>
        public static CubiePositions GetCubiePositons(PanelPositions p)
        {
            return p switch
            {
                PanelPositions.UBL => CubiePositions.UBL,
                PanelPositions.UBR => CubiePositions.UBR,
                PanelPositions.UFR => CubiePositions.UFR,
                PanelPositions.UFL => CubiePositions.UFL,
                PanelPositions.FLU => CubiePositions.UFL,
                PanelPositions.FRU => CubiePositions.UFR,
                PanelPositions.FDR => CubiePositions.DFR,
                PanelPositions.FDL => CubiePositions.DFL,
                PanelPositions.RFU => CubiePositions.UFR,
                PanelPositions.RBU => CubiePositions.UBR,
                PanelPositions.RBD => CubiePositions.DBR,
                PanelPositions.RDF => CubiePositions.DFR,
                PanelPositions.BRU => CubiePositions.UBR,
                PanelPositions.BLU => CubiePositions.UBL,
                PanelPositions.BDL => CubiePositions.DBL,
                PanelPositions.BDR => CubiePositions.DBR,
                PanelPositions.LBU => CubiePositions.UBL,
                PanelPositions.LFU => CubiePositions.UFL,
                PanelPositions.LDF => CubiePositions.DFL,
                PanelPositions.LBD => CubiePositions.DBL,
                PanelPositions.DFL => CubiePositions.DFL,
                PanelPositions.DFR => CubiePositions.DFR,
                PanelPositions.DBR => CubiePositions.DBR,
                PanelPositions.DBL => CubiePositions.DBL,

                PanelPositions.UB => CubiePositions.UB,
                PanelPositions.UR => CubiePositions.UR,
                PanelPositions.UF => CubiePositions.UF,
                PanelPositions.UL => CubiePositions.UL,
                PanelPositions.FU => CubiePositions.UF,
                PanelPositions.FR => CubiePositions.FR,
                PanelPositions.FD => CubiePositions.DF,
                PanelPositions.FL => CubiePositions.FL,
                PanelPositions.RU => CubiePositions.UR,
                PanelPositions.RB => CubiePositions.BR,
                PanelPositions.RD => CubiePositions.DR,
                PanelPositions.RF => CubiePositions.FR,
                PanelPositions.BU => CubiePositions.UB,
                PanelPositions.BL => CubiePositions.BL,
                PanelPositions.BD => CubiePositions.DB,
                PanelPositions.BR => CubiePositions.BR,
                PanelPositions.LU => CubiePositions.UL,
                PanelPositions.LF => CubiePositions.FL,
                PanelPositions.LD => CubiePositions.DL,
                PanelPositions.LB => CubiePositions.BL,
                PanelPositions.DF => CubiePositions.DF,
                PanelPositions.DR => CubiePositions.DR,
                PanelPositions.DB => CubiePositions.DB,
                PanelPositions.DL => CubiePositions.DL,

                PanelPositions.U => CubiePositions.U,
                PanelPositions.F => CubiePositions.F,
                PanelPositions.R => CubiePositions.R,
                PanelPositions.B => CubiePositions.B,
                PanelPositions.L => CubiePositions.L,
                PanelPositions.D => CubiePositions.D,

                _ => throw new NotImplementedException(),
            };
        }
    }
}
