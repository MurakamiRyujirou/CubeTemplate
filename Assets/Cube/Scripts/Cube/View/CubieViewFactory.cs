using System;
using UnityEngine;

namespace MurakamiRyujirou.Cube
{
    public class CubieViewFactory : MonoBehaviour
    {
        [Header("cubiePrefab")]
        [SerializeField]
        private GameObject cubiePrefab;

        [Header("cubiePanelPrefab")]
        [SerializeField]
        private GameObject cubiePanelPrefab;

        [Header("cornerPanelPrefab")]
        [SerializeField]
        private GameObject cornerPanelPrefab;

        [Header("edgePanelPrefab")]
        [SerializeField]
        private GameObject edgePanelPrefab;

        [Header("centerPanelPrefab")]
        [SerializeField]
        private GameObject centerPanelPrefab;

        /// キュービービューを生成する.
        public CubieView CreateView(Transform parent, PanelTable color, Vector3 position, Quaternion rotation, int x, int y, int z)
        {
            PanelType type = GetPanelType(x, y, z);
            GameObject panelPrefab = GetPanelPrefab(type);

            GameObject cubieViewObject = Instantiate(cubiePrefab, position, rotation, parent);
            CubieView cubieView = cubieViewObject.GetComponent<CubieView>();
            for (int i = 0; i < 6; i++)
            {
                Faces face = (Faces)Enum.ToObject(typeof(Faces), i);
                Vector3 pos = positions[i] + position;
                Quaternion rot = rotations[i] * GetPanelRotate(x, y, z, i);
                IPanel p = color.Get(face);

                // キュービーの位置によって作るパネルを切り替える.
                GameObject panel = Instantiate(panelPrefab, pos, rot, cubieViewObject.transform);

                // キュービーからパネルを指定する必要があり、パネル情報を渡すこととした.
                cubieView.AddPanel(face, panel);

                // パネルに色をセットする.
                cubieView.SetPanel(face, p);
            }
            return cubieView;
        }

        private PanelType GetPanelType(int x, int y, int z)
        {
            if (x == 1 && y == 1 && z == 0) return PanelType.CENTER;
            if (x == 1 && y == 1 && z == 2) return PanelType.CENTER;
            if (x == 1 && y == 0 && z == 1) return PanelType.CENTER;
            if (x == 1 && y == 2 && z == 1) return PanelType.CENTER;
            if (x == 0 && y == 1 && z == 1) return PanelType.CENTER;
            if (x == 2 && y == 1 && z == 1) return PanelType.CENTER;
            if (x == 0 && y == 0 && z == 0) return PanelType.CORNER;
            if (x == 0 && y == 0 && z == 2) return PanelType.CORNER;
            if (x == 0 && y == 2 && z == 0) return PanelType.CORNER;
            if (x == 0 && y == 2 && z == 2) return PanelType.CORNER;
            if (x == 2 && y == 0 && z == 0) return PanelType.CORNER;
            if (x == 2 && y == 0 && z == 2) return PanelType.CORNER;
            if (x == 2 && y == 2 && z == 0) return PanelType.CORNER;
            if (x == 2 && y == 2 && z == 2) return PanelType.CORNER;
            return PanelType.EDGE;
        }

        private Quaternion GetPanelRotate(int x, int y, int z, int i)
        {
            if (x == 0 && y == 0 && z == 0 && i == 1) return Quaternion.Euler(new Vector3(0f, 0f,   0f));
            if (x == 0 && y == 0 && z == 0 && i == 3) return Quaternion.Euler(new Vector3(0f, 0f, 180f));
            if (x == 0 && y == 0 && z == 0 && i == 5) return Quaternion.Euler(new Vector3(0f, 0f, 270f));
            if (x == 0 && y == 0 && z == 2 && i == 1) return Quaternion.Euler(new Vector3(0f, 0f, 270f));
            if (x == 0 && y == 0 && z == 2 && i == 3) return Quaternion.Euler(new Vector3(0f, 0f, 270f));
            if (x == 0 && y == 0 && z == 2 && i == 4) return Quaternion.Euler(new Vector3(0f, 0f,   0f));
            if (x == 0 && y == 2 && z == 0 && i == 1) return Quaternion.Euler(new Vector3(0f, 0f,  90f));
            if (x == 0 && y == 2 && z == 0 && i == 2) return Quaternion.Euler(new Vector3(0f, 0f, 270f));
            if (x == 0 && y == 2 && z == 0 && i == 5) return Quaternion.Euler(new Vector3(0f, 0f, 180f));
            if (x == 0 && y == 2 && z == 2 && i == 1) return Quaternion.Euler(new Vector3(0f, 0f, 180f));
            if (x == 0 && y == 2 && z == 2 && i == 2) return Quaternion.Euler(new Vector3(0f, 0f, 180f));
            if (x == 0 && y == 2 && z == 2 && i == 4) return Quaternion.Euler(new Vector3(0f, 0f,  90f));
            if (x == 2 && y == 0 && z == 0 && i == 0) return Quaternion.Euler(new Vector3(0f, 0f, 270f));
            if (x == 2 && y == 0 && z == 0 && i == 3) return Quaternion.Euler(new Vector3(0f, 0f,  90f));
            if (x == 2 && y == 0 && z == 0 && i == 5) return Quaternion.Euler(new Vector3(0f, 0f,   0f));
            if (x == 2 && y == 0 && z == 2 && i == 0) return Quaternion.Euler(new Vector3(0f, 0f,   0f));
            if (x == 2 && y == 0 && z == 2 && i == 3) return Quaternion.Euler(new Vector3(0f, 0f,   0f));
            if (x == 2 && y == 0 && z == 2 && i == 4) return Quaternion.Euler(new Vector3(0f, 0f, 270f));
            if (x == 2 && y == 2 && z == 0 && i == 0) return Quaternion.Euler(new Vector3(0f, 0f, 180f));
            if (x == 2 && y == 2 && z == 0 && i == 2) return Quaternion.Euler(new Vector3(0f, 0f,   0f));
            if (x == 2 && y == 2 && z == 0 && i == 5) return Quaternion.Euler(new Vector3(0f, 0f,  90f));
            if (x == 2 && y == 2 && z == 2 && i == 0) return Quaternion.Euler(new Vector3(0f, 0f,  90f));
            if (x == 2 && y == 2 && z == 2 && i == 2) return Quaternion.Euler(new Vector3(0f, 0f,  90f));
            if (x == 2 && y == 2 && z == 2 && i == 4) return Quaternion.Euler(new Vector3(0f, 0f, 180f));
            if (x == 0 && y == 0 && z == 1 && i == 1) return Quaternion.Euler(new Vector3(0f, 0f,   0f));
            if (x == 0 && y == 0 && z == 1 && i == 3) return Quaternion.Euler(new Vector3(0f, 0f, 270f));
            if (x == 0 && y == 1 && z == 0 && i == 1) return Quaternion.Euler(new Vector3(0f, 0f,  90f));
            if (x == 0 && y == 1 && z == 0 && i == 5) return Quaternion.Euler(new Vector3(0f, 0f, 270f));
            if (x == 0 && y == 2 && z == 1 && i == 1) return Quaternion.Euler(new Vector3(0f, 0f, 180f));
            if (x == 0 && y == 2 && z == 1 && i == 2) return Quaternion.Euler(new Vector3(0f, 0f, 270f));
            if (x == 0 && y == 1 && z == 2 && i == 1) return Quaternion.Euler(new Vector3(0f, 0f, 270f));
            if (x == 0 && y == 1 && z == 2 && i == 4) return Quaternion.Euler(new Vector3(0f, 0f,  90f));
            if (x == 2 && y == 0 && z == 1 && i == 0) return Quaternion.Euler(new Vector3(0f, 0f,   0f));
            if (x == 2 && y == 0 && z == 1 && i == 3) return Quaternion.Euler(new Vector3(0f, 0f,  90f));
            if (x == 2 && y == 1 && z == 0 && i == 0) return Quaternion.Euler(new Vector3(0f, 0f, 270f));
            if (x == 2 && y == 1 && z == 0 && i == 5) return Quaternion.Euler(new Vector3(0f, 0f,  90f));
            if (x == 2 && y == 2 && z == 1 && i == 0) return Quaternion.Euler(new Vector3(0f, 0f, 180f));
            if (x == 2 && y == 2 && z == 1 && i == 2) return Quaternion.Euler(new Vector3(0f, 0f,  90f));
            if (x == 2 && y == 1 && z == 2 && i == 0) return Quaternion.Euler(new Vector3(0f, 0f,  90f));
            if (x == 2 && y == 1 && z == 2 && i == 4) return Quaternion.Euler(new Vector3(0f, 0f, 270f));
            if (x == 1 && y == 0 && z == 0 && i == 3) return Quaternion.Euler(new Vector3(0f, 0f, 180f));
            if (x == 1 && y == 0 && z == 0 && i == 5) return Quaternion.Euler(new Vector3(0f, 0f,   0f));
            if (x == 1 && y == 2 && z == 0 && i == 2) return Quaternion.Euler(new Vector3(0f, 0f,   0f));
            if (x == 1 && y == 2 && z == 0 && i == 5) return Quaternion.Euler(new Vector3(0f, 0f, 180f));
            if (x == 1 && y == 0 && z == 2 && i == 3) return Quaternion.Euler(new Vector3(0f, 0f,   0f));
            if (x == 1 && y == 0 && z == 2 && i == 4) return Quaternion.Euler(new Vector3(0f, 0f,   0f));
            if (x == 1 && y == 2 && z == 2 && i == 2) return Quaternion.Euler(new Vector3(0f, 0f, 180f));
            if (x == 1 && y == 2 && z == 2 && i == 4) return Quaternion.Euler(new Vector3(0f, 0f, 180f));
            return Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }

        private GameObject GetPanelPrefab(PanelType type)
        {
            return type switch
            {
                PanelType.CENTER => centerPanelPrefab,
                PanelType.CORNER => cornerPanelPrefab,
                PanelType.EDGE   => edgePanelPrefab,
                _ => cubiePanelPrefab
            };
        }

        /// パネルの位置情報(右->左->上->下->奥->前の順番).
        private readonly Vector3[] positions = new Vector3[]
        {
            new Vector3( 0.46f,  0.00f,  0.00f),
            new Vector3(-0.46f,  0.00f,  0.00f),
            new Vector3( 0.00f,  0.46f,  0.00f),
            new Vector3( 0.00f, -0.46f,  0.00f),
            new Vector3( 0.00f,  0.00f,  0.46f),
            new Vector3( 0.00f,  0.00f, -0.46f)
        };

        /// パネルの向き情報(右->左->上->下->奥->前の順番).
        private readonly Quaternion[] rotations = new Quaternion[]
        {
            Quaternion.Euler(new Vector3(  0f, -90f, 0f)),
            Quaternion.Euler(new Vector3(  0f,  90f, 0f)),
            Quaternion.Euler(new Vector3( 90f,   0f, 0f)),
            Quaternion.Euler(new Vector3(-90f,   0f, 0f)),
            Quaternion.Euler(new Vector3(  0f, 180f, 0f)),
            Quaternion.Euler(new Vector3(  0f,   0f, 0f))
        };
    }

    enum PanelType
    {
        CENTER,
        CORNER,
        EDGE
    }
}
