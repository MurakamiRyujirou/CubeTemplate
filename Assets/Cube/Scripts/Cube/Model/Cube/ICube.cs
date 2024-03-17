using MurakamiRyujirou.Cube;

namespace MurakamiRyujirou.Cube
{
    public interface ICube
    {
        /// 回転処理を行う.
        /// キューブの回転操作でキュービー配列の位置を入れ替える.
        void Rotate(Operations oper);

        /// 指定した回転操作で動かす対象のキュービーの配列を取得する.
        ICubie[] GetRotateCubies(Operations oper);

        /// 指定した回転操作で動かす対象のキュービー座標配列を取得する.
        Position[] GetRotatePositions(Operations oper);

        /// 指定した回転操作で動かす対象のキュービーの初期位置を取得する.
        Position[] GetRotateInitialPositions(Operations oper);

        /// 指定の座標リストに現在位置するキュービーを取得する.
        ICubie[] GetCubies(Position[] posList);

        /// 指定の座標に現在位置するキュービーを取得する.
        ICubie GetCubie(Position pos);

        /// 指定の座標に現在位置するキュービーのパネル情報を取得する.
        PanelTable GetPanelTable(Position pos);

        /// 指定の座標に現在位置するキュービーの、指定面のパネル情報を取得する.
        IPanel GetPanel(Position pos, Faces face);

        /// 指定の座標に初期状態で存在したキュービーを取得する.
        ICubie GetInitialCubie(Position pos);

        /// 指定の座標に初期状態で存在したキュービーの、指定面のパネル情報を取得する.
        IPanel GetInitialPanel(Position pos, Faces face);
    }
}