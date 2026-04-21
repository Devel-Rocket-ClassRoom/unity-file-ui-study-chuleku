using Newtonsoft.Json;
using System;
using UnityEngine;

public class SaveCharaterData
{
    public Guid instanceId { get; set; }
    [JsonConverter(typeof(CharaterDataConverter))]
    public CharacterData CharacterData { get; set; }
    public DateTime creationTime { get; set; }

    public static SaveCharaterData GetRandomCharater()
    {
        SaveCharaterData newItem = new SaveCharaterData();
        newItem.CharacterData = DataTableManager.CharacterTable.GetRandom();
        return newItem;
    }
    public SaveCharaterData()
    {
        instanceId = Guid.NewGuid();
        creationTime = DateTime.Now;
    }
    public override string ToString()
    {
        return $"{instanceId}\n{creationTime}\n{CharacterData.Id}";
    }

}
