using System;
using System.Collections.Generic;
using UnityEngine;

namespace MurakamiRyujirou.Cube
{
    public class CubieView : MonoBehaviour
    {
        [SerializeField]
        private Material panelMaterialNone;

        [SerializeField]
        private Material panelMaterialRed;

        [SerializeField]
        private Material panelMaterialOrange;

        [SerializeField]
        private Material panelMaterialWhite;

        [SerializeField]
        private Material panelMaterialYellow;

        [SerializeField]
        private Material panelMaterialBlue;

        [SerializeField]
        private Material panelMaterialGreen;

        private Dictionary<Faces, GameObject> panels;

        /// キュービーのパネルをセットする.
        public void AddPanel(Faces face, GameObject panel)
        {
            if (panels == null)
                panels = new();
            panels.Add(face, panel);
        }

        /// 指定の配色情報に併せて六面全てのパネルの色をセットする.
        public void SetColors(ColorScheme colorSchemes)
        {
            for (int index = 0; index < 6; index++)
            {
                Faces face = (Faces)Enum.ToObject(typeof(Faces), index);
                SetColor(face, colorSchemes.Colors[(int)face]);
            }
        }

        /// 指定の面のパネルを指定の色に設定する.
        public void SetColor(Faces face, PanelColors color)
        {
            GameObject panel = panels[face];
            panel.GetComponent<MeshRenderer>().material = GetMaterial(color);
            panel.SetActive(color != PanelColors.NONE);
        }

        /// パネルの色からマテリアルを返す.
        /// <param name="color">パネルの色.</param>
        /// <returns>マテリアル.</returns>
        private Material GetMaterial(PanelColors color)
        {
            return color switch
            {
                PanelColors.NONE   => panelMaterialNone,
                PanelColors.RED    => panelMaterialRed,
                PanelColors.ORANGE => panelMaterialOrange,
                PanelColors.WHITE  => panelMaterialWhite,
                PanelColors.YELLOW => panelMaterialYellow,
                PanelColors.BLUE   => panelMaterialBlue,
                PanelColors.GREEN  => panelMaterialGreen,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
