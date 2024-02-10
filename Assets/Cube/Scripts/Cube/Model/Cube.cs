using System;
using System.Collections.Generic;
using UnityEngine;

namespace MurakamiRyujirou.Cube
{
    /// キューブモデルクラス.
    /// キューブは複数のキュービー(Cubie,Size x Size x Size個存在)で構成される.
    [Serializable]
    public class Cube
    {
        public int Size { get; private set; }

        // キューブを構成するCubie.Rotateすると配列の位置を入れ替える.
        private readonly Cubie[,,] cubies;

        /// キューブの回転操作は本クラスではなく、ICubeRotatorインタフェースを継承するクラスで実現する.
        private readonly ICubeRotator rotator;

        /// constructor.
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
                        ColorScheme c = REAL_CUBE[x * Size * Size + y * Size + z];
                        // ColorScheme c = new();
                        cubies[x, y, z] = new Cubie(c, x, y, z);
                    }
                }
            }
        }

        /// 回転処理を行う.
        /// キューブの回転操作でキュービー配列の位置を入れ替える.
        public void Rotate(Operations oper)
        {
            rotator.Rotate(cubies, oper);
        }

        /// 指定した回転操作で動かす対象のキュービーの配列を取得する.
        public Cubie[] GetRotateCubies(Operations oper)
        {
            return GetCubies(GetRotatePosition(oper));
        }

        /// 指定した回転操作で動かす対象のキュービー座標配列を取得する.
        public Vector3Int[] GetRotatePosition(Operations oper)
        {
            return PositionLogic.GetPositionsFromOpers(oper);
        }

        /// 指定した回転操作で動かす対象のキュービーの添え字(元の座標位置)を取得する.
        public Vector3Int[] GetRotateIndexes(Operations oper)
        {
            Vector3Int[] posList = PositionLogic.GetPositionsFromOpers(oper);
            List<Vector3Int> retList = new();
            foreach (Vector3Int pos in posList)
            {
                Cubie c = GetCubie(pos.x, pos.y, pos.z);
                retList.Add(new Vector3Int(c.X, c.Y, c.Z));
            }
            return retList.ToArray();
        }

        /// ３次元配列の座標リストを引数にその座標に今在るキュービーを取得する.
        public Cubie[] GetCubies(Vector3Int[] posList)
        {
            List<Cubie> ret = new();
            foreach (Vector3Int pos in posList)
            {
                ret.Add(GetCubie(pos.x, pos.y, pos.z));
            }
            return ret.ToArray();
        }

        /// 指定した位置に今在るキュービーを取得する.
        public Cubie GetCubie(int x, int y, int z)
        {
            if (x < 0 || x > Size || y < 0 || y > Size || z < 0 || z > Size)
                throw new ArgumentException("Illegal argument exception. x =" + x + ",y =" + y + ",z =" + z);
            if (cubies[x, y, z] == null)
                throw new Exception("Wrong parameter (" + x + "," + y + "," + z + ")");
            return cubies[x, y, z];
        }

        /// 指定座標の色情報を取得する.
        /// キュービーが回転していた場合は色情報を回転調整する.
        public ColorScheme GetColorScheme(int x, int y, int z)
        {
            return cubies[x, y, z].Colors;
        }

        private readonly ColorScheme[] REAL_CUBE = new ColorScheme[]
        {
            // 000 -> 001 -> 002 -> 010 -> ... -> 222
            new ColorScheme(PanelColors.NONE, PanelColors.ORANGE, PanelColors.NONE,  PanelColors.YELLOW, PanelColors.NONE, PanelColors.GREEN),
            new ColorScheme(PanelColors.NONE, PanelColors.ORANGE, PanelColors.NONE,  PanelColors.YELLOW, PanelColors.NONE, PanelColors.NONE),
            new ColorScheme(PanelColors.NONE, PanelColors.ORANGE, PanelColors.NONE,  PanelColors.YELLOW, PanelColors.BLUE, PanelColors.NONE),
            new ColorScheme(PanelColors.NONE, PanelColors.ORANGE, PanelColors.NONE,  PanelColors.NONE,   PanelColors.NONE, PanelColors.GREEN),
            new ColorScheme(PanelColors.NONE, PanelColors.ORANGE, PanelColors.NONE,  PanelColors.NONE,   PanelColors.NONE, PanelColors.NONE),
            new ColorScheme(PanelColors.NONE, PanelColors.ORANGE, PanelColors.NONE,  PanelColors.NONE,   PanelColors.BLUE, PanelColors.NONE),
            new ColorScheme(PanelColors.NONE, PanelColors.ORANGE, PanelColors.WHITE, PanelColors.NONE,   PanelColors.NONE, PanelColors.GREEN),
            new ColorScheme(PanelColors.NONE, PanelColors.ORANGE, PanelColors.WHITE, PanelColors.NONE,   PanelColors.NONE, PanelColors.NONE),
            new ColorScheme(PanelColors.NONE, PanelColors.ORANGE, PanelColors.WHITE, PanelColors.NONE,   PanelColors.BLUE, PanelColors.NONE),

            new ColorScheme(PanelColors.NONE, PanelColors.NONE,   PanelColors.NONE,  PanelColors.YELLOW, PanelColors.NONE, PanelColors.GREEN),
            new ColorScheme(PanelColors.NONE, PanelColors.NONE,   PanelColors.NONE,  PanelColors.YELLOW, PanelColors.NONE, PanelColors.NONE),
            new ColorScheme(PanelColors.NONE, PanelColors.NONE,   PanelColors.NONE,  PanelColors.YELLOW, PanelColors.BLUE, PanelColors.NONE),
            new ColorScheme(PanelColors.NONE, PanelColors.NONE,   PanelColors.NONE,  PanelColors.NONE,   PanelColors.NONE, PanelColors.GREEN),
            new ColorScheme(PanelColors.NONE, PanelColors.NONE,   PanelColors.NONE,  PanelColors.NONE,   PanelColors.NONE, PanelColors.NONE),
            new ColorScheme(PanelColors.NONE, PanelColors.NONE,   PanelColors.NONE,  PanelColors.NONE,   PanelColors.BLUE, PanelColors.NONE),
            new ColorScheme(PanelColors.NONE, PanelColors.NONE,   PanelColors.WHITE, PanelColors.NONE,   PanelColors.NONE, PanelColors.GREEN),
            new ColorScheme(PanelColors.NONE, PanelColors.NONE,   PanelColors.WHITE, PanelColors.NONE,   PanelColors.NONE, PanelColors.NONE),
            new ColorScheme(PanelColors.NONE, PanelColors.NONE,   PanelColors.WHITE, PanelColors.NONE,   PanelColors.BLUE, PanelColors.NONE),

            new ColorScheme(PanelColors.RED,  PanelColors.NONE,   PanelColors.NONE,  PanelColors.YELLOW, PanelColors.NONE, PanelColors.GREEN),
            new ColorScheme(PanelColors.RED,  PanelColors.NONE,   PanelColors.NONE,  PanelColors.YELLOW, PanelColors.NONE, PanelColors.NONE),
            new ColorScheme(PanelColors.RED,  PanelColors.NONE,   PanelColors.NONE,  PanelColors.YELLOW, PanelColors.BLUE, PanelColors.NONE),
            new ColorScheme(PanelColors.RED,  PanelColors.NONE,   PanelColors.NONE,  PanelColors.NONE,   PanelColors.NONE, PanelColors.GREEN),
            new ColorScheme(PanelColors.RED,  PanelColors.NONE,   PanelColors.NONE,  PanelColors.NONE,   PanelColors.NONE, PanelColors.NONE),
            new ColorScheme(PanelColors.RED,  PanelColors.NONE,   PanelColors.NONE,  PanelColors.NONE,   PanelColors.BLUE, PanelColors.NONE),
            new ColorScheme(PanelColors.RED,  PanelColors.NONE,   PanelColors.WHITE, PanelColors.NONE,   PanelColors.NONE, PanelColors.GREEN),
            new ColorScheme(PanelColors.RED,  PanelColors.NONE,   PanelColors.WHITE, PanelColors.NONE,   PanelColors.NONE, PanelColors.NONE),
            new ColorScheme(PanelColors.RED,  PanelColors.NONE,   PanelColors.WHITE, PanelColors.NONE,   PanelColors.BLUE, PanelColors.NONE)
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
            string UB  = s[PanelPositions.UB];
            string UBR = s[PanelPositions.UBR];
            string UL  = s[PanelPositions.UL];
            string U   = s[PanelPositions.U];
            string UR  = s[PanelPositions.UR];
            string UFL = s[PanelPositions.UFL];
            string UF  = s[PanelPositions.UF];
            string UFR = s[PanelPositions.UFR];
            string LBU = s[PanelPositions.LBU];
            string LU  = s[PanelPositions.LU];
            string LFU = s[PanelPositions.LFU];
            string FLU = s[PanelPositions.FLU];
            string FU  = s[PanelPositions.FU];
            string FRU = s[PanelPositions.FRU];
            string RFU = s[PanelPositions.RFU];
            string RU  = s[PanelPositions.RU];
            string RBU = s[PanelPositions.RBU];
            string BRU = s[PanelPositions.BRU];
            string BU  = s[PanelPositions.BU];
            string BLU = s[PanelPositions.BLU];
            string LB  = s[PanelPositions.LB];
            string L   = s[PanelPositions.L];
            string LF  = s[PanelPositions.LF];
            string FL  = s[PanelPositions.FL];
            string F   = s[PanelPositions.F];
            string FR  = s[PanelPositions.FR];
            string RF  = s[PanelPositions.RF];
            string R   = s[PanelPositions.R];
            string RB  = s[PanelPositions.RB];
            string BR  = s[PanelPositions.BR];
            string B   = s[PanelPositions.B];
            string BL  = s[PanelPositions.BL];
            string LBD = s[PanelPositions.LBD];
            string LD  = s[PanelPositions.LD];
            string LDF = s[PanelPositions.LDF];
            string FDL = s[PanelPositions.FDL];
            string FD  = s[PanelPositions.FD];
            string FDR = s[PanelPositions.FDR];
            string RDF = s[PanelPositions.RDF];
            string RD  = s[PanelPositions.RD];
            string RBD = s[PanelPositions.RBD];
            string BDR = s[PanelPositions.BDR];
            string BD  = s[PanelPositions.BD];
            string BDL = s[PanelPositions.BDL];
            string DFL = s[PanelPositions.DFL];
            string DF  = s[PanelPositions.DF];
            string DFR = s[PanelPositions.DFR];
            string DL  = s[PanelPositions.DL];
            string D   = s[PanelPositions.D];
            string DR  = s[PanelPositions.DR];
            string DBL = s[PanelPositions.DBL];
            string DB  = s[PanelPositions.DB];
            string DBR = s[PanelPositions.DBR];
            string str = "";
            str += "   "          + UBL + UB + UBR + "\n";
            str += "   "          + UL  + U  + UR  + "\n";
            str += "   "          + UFL + UF + UFR + "\n";
            str += LBU + LU + LFU + FLU + FU + FRU + RFU + RU + RBU + BRU + BU + BLU + "\n";
            str += LB  + L  + LF  + FL  + F  + FR  + RF  + R  + RB  + BR  + B  + BL  + "\n";
            str += LBD + LD + LDF + FDL + FD + FDR + RDF + RD + RBD + BDR + BD + BDL + "\n";
            str += "   "          + DFL + DF + DFR + "\n";
            str += "   "          + DL  + D  + DR  + "\n";
            str += "   "          + DBL + DB + DBR + "\n";
            return str;
        }

        private string ColorInitial(PanelPosition p)
        {
            PanelColors color = GetColorScheme(p.X, p.Y, p.Z).GetColor(p.Face);
            return ColorScheme.ColorInitial(color);
        }
    }
}
