using System;

namespace MurakamiRyujirou.Cube
{
    /// 単層系の回転操作に関連したロジック.
    public class SingleOperationLogic
    {
        /// 回転操作から回転軸を取得する.
        public static Axes GetAxisFromSingleOperation(SingleOperations oper)
        {
            return oper switch
            {
                SingleOperations.R or SingleOperations.R_ or 
                SingleOperations.L or SingleOperations.L_ or 
                SingleOperations.M or SingleOperations.M_ => Axes.X,
                SingleOperations.U or SingleOperations.U_ or 
                SingleOperations.D or SingleOperations.D_ or 
                SingleOperations.E or SingleOperations.E_ => Axes.Y,
                SingleOperations.B or SingleOperations.B_ or
                SingleOperations.F or SingleOperations.F_ or
                SingleOperations.S or SingleOperations.S_ => Axes.Z,
                _ => Axes.NONE
            };
        }

        /// 回転操作がスライス系か否か返す.
        public static bool IsSlice(SingleOperations oper)
        {
            return oper == SingleOperations.M  || oper == SingleOperations.E  || oper == SingleOperations.S  ||
                   oper == SingleOperations.M_ || oper == SingleOperations.E_ || oper == SingleOperations.S_;
        }

        /// 時計回りでの回転操作か否かを返す.
        public static bool IsClockwise(SingleOperations oper)
        {
            return oper == SingleOperations.R || oper == SingleOperations.M_ || oper == SingleOperations.L_ ||
                   oper == SingleOperations.U || oper == SingleOperations.E_ || oper == SingleOperations.D_ ||
                   oper == SingleOperations.B || oper == SingleOperations.S_ || oper == SingleOperations.F_;
        }

        /// 回転操作から回転する面を取得する.
        public static Faces GetFaceFromOperation(SingleOperations oper)
        {
            return oper switch
            {
                SingleOperations.R or SingleOperations.R_ => Faces.RIGHT,
                SingleOperations.L or SingleOperations.L_ => Faces.LEFT,
                SingleOperations.U or SingleOperations.U_ => Faces.UP,
                SingleOperations.D or SingleOperations.D_ => Faces.DOWN,
                SingleOperations.B or SingleOperations.B_ => Faces.BACK,
                SingleOperations.F or SingleOperations.F_ => Faces.FRONT,
                _ => Faces.NONE
            };
        }
    }
}
