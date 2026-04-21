using UnityEngine;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;

public class UiPanelInventory : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;
    public UiInvenSlotList uiInvenSlotList;
    public UiItemInfo uiItemInfo;

    private void OnEnable()
    {
        OnLoad();
    }
/*    private void Update()
    {
        uiInvenSlotList.UiInfo(uiItemInfo);
    }
*/
    public void OnChangeSorting(int index)
    {
        uiInvenSlotList.Sorting = (UiInvenSlotList.SortingOptions)index;
    }
    public void OnChangeFiltering(int index) 
    {
        uiInvenSlotList.Filtering = (UiInvenSlotList.FilteringOptions)index;
    }
    public void OnChangeLanguage(int index)
    {
        uiInvenSlotList.Language = (UiInvenSlotList.LanguageOptions)index;
    }
    public void OnSave()
    {
        SaveLoadManager.Data.itemids = uiInvenSlotList.GetSaveItemDataList();
        SaveLoadManager.Data.ItemSorting = (UiInvenSlotList.SortingOptions)sorting.value;
        SaveLoadManager.Data.ItemFiltering = (UiInvenSlotList.FilteringOptions)filtering.value;
        SaveLoadManager.Save();
    }
    public void OnLoad()
    {
        SaveLoadManager.Load();
        OnChangeFiltering((int)SaveLoadManager.Data.ItemSorting);
        OnChangeSorting((int)SaveLoadManager.Data.ItemFiltering);
        uiInvenSlotList.SetSaveItemData(SaveLoadManager.Data.itemids);
    }
    public void OnCreateItem()
    {
        uiInvenSlotList.AddRandomItem();
    }
    public void OnRemoveItem()
    {
        uiInvenSlotList.RemoveItem();
    }
}
