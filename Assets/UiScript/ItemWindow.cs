using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ItemWindow : GenericWindow
{
    public Uicharater charaterInfo;
    public UiItemInfo itemInfo;
    public Image weaponImageSprite;
    public Image EquipImageSprite;
    private SaveCharaterData saveCharaterData;
    private bool equipCheck;
    private bool weaponCheck;
    public UnityEvent onUpdateSlots;


    public override void Open(SaveCharaterData data)
    {
        base.Open();
        saveCharaterData = data;
 
        charaterInfo.SetSaveItemData(data);

        itemInfo.SetEmpty();
        if(!string.IsNullOrEmpty(data.equippedEquipId)&& DataTableManager.ItemTable.Get(data.equippedEquipId).IconSprite)
        {
            EquipImageSprite.sprite = DataTableManager.ItemTable.Get(data.equippedEquipId).IconSprite;
        }
        else
        {
            EquipImageSprite.sprite = null;
        }
        if (!string.IsNullOrEmpty(data.equippedWeaponId)&&DataTableManager.ItemTable.Get(data.equippedWeaponId).IconSprite)
        {
            /*data.equippedWeaponId != string.Empty*/
            weaponImageSprite.sprite = DataTableManager.ItemTable.Get(data.equippedWeaponId).IconSprite;
        }
        else
        {
            weaponImageSprite.sprite = null;
        }
        weaponCheck = false;
        equipCheck = false;
    }
    public override void Close()
    {
        base.Close();
       
    }

    public void Equip(SaveItemData data)
    {
        
        if (data == null || saveCharaterData == null)
            return;
        if (data.itemData.Type == ItemType.Equip)
        {
            saveCharaterData.CharacterData.InFoDefense = data.itemData.Value;
            saveCharaterData.equippedEquipId = data.itemData.Id;
            EquipImageSprite.sprite = data.itemData.IconSprite;
            charaterInfo.SetSaveItemData(saveCharaterData);
        }
        else if (data.itemData.Type == ItemType.Weapon)
        {
            saveCharaterData.CharacterData.InFoAttackDamage = data.itemData.Value;
            saveCharaterData.equippedWeaponId = data.itemData.Id;
            weaponImageSprite.sprite = data.itemData.IconSprite;
            charaterInfo.SetSaveItemData(saveCharaterData);
        }
    }
    public void OnClickEquip()
    {
        equipCheck = true;
        weaponCheck = false;
    }
    public void OnClickOutEquip()
    {
        if(equipCheck)
        {
            equipCheck = false;
            saveCharaterData.equippedEquipId = string.Empty;
            saveCharaterData.CharacterData.InFoDefense = 0;
            EquipImageSprite.sprite = null;
            charaterInfo.SetSaveItemData(saveCharaterData);
        }
        if (weaponCheck)
        {
            weaponCheck = false;
            saveCharaterData.equippedWeaponId= string.Empty;
            saveCharaterData.CharacterData.InFoAttackDamage = 0;
            weaponImageSprite.sprite = null;
            charaterInfo.SetSaveItemData(saveCharaterData);
        }
    }
    public void OnClickWeapon()
    {
        equipCheck = false;
        weaponCheck = true;
    }
    public void OnClickBack()
    {
        weaponCheck = false;
        equipCheck = false;
        windowManager.Open(0,saveCharaterData);
    }
}
