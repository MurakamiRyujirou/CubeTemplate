namespace MurakamiRyujirou.Cube
{
    /// キューブモデルの回転インタフェース.
    public interface ICubeRotator
    {
        void Rotate(ICubie[,,] cubies, Operations oper);
    }
}