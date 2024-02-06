namespace MurakamiRyujirou.Cube
{
    /// 単層系の90度回転操作.
    public enum SingleOperations
    {
        // 単層回し(外側).
        R,  L,  U,  D,  B,  F,
        R_, L_, U_, D_, B_, F_,

        // 単層回し(内側).スライス系操作.
        M,  E,  S,
        M_, E_, S_
    }
}