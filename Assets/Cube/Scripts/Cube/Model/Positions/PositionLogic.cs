using System.Linq;
using UnityEngine;

namespace MurakamiRyujirou.Cube
{
    /// 座標関連のロジック.
    public static class PositionLogic
    {
        private static Vector3Int c = new(1, 1, 1);
        private static Vector3Int u = new(1, 2, 1);
        private static Vector3Int f = new(1, 1, 0);
        private static Vector3Int r = new(2, 1, 1);
        private static Vector3Int b = new(1, 1, 2);
        private static Vector3Int l = new(0, 1, 1);
        private static Vector3Int d = new(1, 0, 1);
        private static Vector3Int ubl = new(0, 2, 2);
        private static Vector3Int ubr = new(2, 2, 2);
        private static Vector3Int ufr = new(2, 2, 0);
        private static Vector3Int ufl = new(0, 2, 0);
        private static Vector3Int dbl = new(0, 0, 2);
        private static Vector3Int dbr = new(2, 0, 2);
        private static Vector3Int dfr = new(2, 0, 0);
        private static Vector3Int dfl = new(0, 0, 0);
        private static Vector3Int ub = new(1, 2, 2);
        private static Vector3Int ur = new(2, 2, 1);
        private static Vector3Int uf = new(1, 2, 0);
        private static Vector3Int ul = new(0, 2, 1);
        private static Vector3Int bl = new(0, 1, 2);
        private static Vector3Int br = new(2, 1, 2);
        private static Vector3Int fr = new(2, 1, 0);
        private static Vector3Int fl = new(0, 1, 0);
        private static Vector3Int db = new(1, 0, 2);
        private static Vector3Int dr = new(2, 0, 1);
        private static Vector3Int df = new(1, 0, 0);
        private static Vector3Int dl = new(0, 0, 1);
        private static Vector3Int[] all = { ubl, ubr, ufr, ufl, dbl, dbr, dfr, dfl, ub, ur, uf, ul, bl, br, fr, fl, db, dr, df, dl, u, f, r, b, l, d, c };

        /// 指定した回転操作で動くキュービーの座標を返す.
        /// <param name="oper">回転操作.</param>
        /// <returns>座標の配列.</returns>
        public static Vector3Int[] GetPositionsFromOpers(Operations oper)
        {
            return oper switch
            {
                Operations.R or Operations.R_ or Operations.R2 => GetPositionsFromFace(Faces.RIGHT),
                Operations.L or Operations.L_ or Operations.L2 => GetPositionsFromFace(Faces.LEFT),
                Operations.U or Operations.U_ or Operations.U2 => GetPositionsFromFace(Faces.UP),
                Operations.D or Operations.D_ or Operations.D2 => GetPositionsFromFace(Faces.DOWN),
                Operations.B or Operations.B_ or Operations.B2 => GetPositionsFromFace(Faces.BACK),
                Operations.F or Operations.F_ or Operations.F2 => GetPositionsFromFace(Faces.FRONT),
                Operations.M or Operations.M_ or Operations.M2 => GetPositionsFromAxis(Axes.X),
                Operations.E or Operations.E_ or Operations.E2 => GetPositionsFromAxis(Axes.Y),
                Operations.S or Operations.S_ or Operations.S2 => GetPositionsFromAxis(Axes.Z),
                Operations.x or Operations.x_ or Operations.x2 or
                Operations.y or Operations.y_ or Operations.y2 or
                Operations.z or Operations.z_ or Operations.z2 => all,
                Operations.Rw or Operations.Rw_ or Operations.Rw2 => GetPositionsFromFace(Faces.RIGHT).Concat(GetPositionsFromAxis(Axes.X)).ToArray(),
                Operations.Lw or Operations.Lw_ or Operations.Lw2 => GetPositionsFromFace(Faces.LEFT).Concat(GetPositionsFromAxis(Axes.X)).ToArray(),
                Operations.Uw or Operations.Uw_ or Operations.Uw2 => GetPositionsFromFace(Faces.UP).Concat(GetPositionsFromAxis(Axes.Y)).ToArray(),
                Operations.Dw or Operations.Dw_ or Operations.Dw2 => GetPositionsFromFace(Faces.DOWN).Concat(GetPositionsFromAxis(Axes.Y)).ToArray(),
                Operations.Bw or Operations.Bw_ or Operations.Bw2 => GetPositionsFromFace(Faces.BACK).Concat(GetPositionsFromAxis(Axes.Z)).ToArray(),
                Operations.Fw or Operations.Fw_ or Operations.Fw2 => GetPositionsFromFace(Faces.FRONT).Concat(GetPositionsFromAxis(Axes.Z)).ToArray(),
                Operations.NONE => new Vector3Int[] { },
                _ => new Vector3Int[] { },
            };
        }

        /// 指定した回転操作で動くキュービーの座標を返す.
        /// <param name="oper">回転操作.</param>
        /// <returns>座標の配列.</returns>
        public static Vector3Int[] GetPositionsFromOpers(SingleOperations oper)
        {
            return oper switch
            {
                SingleOperations.R or SingleOperations.R_ => GetPositionsFromFace(Faces.RIGHT),
                SingleOperations.L or SingleOperations.L_ => GetPositionsFromFace(Faces.LEFT),
                SingleOperations.U or SingleOperations.U_ => GetPositionsFromFace(Faces.UP),
                SingleOperations.D or SingleOperations.D_ => GetPositionsFromFace(Faces.DOWN),
                SingleOperations.B or SingleOperations.B_ => GetPositionsFromFace(Faces.BACK),
                SingleOperations.F or SingleOperations.F_ => GetPositionsFromFace(Faces.FRONT),
                SingleOperations.M or SingleOperations.M_ => GetPositionsFromAxis(Axes.X),
                SingleOperations.E or SingleOperations.E_ => GetPositionsFromAxis(Axes.Y),
                SingleOperations.S or SingleOperations.S_ => GetPositionsFromAxis(Axes.Z),
                _ => new Vector3Int[] { },
            };
        }

        /// 引数で与えられた回転軸でのスライス系操作に対して、動くエッジキュービーの座標を返す.
        /// CubeRotatorx3から利用される.
        /// <param name="axis">回転軸(X,Y,Z).</param>
        /// <returns>エッジキュービーの座標の配列.</returns>
        public static Vector3Int[] GetEdgePositionFromSlice(Axes axis)
        {
            return axis switch
            {
                Axes.X => new Vector3Int[] { uf, df, db, ub },
                Axes.Y => new Vector3Int[] { bl, fl, fr, br },
                Axes.Z => new Vector3Int[] { ul, ur, dr, dl },
                _ => new Vector3Int[] { },
            };
        }

        /// 引数で与えられた回転軸でのスライス系操作に対して、動くセンターキュービーの座標を返す.
        /// CubeRotatorx3から利用される.
        /// <param name="axis">回転軸(X,Y,Z).</param>
        /// <returns>センターキュービーの座標の配列.</returns>
        public static Vector3Int[] GetCenterPositionsFromSlice(Axes axis)
        {
            return axis switch
            {
                Axes.X => new Vector3Int[] { u, f, d, b },
                Axes.Y => new Vector3Int[] { b, l, f, r },
                Axes.Z => new Vector3Int[] { u, r, d, l },
                _ => new Vector3Int[] { },
            };
        }

        /// 指定した面のコーナーキュービーの座標を返す.
        /// 面に対してではなく、回転軸に向かって左上から半時計回りで格納する.
        /// --------------------------
        /// |RIGHT       |LEFT       |
        /// |ufr ur  ubr |ufl ul ubl |
        /// |fr   r  br  |fl  l  bl  |
        /// |dfr dr  dbr |dfl dl dbl |
        /// --------------------------
        /// Corner R : ufr <- dfr <- dbr <- ubr
        /// Corner L : ubl <- ufl <- dfl <- dbl
        /// Corner U : ubl <- ufl <- ufr <- ubr
        /// Corner D : dfl <- dfr <- dbr <- dbl
        /// Corner B : ubr <- dbr <- dbl <- ubl
        /// Corner F : ufl <- ufr <- dfr <- dfl
        /// <param name="face">面.</param>
        /// <returns>座標の配列.</returns>
        public static Vector3Int[] GetCornerPositionsFromFace(Faces face)
        {
            return face switch
            {
                Faces.RIGHT => new Vector3Int[] { ufr, dfr, dbr, ubr },
                Faces.LEFT  => new Vector3Int[] { ubl, ufl, dfl, dbl },
                Faces.UP    => new Vector3Int[] { ubl, ufl, ufr, ubr },
                Faces.DOWN  => new Vector3Int[] { dfl, dfr, dbr, dbl },
                Faces.BACK  => new Vector3Int[] { ubr, dbr, dbl, ubl },
                Faces.FRONT => new Vector3Int[] { ufl, ufr, dfr, dfl },
                _ => new Vector3Int[] { },
            };
        }

        /// 指定した面のエッジキュービーの座標を返す.
        /// 回転軸に対して上から半時計回りで格納する.
        /// RIGHT
        /// ufr ur  ubr
        /// fr   r  br
        /// dfr dr  dbr
        /// Edge R : ur <- fr <- dr <- br
        /// Edge L : ul <- fl <- dl <- bl  
        /// Edge U : ub <- ul <- uf <- ur 
        /// Edge D : df <- dr <- db <- dl 
        /// Edge B : ub <- br <- db <- bl 
        /// Edge F : uf <- fr <- df <- fl 
        /// <param name="face">面.</param>
        /// <returns>座標の配列.</returns>
        public static Vector3Int[] GetEdgePositionsFromFace(Faces face)
        {
            return face switch
            {
                Faces.RIGHT => new Vector3Int[] { ur, fr, dr, br },
                Faces.LEFT  => new Vector3Int[] { ul, fl, dl, bl },
                Faces.UP    => new Vector3Int[] { ub, ul, uf, ur },
                Faces.DOWN  => new Vector3Int[] { df, dr, db, dl },
                Faces.BACK  => new Vector3Int[] { ub, br, db, bl },
                Faces.FRONT => new Vector3Int[] { uf, fr, df, fl },
                _ => new Vector3Int[] { },
            };
        }

        /// 真ん中のキュービーの座標を返す.
        /// <returns>座標.常に[1,1,1].</returns>
        public static Vector3Int[] GetCorePosition()
        {
            return new Vector3Int[] { c };
        }

        /// 指定した面のセンターキュービーの座標を返す.
        /// <param name="face">面.</param>
        /// <returns>座標の配列.</returns>
        public static Vector3Int[] GetCenterPositionFromFace(Faces face)
        {
            return face switch
            {
                Faces.RIGHT => new Vector3Int[] { r },
                Faces.LEFT  => new Vector3Int[] { l },
                Faces.UP    => new Vector3Int[] { u },
                Faces.DOWN  => new Vector3Int[] { d },
                Faces.BACK  => new Vector3Int[] { b },
                Faces.FRONT => new Vector3Int[] { f },
                _ => new Vector3Int[] { },
            };
        }

        /// 指定した回転操作で動くキュービーの座標を返す.
        /// <param name="face">面.</param>
        /// <returns>座標の配列.</returns>
        public static Vector3Int[] GetPositionsFromFace(Faces face)
        {
            return GetCornerPositionsFromFace(face).
                Concat(GetEdgePositionsFromFace(face)).
                Concat(GetCenterPositionFromFace(face)).ToArray();
        }

        /// 指定した回転軸で動くキュービーの座標を返す.
        /// <param name="axis">回転軸.</param>
        /// <returns>座標の配列.</returns>
        public static Vector3Int[] GetPositionsFromAxis(Axes axis)
        {
            return GetEdgePositionFromSlice(axis).
                Concat(GetCenterPositionsFromSlice(axis)).
                Concat(GetCorePosition()).ToArray();
        }
    }
}
