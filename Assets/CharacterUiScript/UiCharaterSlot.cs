using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UiCharaterSlot : MonoBehaviour
{
    public int slotIndex = -1;
    public Image imageIcon;
    public TextMeshProUGUI textName;
    public SaveCharaterData saveCharaterData { get; private set; }

    public Button button;
    public void SetEmpty()
    {
        imageIcon.sprite = null;
        textName.text = string.Empty;
        saveCharaterData = null;
    }

    public void SetItem(SaveCharaterData data)
    {
        saveCharaterData = data;
        imageIcon.sprite = saveCharaterData.CharacterData.IconSprite;
        textName.text = saveCharaterData.CharacterData.StringName;
    }

}
