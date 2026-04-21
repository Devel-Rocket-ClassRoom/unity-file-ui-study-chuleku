using UnityEngine;

public class ItemWindow : GenericWindow
{
    public Uicharater charaterInfo;
    public UiItemInfo itemInfo;
    private SaveCharaterData saveCharaterData;
    private SaveItemData saveItemData;
    public override void Open(SaveCharaterData data)
    {
        base.Open();
        charaterInfo.SetSaveItemData(data);
    }
    public override void Close()
    {
        base.Close();
    }

    public void OnClickEquip()
    {

    }

    public void OnClickBack()
    {
        windowManager.Open(0);
    }

}
