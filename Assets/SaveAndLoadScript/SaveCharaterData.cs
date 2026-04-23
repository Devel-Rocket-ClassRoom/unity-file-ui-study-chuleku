using Newtonsoft.Json;
using System;
using UnityEngine;

public class SaveCharaterData
{
    public Guid instanceId { get; set; }
    [JsonConverter(typeof(CharaterDataConverter))]
    public CharacterData CharacterData { get; set; }
    public DateTime creationTime { get; set; }
    // 장착된 장비 id (아이템 테이블의 Id)
    public string equippedWeaponId { get; set; }
    public string equippedEquipId { get; set; }

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

    [JsonIgnore]
    public bool IsModified => !string.IsNullOrEmpty(equippedWeaponId) || !string.IsNullOrEmpty(equippedEquipId);
    public override string ToString()
    {
        return $"{instanceId}\n{creationTime}\n{CharacterData.Id}";
    }

}
