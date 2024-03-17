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

        public static Operations GetReverse(Operations oper)
        {
            return oper switch
            {
                Operations.R => Operations.R_,
                Operations.L => Operations.L_,
                Operations.U => Operations.U_,
                Operations.D => Operations.D_,
                Operations.B => Operations.B_,
                Operations.F => Operations.F_,
                Operations.R_ => Operations.R,
                Operations.L_ => Operations.L,
                Operations.U_ => Operations.U,
                Operations.D_ => Operations.D,
                Operations.B_ => Operations.B,
                Operations.F_ => Operations.F,
                Operations.M => Operations.M_,
                Operations.E => Operations.E_,
                Operations.S => Operations.S_,
                Operations.M_ => Operations.M,
                Operations.E_ => Operations.E,
                Operations.S_ => Operations.S,
                Operations.x => Operations.x_,
                Operations.y => Operations.y_,
                Operations.z => Operations.z_,
                Operations.x_ => Operations.x,
                Operations.y_ => Operations.y,
                Operations.z_ => Operations.z,
                Operations.Rw => Operations.Rw_,
                Operations.Lw => Operations.Lw_,
                Operations.Uw => Operations.Uw_,
                Operations.Dw => Operations.Dw_,
                Operations.Bw => Operations.Bw_,
                Operations.Fw => Operations.Fw_,
                Operations.Rw_ => Operations.Rw,
                Operations.Lw_ => Operations.Lw,
                Operations.Uw_ => Operations.Uw,
                Operations.Dw_ => Operations.Dw,
                Operations.Bw_ => Operations.Bw,
                Operations.Fw_ => Operations.Fw,
                _ => oper
            };
        }

        public static Operations GetDouble(Operations oper)
        {
            return oper switch
            {
                Operations.R => Operations.R2,
                Operations.L => Operations.L2,
                Operations.U => Operations.U2,
                Operations.D => Operations.D2,
                Operations.B => Operations.B2,
                Operations.F => Operations.F2,
                Operations.R_ => Operations.R2,
                Operations.L_ => Operations.L2,
                Operations.U_ => Operations.U2,
                Operations.D_ => Operations.D2,
                Operations.B_ => Operations.B2,
                Operations.F_ => Operations.F2,
                Operations.M => Operations.M2,
                Operations.E => Operations.E2,
                Operations.S => Operations.S2,
                Operations.M_ => Operations.M2,
                Operations.E_ => Operations.E2,
                Operations.S_ => Operations.S2,
                Operations.x => Operations.x2,
                Operations.y => Operations.y2,
                Operations.z => Operations.z2,
                Operations.x_ => Operations.x2,
                Operations.y_ => Operations.y2,
                Operations.z_ => Operations.z2,
                Operations.Rw => Operations.Rw2,
                Operations.Lw => Operations.Lw2,
                Operations.Uw => Operations.Uw2,
                Operations.Dw => Operations.Dw2,
                Operations.Bw => Operations.Bw2,
                Operations.Fw => Operations.Fw2,
                Operations.Rw_ => Operations.Rw2,
                Operations.Lw_ => Operations.Lw2,
                Operations.Uw_ => Operations.Uw2,
                Operations.Dw_ => Operations.Dw2,
                Operations.Bw_ => Operations.Bw2,
                Operations.Fw_ => Operations.Fw2,
                _ => oper
            };
        }
    }
}
