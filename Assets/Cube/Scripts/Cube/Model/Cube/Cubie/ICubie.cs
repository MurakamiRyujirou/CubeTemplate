namespace MurakamiRyujirou.Cube
{
    public interface ICubie
    {
        /// 初期位置.
        Position InitialPosition { get; }

        /// 初期パネル.
        PanelTable InitialPanels { get; }

        /// 現在位置.
        Position CurrentPosition { get; set; }

        /// 現在パネル.
        PanelTable CurrentPanels { get; }
    }
}