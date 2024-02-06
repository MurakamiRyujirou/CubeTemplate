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

        /// �L���[�u���f������A�L���[�u�r���[�𐶐�����.
        public CubeView Create(Cube cube)
        {
            int size = cube.Size;
            CubieView[,,] cubieViews = new CubieView[size, size, size];
            GameObject cubeViewObject = Instantiate(cubeViewPrefab);

            // �L���[�u�̃T�C�Y�ɂ��L���[�r�[�̕\���ʒu�𒲐�����.
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
                        // �L���[�r�[�r���[�̍��W��d�Ŕ���������.
                        Vector3 position = new(x + d, y + d, z + d);

                        // ���f������z�F���擾���A�����z�F�̃r���[�𐶐�����.
                        ColorScheme colorScheme = cube.GetColorScheme(x, y, z);

                        CubieView cv = cubieFactory.CreateView(cubeViewObject.transform, colorScheme, position, Quaternion.identity, x, y, z);

                        // ���f���Ɠ����悤��3�����z��Ɋi�[����.
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