using UnityEngine;
using UnityEngine.UI;

public class ItemButtonTable : MonoBehaviour
{
    public ItemTable ItemTable => DataTableManager.ItemTable;
    public string id;
    public string curruntname;
    public string desc;
    public LocalizationText nameLocalizationText;

    public Image iconImage;
    public CurrentItem currentItem;


    public void OnChangeItemId()
    {
        ItemData itemData = DataTableManager.ItemTable.Get(id);
        if (itemData != null)
        {
            iconImage.sprite = itemData.IconSprite;
            nameLocalizationText.id = id;
            nameLocalizationText.OnChangeId();
        }
    }

    public void OnClick()
    {
        currentItem.SetItemData(id);
    }
/*    public void OnClickButtonWeaponSword()
    { 
        GameObject currentItemObject = GameObject.Find("CurrentItem");
        CurrentItem currentItem = currentItemObject.GetComponent<CurrentItem>();
    }
    public void OnClickButtonWeaponBow()
    {
        GameObject currentItemObject = GameObject.Find("CurrentItem");
        CurrentItem currentItem = currentItemObject.GetComponent<CurrentItem>();

    }

    public void OnClickButtonEquip()
    {

        GameObject currentItemObject = GameObject.Find("CurrentItem");
        CurrentItem currentItem = currentItemObject.GetComponent<CurrentItem>();
    }

    public void OnClickButtonConsumable()
    {

        GameObject currentItemObject = GameObject.Find("CurrentItem");
        CurrentItem currentItem = currentItemObject.GetComponent<CurrentItem>();

    }*/

}
