using System;

namespace MurakamiRyujirou.Cube
{
    /// ’P‘wŒn‚Ì‰ñ“]‘€ì‚ÉŠÖ˜A‚µ‚½ƒƒWƒbƒN.
    public class SingleOperationLogic
    {
        /// ‰ñ“]‘€ì‚©‚ç‰ñ“]Ž²‚ðŽæ“¾‚·‚é.
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

        /// ‰ñ“]‘€ì‚ªƒXƒ‰ƒCƒXŒn‚©”Û‚©•Ô‚·.
        public static bool IsSlice(SingleOperations oper)
        {
            return oper == SingleOperations.M  || oper == SingleOperations.E  || oper == SingleOperations.S  ||
                   oper == SingleOperations.M_ || oper == SingleOperations.E_ || oper == SingleOperations.S_;
        }

        /// ŽžŒv‰ñ‚è‚Å‚Ì‰ñ“]‘€ì‚©”Û‚©‚ð•Ô‚·.
        public static bool IsClockwise(SingleOperations oper)
        {
            return oper == SingleOperations.R || oper == SingleOperations.M_ || oper == SingleOperations.L_ ||
                   oper == SingleOperations.U || oper == SingleOperations.E_ || oper == SingleOperations.D_ ||
                   oper == SingleOperations.B || oper == SingleOperations.S_ || oper == SingleOperations.F_;
        }

        /// ‰ñ“]‘€ì‚©‚ç‰ñ“]‚·‚é–Ê‚ðŽæ“¾‚·‚é.
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
