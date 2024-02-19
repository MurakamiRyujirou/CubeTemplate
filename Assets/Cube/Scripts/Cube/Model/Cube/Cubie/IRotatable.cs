namespace MurakamiRyujirou.Cube
{
    public interface IRotatable
    {
        /// 指定した面(Faces)のパネルを取得する.
        /// <param name="face">面.</param>
        /// <returns>パネル(IPanel).</returns>
        IPanel GetPanel(Faces face);

        /// 指定した回転軸と時計回りか否かを示すbool値で、
        /// IRotatableを実装済の物体を90度回転させる.
        /// <param name="axis">回転軸.</param>
        /// <param name="isClockwise">TRUE:時計回り.</param>
        void Rotate(Axes axis, bool isClockwise);
    }
}