using UnityEngine;

namespace MurakamiRyujirou.Cube
{
    public class CubeViewFactory : MonoBehaviour
    {
        [Header("cubieFactory")]
        [SerializeField]
        private CubieViewFactory cubieFactory;

        [Header("cubeViewPrefab")]
        [SerializeField]
        private GameObject cubeViewPrefab;

        /// キューブモデルから、キューブビューを生成する.
        public CubeView Create(Cube cube)
        {
            int size = cube.Size;
            CubieView[,,] cubieViews = new CubieView[size, size, size];
            GameObject cubeViewObject = Instantiate(cubeViewPrefab);

            // キューブのサイズによりキュービーの表示位置を調整する.
            // size = 3. (x, y, z) = (1.0, 1.0, 1.0)
            // size = 2. (x, y, z) = (0.5, 0.5, 0.5)
            // size = 1. (x, y, z) = (0.0, 0.0, 0.0)
            float d = (size == 1) ? 0f : (size == 2) ? -0.5f : -1.0f;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        // キュービービューの座標はdで微調整する.
                        Vector3 position = new(x + d, y + d, z + d);

                        // モデルから配色を取得し、同じ配色のビューを生成する.
                        ColorScheme colorScheme = cube.GetColorScheme(x, y, z);

                        CubieView cv = cubieFactory.CreateView(cubeViewObject.transform, colorScheme, position, Quaternion.identity, x, y, z);

                        // モデルと同じように3次元配列に格納する.
                        cubieViews[x, y, z] = cv;
                    }
                }
            }
            CubeView cubeView = cubeViewObject.GetComponent<CubeView>();
            cubeView.SetCubieViews(cubieViews);
            return cubeView;
        }
    }
}