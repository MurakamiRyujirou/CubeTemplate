using System.Collections.Generic;

namespace MurakamiRyujirou.Cube
{
    /// 予め設定した連続した回転操作を行うクラス.
    /// シンプルにリストに回転操作を複数登録し、順番に吐き出すだけ.
    public class AutoRotator
    {
        /// 連続した回転操作を格納するリスト.
        /// 処理開始を指示するとリストに連続回転操作が格納される.
        /// このリストは勝手に空になったり減ったりせず、次の指示で新しいリストに差し替えられる.
        private List<Operations> operations;

        /// 回転操作のリストに対するインデックス.
        private int index;

        /// 連続回転操作をする(回転操作のリストをセットする).
        /// 実際の回転操作はCubeRotatorが担当する.
        /// <param name="opers">回転操作の配列.</param>
        public void Setup(Operations[] opers)
        {
            operations = new List<Operations>(opers);
            index = 0;
        }

        /// 回転操作のリストから、一つ回転操作を取得する.
        /// 取得後、インデックスが一つ進む.
        /// 回転操作開始後に呼び出される想定.
        /// <returns>回転操作.</returns>
        public Operations GetOperation()
        {
            // オリジナル回転操作が未登録または登録件数が0件、またはインデックスが登録件数以上進んでいた場合、
            // 回転なし(Operations.NONE)を返す.
            if (operations == null || operations.Count <= index)
            {
                return Operations.NONE;
            }
            return operations[index++];
        }

        /// オリジナル回転が設定済か否かを返す.
        /// オリジナル回転操作が1個以上登録されているか否かで判断する.
        /// <returns>TRUE:予約あり.FALSE:予約なしまたは全て回転終わった.</returns>
        public bool IsReserved()
        {
            return operations != null && operations.Count > index;
        }
    }
}
