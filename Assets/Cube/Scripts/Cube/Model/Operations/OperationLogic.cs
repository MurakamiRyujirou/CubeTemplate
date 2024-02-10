using System;

namespace MurakamiRyujirou.Cube
{
    /// ‰ñ“]‘€ìŠÖ˜A‚ÌƒƒWƒbƒN.
    public class OperationLogic
    {
        /// ‘S‰ñ“]‘€ì(Operations)‚ğ•ª‰ğ‚µA’P‘w‰ñ“]‘€ì(SingleOperations)‚ğæ“¾‚·‚é.
        public static SingleOperations[] Break(Operations oper)
        {
            return oper switch
            {
                Operations.R => new SingleOperations[] { SingleOperations.R },
                Operations.L => new SingleOperations[] { SingleOperations.L },
                Operations.U => new SingleOperations[] { SingleOperations.U },
                Operations.D => new SingleOperations[] { SingleOperations.D },
                Operations.B => new SingleOperations[] { SingleOperations.B },
                Operations.F => new SingleOperations[] { SingleOperations.F },

                Operations.R_ => new SingleOperations[] { SingleOperations.R_ },
                Operations.L_ => new SingleOperations[] { SingleOperations.L_ },
                Operations.U_ => new SingleOperations[] { SingleOperations.U_ },
                Operations.D_ => new SingleOperations[] { SingleOperations.D_ },
                Operations.B_ => new SingleOperations[] { SingleOperations.B_ },
                Operations.F_ => new SingleOperations[] { SingleOperations.F_ },

                Operations.R2 => new SingleOperations[] { SingleOperations.R, SingleOperations.R },
                Operations.L2 => new SingleOperations[] { SingleOperations.L, SingleOperations.L },
                Operations.U2 => new SingleOperations[] { SingleOperations.U, SingleOperations.U },
                Operations.D2 => new SingleOperations[] { SingleOperations.D, SingleOperations.D },
                Operations.B2 => new SingleOperations[] { SingleOperations.B, SingleOperations.B },
                Operations.F2 => new SingleOperations[] { SingleOperations.F, SingleOperations.F },

                Operations.M => new SingleOperations[] { SingleOperations.M },
                Operations.E => new SingleOperations[] { SingleOperations.E },
                Operations.S => new SingleOperations[] { SingleOperations.S },

                Operations.M_ => new SingleOperations[] { SingleOperations.M_ },
                Operations.E_ => new SingleOperations[] { SingleOperations.E_ },
                Operations.S_ => new SingleOperations[] { SingleOperations.S_ },

                Operations.M2 => new SingleOperations[] { SingleOperations.M, SingleOperations.M },
                Operations.E2 => new SingleOperations[] { SingleOperations.E, SingleOperations.E },
                Operations.S2 => new SingleOperations[] { SingleOperations.S, SingleOperations.S },

                Operations.x => new SingleOperations[] { SingleOperations.R, SingleOperations.M_, SingleOperations.L_ },
                Operations.y => new SingleOperations[] { SingleOperations.U, SingleOperations.E_, SingleOperations.D_ },
                Operations.z => new SingleOperations[] { SingleOperations.F, SingleOperations.S,  SingleOperations.B_ },

                Operations.x_ => new SingleOperations[] { SingleOperations.R_, SingleOperations.M, SingleOperations.L },
                Operations.y_ => new SingleOperations[] { SingleOperations.U_, SingleOperations.E, SingleOperations.D },
                Operations.z_ => new SingleOperations[] { SingleOperations.F_, SingleOperations.S_, SingleOperations.B },

                Operations.x2 => new SingleOperations[] { SingleOperations.R, SingleOperations.R, SingleOperations.M_, SingleOperations.M_, SingleOperations.L_, SingleOperations.L_ },
                Operations.y2 => new SingleOperations[] { SingleOperations.U, SingleOperations.U, SingleOperations.E_, SingleOperations.E_, SingleOperations.D_, SingleOperations.D_ },
                Operations.z2 => new SingleOperations[] { SingleOperations.F, SingleOperations.F, SingleOperations.S, SingleOperations.S, SingleOperations.B_, SingleOperations.B_ },

                Operations.Rw => new SingleOperations[] { SingleOperations.R, SingleOperations.M_ },
                Operations.Lw => new SingleOperations[] { SingleOperations.L, SingleOperations.M },
                Operations.Uw => new SingleOperations[] { SingleOperations.U, SingleOperations.E_ },
                Operations.Dw => new SingleOperations[] { SingleOperations.D, SingleOperations.E },
                Operations.Bw => new SingleOperations[] { SingleOperations.B, SingleOperations.S_ },
                Operations.Fw => new SingleOperations[] { SingleOperations.F, SingleOperations.S },

                Operations.Rw_ => new SingleOperations[] { SingleOperations.R_, SingleOperations.M },
                Operations.Lw_ => new SingleOperations[] { SingleOperations.L_, SingleOperations.M_ },
                Operations.Uw_ => new SingleOperations[] { SingleOperations.U_, SingleOperations.E },
                Operations.Dw_ => new SingleOperations[] { SingleOperations.D_, SingleOperations.E_ },
                Operations.Bw_ => new SingleOperations[] { SingleOperations.B_, SingleOperations.S },
                Operations.Fw_ => new SingleOperations[] { SingleOperations.F_, SingleOperations.S_ },

                Operations.Rw2 => new SingleOperations[] { SingleOperations.R, SingleOperations.R, SingleOperations.M_, SingleOperations.M_ },
                Operations.Lw2 => new SingleOperations[] { SingleOperations.L, SingleOperations.L, SingleOperations.M, SingleOperations.M },
                Operations.Uw2 => new SingleOperations[] { SingleOperations.U, SingleOperations.U, SingleOperations.E_, SingleOperations.E_ },
                Operations.Dw2 => new SingleOperations[] { SingleOperations.D, SingleOperations.D, SingleOperations.E, SingleOperations.E },
                Operations.Bw2 => new SingleOperations[] { SingleOperations.B, SingleOperations.B, SingleOperations.S_, SingleOperations.S_ },
                Operations.Fw2 => new SingleOperations[] { SingleOperations.F, SingleOperations.F, SingleOperations.S, SingleOperations.S },

                Operations.NONE => new SingleOperations[] { },
                _ => throw new NotImplementedException()
            };
        }

        /// 180“x‰ñ“]Œn‚Ì‰ñ“]‘€ì‚Ìê‡TRUE‚ğ•Ô‚·.NONE‚ğŠÜ‚Ş‚»‚êˆÈŠO‚ÍFALSE.
        public static bool IsDouble(Operations oper)
        {
            return oper == Operations.R2  || oper == Operations.L2  || oper == Operations.U2 ||
                   oper == Operations.D2  || oper == Operations.B2  || oper == Operations.F2 ||
                   oper == Operations.M2  || oper == Operations.E2  || oper == Operations.S2 ||
                   oper == Operations.x2  || oper == Operations.y2  || oper == Operations.z2 ||
                   oper == Operations.Rw2 || oper == Operations.Lw2 || oper == Operations.Uw2 ||
                   oper == Operations.Dw2 || oper == Operations.Bw2 || oper == Operations.Fw2;
        }
    }
}
