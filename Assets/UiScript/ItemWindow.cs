using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ItemWindow : GenericWindow
{
    public static readonly string FormatCommon = "{0}:{1}";
    public Uicharater charaterInfo;
    public UiItemInfo itemInfo;
    public Image weaponImageSprite;
    public Image EquipImageSprite;
    private SaveCharaterData saveCharaterData;
    private SaveItemData saveItemData;
    private bool equipCheck;
    private bool weaponCheck;
    private CharacterData characterData;
    public UnityEvent onUpdateSlots;
    private EquipCharater equipCharater;

    public override void Open(SaveCharaterData data)
    {
        base.Open();
        saveCharaterData = data;
        characterData = data.CharacterData;
        charaterInfo.SetSaveItemData(data);
        equipCharater = new EquipCharater(characterData);
        itemInfo.SetEmpty();
        if(data.equippedEquipId != null)
        {
            EquipImageSprite.sprite = DataTableManager.ItemTable.Get(data.equippedEquipId).IconSprite;
        }
        else
        {
            EquipImageSprite.sprite = null;
        }
        if (data.equippedWeaponId != null)
        {
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
        saveItemData = data;
        
        if (data == null || saveCharaterData == null)
            return;
        if (data.itemData.Type == ItemType.Equip)
        {
            equipCharater.newDefense = data.itemData.Value;
            saveCharaterData.equippedEquipId = data.itemData.Id;
            charaterInfo.textDefense.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Defense"),equipCharater.DefenseInfo());
            EquipImageSprite.sprite = data.itemData.IconSprite;
        }
        else if (data.itemData.Type == ItemType.Weapon)
        {
            equipCharater.newAttack = data.itemData.Value;
            saveCharaterData.equippedWeaponId = data.itemData.Id;
            charaterInfo.textDamage.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("AttackDamage"),equipCharater.AttackInfo());
            weaponImageSprite.sprite = data.itemData.IconSprite;
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
            saveCharaterData.equippedEquipId = null;
            equipCharater.newDefense = 0;
            EquipImageSprite.sprite = null;
            charaterInfo.textDefense.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Defense"), equipCharater.AttackInfo());
        }
        if (weaponCheck)
        {
            weaponCheck = false;
            saveCharaterData.equippedWeaponId=null;
            equipCharater.newAttack =0;
            charaterInfo.textDamage.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("AttackDamage"), equipCharater.DefenseInfo());
            weaponImageSprite.sprite = null;
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
        saveCharaterData.CharacterData.AttackDamage = equipCharater.AttackInfo();
        saveCharaterData.CharacterData.Defense = equipCharater.DefenseInfo();
        windowManager.Open(0,saveCharaterData);
    }
    public void OnSave()
    {

    }
    public void OnLoad()
    {

    }

}
