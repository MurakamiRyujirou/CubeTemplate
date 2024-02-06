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
    }
}
