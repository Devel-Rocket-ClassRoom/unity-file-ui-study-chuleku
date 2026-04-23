using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class SaveData
{
    public int Version { get; protected set; }
    public abstract SaveData VersionUp();
}
[System.Serializable]
public class SaveDataV1 : SaveData
{

    public string PlayerName { get; set; } = string.Empty;
    public SaveDataV1()
    {
        Version = 1;
    }
    public override SaveData VersionUp()
    {
        var saveData = new SaveDataV2();
        saveData.Name = PlayerName;
        return saveData;
    }
}
[System.Serializable]
public class SaveDataV2 : SaveData
{
    public string Name {  get; set; } = string.Empty;

    public int Gold = 0;

    public SaveDataV2()
    {
        Version = 2;
    }
    public override SaveData VersionUp()
    {
        var saveData = new SaveDataV3();
        saveData.Name = Name;
        saveData.Gold = Gold;
        return saveData;
    }
}
[System.Serializable]
public class SaveDataV3 : SaveDataV2
{
    public List<string> itemids = new List<string>();
/*    public List<string> itemIds = new List<string>
    {
        "Item1",
        "Item2",
        "Item3",
        "Item4"
    };*/
    public SaveDataV3()
    {
        Version = 3;
    }
    public override SaveData VersionUp()
    {
        SaveDataV4 data = new SaveDataV4();
        data.Name = Name;
        data.Gold = Gold;
        foreach(var i  in itemids)
        {
            SaveItemData itemData = new SaveItemData();
            itemData.itemData = DataTableManager.ItemTable.Get(i);
            data.itemids.Add(itemData);
        }
        return data;
    }
}
[System.Serializable]
public class SaveDataV4 : SaveDataV3
{
    public List<SaveItemData> itemids = new List<SaveItemData>();
    public List<SaveCharaterData> charaterid = new List<SaveCharaterData>();
    public UiCharaterList.SortingOptions charaterSortingOptions = new UiCharaterList.SortingOptions();
    public UiCharaterList.FilteringOptions charaterFilteringOptions = new UiCharaterList.FilteringOptions();
    public UiInvenSlotList.SortingOptions ItemSorting = new UiInvenSlotList.SortingOptions();
    public UiInvenSlotList.FilteringOptions ItemFiltering = new UiInvenSlotList.FilteringOptions();
    
    public SaveDataV4()
    {
        Version = 4;
    }
    public override SaveData VersionUp()
    {
        throw new System.NotImplementedException();
    }
}

