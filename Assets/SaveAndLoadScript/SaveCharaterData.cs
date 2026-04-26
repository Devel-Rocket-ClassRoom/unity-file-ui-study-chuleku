using Newtonsoft.Json;
using System;
using UnityEngine;

public class SaveCharaterData
{
    public Guid instanceId { get; set; }
    public CharacterData CharacterData { get; set; }
    public DateTime creationTime { get; set; }
    public string equippedWeaponId { get; set; }
    public string equippedEquipId { get; set; }
    public int equipWeapondamage { get; set; }
    public int equipDefense { get; set; }
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
        equippedWeaponId = null;
        equippedEquipId = null;
   
    }
    public SaveCharaterData(CharacterData characterData)
    {
        instanceId = Guid.NewGuid();
        creationTime = DateTime.Now;
        equippedWeaponId = null;
        equippedEquipId = null;
        equipDefense = characterData.Defense;
        equipWeapondamage = characterData.AttackDamage;
    }
    public override string ToString()
    {
        return $"{instanceId}\n{creationTime}\n{CharacterData.Id}";
    }

}
