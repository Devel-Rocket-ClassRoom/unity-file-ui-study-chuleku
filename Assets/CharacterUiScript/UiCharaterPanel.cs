using TMPro;
using UnityEngine;
using System.Linq;

public class UiCharaterPanel : GenericWindow
{
    public TMP_Dropdown sorting;
    public UiCharaterList uiCharaterSlotList;
    public Uicharater uiCharaterInfo;
    private SaveCharaterData currentData;
    private void OnEnable()
    {
        OnLoad();
    }
    public override void Open()
    {
        base.Open();
        uiCharaterInfo.SetEmpty();
    }
    public void OnChangeSorting(int index)
    {
        uiCharaterSlotList.Sorting = (UiCharaterList.SortingOptions)index;
    }
    public void OnChangeFiltering(int index)
    {
        uiCharaterSlotList.Filtering = (UiCharaterList.FilteringOptions)index;
    }
    public void OnChangeLanguage(int index)
    {
        uiCharaterSlotList.Language = (UiCharaterList.LanguageOptions)index;
    }
    public void OnSave()
    {
        SaveLoadManager.Data.charaterid = uiCharaterSlotList.GetSaveCharaterDataList().Where(x => x.IsModified).ToList();
        SaveLoadManager.Data.charaterSortingOptions = (UiCharaterList.SortingOptions)sorting.value;
        SaveLoadManager.Save();
    }
    public void OnLoad()
    {
        SaveLoadManager.Load();

        OnChangeSorting((int)SaveLoadManager.Data.charaterSortingOptions);
        uiCharaterSlotList.SetSaveItemData(SaveLoadManager.Data.charaterid);
    }
    public void OnCreateItem()
    {
        uiCharaterSlotList.AddRandomItem();
    }
    public void OnRemoveItem()
    {
        uiCharaterSlotList.RemoveItem();
    }
    public void OnClickItem(SaveCharaterData data)
    {
        currentData = data;
        if (currentData == null)
        {
            return;
        }
        windowManager.Open(1, currentData);
    }
}
