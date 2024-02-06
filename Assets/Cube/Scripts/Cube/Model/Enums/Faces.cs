namespace MurakamiRyujirou.Cube
{
    /// 面.
    public enum Faces
    {
        NONE  = -1, // なくてもOKだが、回転操作から回転すべき面を求めるような処理で例外を返すケースを考慮してNONEを作っておいた.
        RIGHT = 0,
        LEFT  = 1,
        UP    = 2,
        DOWN  = 3,
        BACK  = 4,
        FRONT = 5
    }

}