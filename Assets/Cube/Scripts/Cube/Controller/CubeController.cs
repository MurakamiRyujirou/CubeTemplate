using UnityEngine;

namespace MurakamiRyujirou.Cube
{
    /// キューブのビューとモデルを管理するクラス.
    public class CubeController
    {
        /// キューブモデル.
        public Cube Cube { get; private set; }

        /// キューブビュー.
        public CubeView CubeView { get; private set; }

        /// 自動回転を行うためのクラス.
        private AutoRotator autoRotator;

        public bool IsRotating { get { return CubeView.IsRotating; } }

        /// constructor.
        public CubeController(Cube cube, CubeView cubeView)
        {
            this.Cube = cube;
            this.CubeView = cubeView;

            // ビューで回転完了したタイミングでRotateDoneを実行するようにする.
            // これによりビューが回転し終えたあと、モデルが回転するように連動させることができる.
            this.CubeView.completeRotate = RotateDone;

            this.autoRotator = new();
        }

        /// 更新処理.回転途中の場合、回転を進める.
        /// 回転が終わっていて自動回転中の場合、次の回転を行う.
        public void OnUpdate()
        {
            // 回転途中なら回転を進める.
            CubeView.OnUpdate();

            // 自動回転中かつ回転していない場合、次の回転操作を適用する.
            if (autoRotator.IsReserved() && !CubeView.IsRotating)
            {
                Rotate(autoRotator.GetOperation());
            }
        }

        /// 回転スピードをセットする.
        public void SetRotateSpeed(float value)
        {
            CubeView.SetRotateSpeed(value);
        }

        /// 回転処理を行う.回転処理を開始できた場合はTRUEを返す.
        public bool Rotate(Operations oper)
        {
            // 現在回転途中の場合、新しい回転処理は行わず本処理は終了する.
            if (CubeView.IsRotating) return false;

            // 回転すべきキュービーの、キューブビュー上の配列の添え字(座標)を取得する.
            Vector3Int[] posList = Cube.GetRotateIndexes(oper);

            // 座標に応じたビューを取得.
            CubieView[] rotateCubieViews = CubeView.GetCubieViews(posList);

            // ビューと回転操作を引数に回転処理を行う.
            return CubeView.Rotate(rotateCubieViews, oper);
        }

        /// キューブに自動回転操作を指示する.
        /// <param name="opers">複数の回転操作.配列に格納して渡す.</param>
        /// <returns>TRUE:処理を実施した.</returns>
        public bool AutoRotate(Operations[] opers)
        {
            // 自動回転中であれば新たな処理は受け付けない.
            if (autoRotator.IsReserved()) return false;

            // 手動回転の途中である場合も受付ない.
            if (CubeView.IsRotating) return false;

            // オリジナル回転を担当するクラスに回転操作をセットする.
            autoRotator.Setup(opers);

            // 最初の回転処理を開始する.
            return Rotate(autoRotator.GetOperation());
        }

        /// キューブをリセットする.
        /// モデルをリセットし、その内容をビューに反映する.
        public void Reset()
        {
            // 自動回転はリセット機能を設けていないので新たなインスタンスをセットする.
            if (autoRotator.IsReserved()) autoRotator = new AutoRotator();

            // キューブモデルを再生成する.同じサイズを引き継ぐ.
            Cube = new Cube();

            // キューブビューは回転停止を指示する.
            CubeView.Stop();
        }

        /// ビューのキューブ回転クラスから回転操作の完了通知がくるとコールされるメソッド.
        private void RotateDone(Operations oper, bool isOperationDone)
        {
            // 回転操作を実施して完了通知が届いていた場合には、モデル側も回転操作を反映する.
            if (isOperationDone)
            {
                // モデルを回転させる.
                Cube.Rotate(oper);

                // モデルとビューが一致することを確認する.
                // CubeView.Adjust(Cube);
            }
        }
    }
}