namespace MurakamiRyujirou.Cube
{
    /// ��]����.
    /// 18+9+9+18=54
    public enum Operations
    {
        // �P�w��(�O��).
        R,  L,  U,  D,  B,  F,
        R_, L_, U_, D_, B_, F_,
        R2, L2, U2, D2, B2, F2,

        // �P�w��(����).�X���C�X�n����.
        M,  E,  S,
        M_, E_, S_,
        M2, E2, S2,

        // �����ւ��n����.
        x,  y,  z,
        x_, y_, z_,
        x2, y2, z2,

        // ��w�񂵌n����.
        Rw,  Lw,  Uw,  Dw,  Bw,  Fw,
        Rw_, Lw_, Uw_, Dw_, Bw_, Fw_,
        Rw2, Lw2, Uw2, Dw2, Bw2, Fw2,

        // ����Ȃ�.
        NONE,
    }
}