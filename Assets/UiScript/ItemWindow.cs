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

    public UnityEvent onUpdateSlots;

    public override void Open(SaveCharaterData data)
    {
        base.Open();
        saveCharaterData = data;
        charaterInfo.SetSaveItemData(data);
        itemInfo.SetEmpty();
        weaponCheck = false;
        equipCheck = false;
    }
    public override void Close()
    {
        base.Close();
    }

    public void Equip(SaveItemData data)
    {
        weaponCheck = false;
        equipCheck = false;
        if (data.itemData.Type == ItemType.Equip)
        {
            charaterInfo.textDefense.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Defense"), saveCharaterData.CharacterData.Defense.ToString());
            charaterInfo.textDefense.text = $"{charaterInfo.textDefense.text}(+{data.itemData.Value})";
            EquipImageSprite.sprite = data.itemData.IconSprite;
        }
        else if(data.itemData.Type == ItemType.Weapon)
        {
            charaterInfo.textDamage.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("AttackDamage"),saveCharaterData.CharacterData.AttackDamage.ToString());
            charaterInfo.textDamage.text = $"{charaterInfo.textDamage.text}(+{data.itemData.Value})";
            weaponImageSprite.sprite = data.itemData.IconSprite;
        }
        if(data.itemData.Type == ItemType.Consumable)
        {
            charaterInfo.textHealth.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Health"), saveCharaterData.CharacterData.Health.ToString());
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
            EquipImageSprite.sprite = null;
        }
        if (weaponCheck)
        {
            weaponCheck = false;
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
        windowManager.Open(0);
    }
    public void OnSave()
    {

    }
    public void OnLoad()
    {

    }

}
