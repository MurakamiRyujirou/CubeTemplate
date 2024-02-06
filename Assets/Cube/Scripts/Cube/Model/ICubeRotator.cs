namespace MurakamiRyujirou.Cube
{
    /// キューブモデルの回転インタフェース.
    public interface ICubeRotator
    {
        void Rotate(Cubie[,,] cubies, Operations oper);
    }
}