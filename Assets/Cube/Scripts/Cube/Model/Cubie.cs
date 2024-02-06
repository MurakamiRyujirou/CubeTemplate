using System;

namespace MurakamiRyujirou.Cube
{
    /// �L���[�u���\������P�̃u���b�N���L���[�r�[�ƌĂ�.
    [Serializable]
    public class Cubie
    {
        // �L���[�r�[�̖ʂ̐�.Faces�Œ�`����Ă͂��邪�A�}�W�b�N�i���o�[��6���g��Ȃ��悤�ɒ�`.
        public const int NUMBER_OF_FACES = 6;

        // ���݂̎p��(��]�p�x)�ł̔z�F.
        public ColorScheme Colors { get; private set; }

        // ���Ƃ��ƍ݂����ʒu.���ꂪ�Ȃ���CubieView���Ή��t�����̂�������Ȃ��̂ŕێ�.
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }

        /// default constructor.
        public Cubie()
        {
            Colors = new ColorScheme();
            X = 0;
            Y = 0;
            Z = 0;
        }

        /// copy constructor.
        public Cubie(Cubie cubie)
        {
            Colors = new ColorScheme(cubie.Colors);
            X = cubie.X;
            Y = cubie.Y;
            Z = cubie.Z;
        }

        /// constructor with fields.
        public Cubie(ColorScheme c, int x, int y, int z)
        {
            Colors = new ColorScheme(c);
            X = x;
            Y = y;
            Z = z;
        }

        /// �L���[�r�[�̌��݂̎p��(Rx,Ry,Rz�ŉ�]��̏��)�Ŏw��ʂ̐F��Ԃ�.
        public PanelColors GetColor(Faces face)
        {
            return Colors.GetColor(face);
        }

        /// ��]���Ǝ��v��肩�ۂ��̎w���ɕ����ăL���[�r�[����]������.
        /// <param name="axis">��]��.</param>
        /// <param name="isClockwise">���v��肩�ۂ�.</param>
        public void Rotate(Axes axis, bool isClockwise)
        {
            Colors.Rotate(axis, isClockwise);
        }

        // -------- OVERRIDE --------

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (obj.GetType() != this.GetType()) return false;
            Cubie target = (Cubie)obj;
            return target.X  == X  && target.Y  == Y  && target.Z  == Z  &&
                   target.Colors.Equals(Colors);
        }

        public override int GetHashCode()
        {
            int hash = 0;
            hash += hash * 31 + X;
            hash += hash * 31 + Y;
            hash += hash * 31 + Z;
            hash += hash * 31 + Colors.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return "x="  + X  + ",y="  + Y  + ",z="  + Z  +
                   ",color=" + Colors.ToString();
        }
    }
}