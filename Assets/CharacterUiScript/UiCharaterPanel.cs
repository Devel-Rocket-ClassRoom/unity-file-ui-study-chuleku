using TMPro;
using UnityEngine;
using System.Linq;

public class UiCharaterPanel : GenericWindow
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;
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
        currentData = null;
        uiCharaterInfo.SetEmpty();
        OnLoad();
    }
    public override void Open(SaveCharaterData data)
    {
        base.Open();
        currentData = null;
        uiCharaterInfo.SetEmpty();
        uiCharaterSlotList.Add(data);
        OnSave();
        OnLoad();
        
    }
    public override void Close()
    {
        base.Close();
       
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
        SaveLoadManager.Data.charaterid = uiCharaterSlotList.GetSaveCharaterDataList().ToList();
        SaveLoadManager.Data.charaterSortingOptions = (UiCharaterList.SortingOptions)sorting.value;
        SaveLoadManager.Data.charaterFilteringOptions = (UiCharaterList.FilteringOptions)filtering.value;
        SaveLoadManager.Save();
    }
    public void OnLoad()
    {
        OnChangeSorting((int)SaveLoadManager.Data.charaterSortingOptions);
        OnChangeFiltering((int)SaveLoadManager.Data.charaterFilteringOptions);
        uiCharaterSlotList.SetSaveCharaterData(SaveLoadManager.Data.charaterid);
        SaveLoadManager.Load();
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
        if(uiCharaterInfo.imageIcon == null)
        {
            return;
        }
        OnSave();
        windowManager.Open(1, currentData);
    }
}
