namespace MurakamiRyujirou.Cube
{
    /// 回転軸.
    public enum Axes
    {
        NONE = -1, // なくてもOKだが、回転操作から回転軸を求めるような処理で例外を返すケースを考慮してNONEを作っておいた.
        X = 0,
        Y = 1,
        Z = 2
    }
}