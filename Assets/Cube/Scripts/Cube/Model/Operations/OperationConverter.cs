using System.Collections.Generic;

namespace MurakamiRyujirou.Cube
{
    public class OperationConverter
    {
        public static Operations[] Convert(string[] solutions)
        {
            List<Operations> ret = new();
            foreach (string solution in solutions)
            {
                Operations oper = Convert(solution);
                ret.Add(oper);
            }
            return ret.ToArray();
        }

        public static Operations Convert(string solution)
        {
            return solution switch
            {
                "R" => Operations.R,
                "L" => Operations.L,
                "U" => Operations.U,
                "D" => Operations.D,
                "B" => Operations.B,
                "F" => Operations.F,
                "R'" => Operations.R_,
                "L'" => Operations.L_,
                "U'" => Operations.U_,
                "D'" => Operations.D_,
                "B'" => Operations.B_,
                "F'" => Operations.F_,
                "R2" => Operations.R2,
                "L2" => Operations.L2,
                "U2" => Operations.U2,
                "D2" => Operations.D2,
                "B2" => Operations.B2,
                "F2" => Operations.F2,
                "M" => Operations.M,
                "E" => Operations.E,
                "S" => Operations.S,
                "M'" => Operations.M_,
                "E'" => Operations.E_,
                "S'" => Operations.S_,
                "M2" => Operations.M2,
                "E2" => Operations.E2,
                "S2" => Operations.S2,
                "x" => Operations.x,
                "y" => Operations.y,
                "z" => Operations.z,
                "x'" => Operations.x_,
                "y'" => Operations.y_,
                "z'" => Operations.z_,
                "x2" => Operations.x2,
                "y2" => Operations.y2,
                "z2" => Operations.z2,
                "Rw" => Operations.Rw,
                "Lw" => Operations.Lw,
                "Uw" => Operations.Uw,
                "Dw" => Operations.Dw,
                "Bw" => Operations.Bw,
                "Fw" => Operations.Fw,
                "Rw'" => Operations.Rw_,
                "Lw'" => Operations.Lw_,
                "Uw'" => Operations.Uw_,
                "Dw'" => Operations.Dw_,
                "Bw'" => Operations.Bw_,
                "Fw'" => Operations.Fw_,
                "Rw2" => Operations.Rw2,
                "Lw2" => Operations.Lw2,
                "Uw2" => Operations.Uw2,
                "Dw2" => Operations.Dw2,
                "Bw2" => Operations.Bw2,
                "Fw2" => Operations.Fw2,
                _ => Operations.NONE
            };
        }

        public static string Convert(Operations solution)
        {
            return solution switch
            {
                Operations.R => "R",
                Operations.L => "L",
                Operations.U => "U",
                Operations.D => "D",
                Operations.B => "B",
                Operations.F => "F",
                Operations.R_ => "R'",
                Operations.L_ => "L'",
                Operations.U_ => "U'",
                Operations.D_ => "D'",
                Operations.B_ => "B'",
                Operations.F_ => "F'",
                Operations.R2 => "R2",
                Operations.L2 => "L2",
                Operations.U2 => "U2",
                Operations.D2 => "D2",
                Operations.B2 => "B2",
                Operations.F2 => "F2",
                Operations.M => "M",
                Operations.E => "E",
                Operations.S => "S",
                Operations.M_ => "M'",
                Operations.E_ => "E'",
                Operations.S_ => "S'",
                Operations.M2 => "M2",
                Operations.E2 => "E2",
                Operations.S2 => "S2",
                Operations.x => "x",
                Operations.y => "y",
                Operations.z => "z",
                Operations.x_ => "x'",
                Operations.y_ => "y'",
                Operations.z_ => "z'",
                Operations.x2 => "x2",
                Operations.y2 => "y2",
                Operations.z2 => "z2",
                Operations.Rw => "Rw",
                Operations.Lw => "Lw",
                Operations.Uw => "Uw",
                Operations.Dw => "Dw",
                Operations.Bw => "Bw",
                Operations.Fw => "Fw",
                Operations.Rw_ => "Rw'",
                Operations.Lw_ => "Lw'",
                Operations.Uw_ => "Uw'",
                Operations.Dw_ => "Dw'",
                Operations.Bw_ => "Bw'",
                Operations.Fw_ => "Fw'",
                Operations.Rw2 => "Rw2",
                Operations.Lw2 => "Lw2",
                Operations.Uw2 => "Uw2",
                Operations.Dw2 => "Dw2",
                Operations.Bw2 => "Bw2",
                Operations.Fw2 => "Fw2",
            };
        }

        public static Operations Convert(PureOperations o)
        {
            return o switch
            {
                PureOperations.R => Operations.R,
                PureOperations.L => Operations.L,
                PureOperations.U => Operations.U,
                PureOperations.D => Operations.D,
                PureOperations.B => Operations.B,
                PureOperations.F => Operations.F,
            };
        }
    }
}
