using UnityEngine;
using UnityEngine.UI;

public class CheckCharacher : MonoBehaviour
{

    public LocalizationText localizationTextName;
    public LocalizationText localizationTextDesc;
    public LocalizationText localizationTextHealth;
    public LocalizationText localizationTextAttack;
    public LocalizationText localizationTextDefense;

    public Image icon;


    public void SetEmpty()
    {
        icon.sprite = null;
        localizationTextName.id = string.Empty;
        localizationTextDesc.id = string.Empty;
        localizationTextHealth.id = string.Empty;
        localizationTextAttack.id = string.Empty;
        localizationTextDefense.id = string.Empty;
        localizationTextName.OnChangeId();
        localizationTextDesc.OnChangeId();
        localizationTextHealth.OnChangeId();
        localizationTextAttack.OnChangeId();
        localizationTextDefense.OnChangeId();
    }


    public void SetCharacterData(string characterId)
    {
        CharacterData character = DataTableManager.CharacterTable.Get(characterId);
        if (character != null)
        {
            SetCharacterData(character);
        }
    }
    public void SetCharacterData(CharacterData character)
    {
        icon.sprite = character.IconSprite;
        localizationTextName.id = character.Name;
        localizationTextDesc.id = character.Desc;
        localizationTextHealth.id = character.Health;
        localizationTextAttack.id = character.AttackDamage;
        localizationTextDefense.id = character.Defense;
        localizationTextName.OnChangeId();
        localizationTextDesc.OnChangeId();
        localizationTextHealth.OnChangeId();
        localizationTextAttack.OnChangeId();
        localizationTextDefense.OnChangeId();
    }

}
