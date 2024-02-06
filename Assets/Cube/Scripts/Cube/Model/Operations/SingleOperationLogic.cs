using System;

namespace MurakamiRyujirou.Cube
{
    /// �P�w�n�̉�]����Ɋ֘A�������W�b�N.
    public class SingleOperationLogic
    {
        /// ��]���삩���]�����擾����.
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

        /// ��]���삪�X���C�X�n���ۂ��Ԃ�.
        public static bool IsSlice(SingleOperations oper)
        {
            return oper == SingleOperations.M  || oper == SingleOperations.E  || oper == SingleOperations.S  ||
                   oper == SingleOperations.M_ || oper == SingleOperations.E_ || oper == SingleOperations.S_;
        }

        /// ���v���ł̉�]���삩�ۂ���Ԃ�.
        public static bool IsClockwise(SingleOperations oper)
        {
            return oper == SingleOperations.R || oper == SingleOperations.M_ || oper == SingleOperations.L_ ||
                   oper == SingleOperations.U || oper == SingleOperations.E_ || oper == SingleOperations.D_ ||
                   oper == SingleOperations.B || oper == SingleOperations.S_ || oper == SingleOperations.F_;
        }

        /// ��]���삩���]����ʂ��擾����.
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
