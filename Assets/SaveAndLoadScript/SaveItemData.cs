using Newtonsoft.Json;
using System;
using System.Data;
using UnityEngine;

[System.Serializable]
public class SaveItemData
{
    public Guid instanceId { get; set; }
    [JsonConverter(typeof(ItemDataConverter))]
    public ItemData itemData {  get; set; }
    public DateTime creationTime {  get; set; }

    public static SaveItemData GetRandomItem()
    {
        SaveItemData newItem = new SaveItemData();
        newItem.itemData = DataTableManager.ItemTable.GetRandom();
        return newItem;
    }
    public SaveItemData()
    {
        instanceId = Guid.NewGuid();
        creationTime = DateTime.Now;
    }
    public override string ToString()
    {
        return $"{instanceId}\n{creationTime}\n{itemData.Id}";
    }

}
