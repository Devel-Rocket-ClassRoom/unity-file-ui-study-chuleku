using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Uicharater : MonoBehaviour
{
    public static readonly string FormatCommon = "{0}:{1}";
    public static readonly string FormatEquipCommon = "{0}:{1}(+{2})";
    public Image imageIcon;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDesc;
    public TextMeshProUGUI textHealth;
    public TextMeshProUGUI textDamage;
    public TextMeshProUGUI textDefense;
    public UiCharaterPanel panel;

    private SaveCharaterData currentData;
    public void SetEmpty()
    {
        imageIcon.sprite = null;
        textName.text = string.Empty;
        textDesc.text = string.Empty;
        textHealth.text = string.Empty;
        textDamage.text = string.Empty;
        textDefense.text = string.Empty;
    }

    public void SetSaveItemData(SaveCharaterData data)
    {
        currentData = data;
        CharacterData charater =  data.CharacterData;   
        EquipCharater equipCharater = new EquipCharater(charater);
        imageIcon.sprite = charater.IconSprite;
        textName.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Name"), charater.StringName);
        textDesc.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Desc"), charater.StringDesc);
        textHealth.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Health"), charater.Health.ToString());
        textDamage.text = string.Format(FormatEquipCommon, DataTableManager.StringTable.Get("AttackDamage"),charater.AttackDamage.ToString(),charater.InFoAttackDamage.ToString());
        textDefense.text = string.Format(FormatEquipCommon, DataTableManager.StringTable.Get("Defense"), charater.Defense.ToString(),charater.InFoDefense.ToString());
    }
    public void OnClickItem()
    {
        panel.OnClickItem(currentData);
    }
}
