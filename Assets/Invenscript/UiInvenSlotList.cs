using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using Unity.Android.Gradle.Manifest;
public class UiInvenSlotList : MonoBehaviour
{
   
    public enum SortingOptions
    {
        CreationTimeAsscding,
        CreationTimeDeccending,
        NameAccending,
        NameDeccending,
        CostAccending,
        CostDeccending,

    }
    public enum FilteringOptions
    {
        None,
        Weapon,
        Equip,
        Consumable,
        NoneConsumable,
    }
    public enum LanguageOptions
    {
        Korean,
        English,
        Japanese,
    }
   
    public readonly System.Comparison<SaveItemData>[] comparisons =
    {
        (lhs,rhs) =>lhs.creationTime.CompareTo(rhs.creationTime),
        (lhs,rhs) =>rhs.creationTime.CompareTo(lhs.creationTime),
        (lhs,rhs) =>lhs.itemData.StringName.CompareTo(rhs.itemData.StringName),
        (lhs,rhs) =>rhs.itemData.StringName.CompareTo(lhs.itemData.StringName),
        (lhs,rhs) =>lhs.itemData.Cost.CompareTo(rhs.itemData.Cost),
        (lhs,rhs) =>rhs.itemData.Cost.CompareTo(lhs.itemData.Cost),
    };

    public readonly System.Func<SaveItemData,bool>[] filterings =
    {
        (x) =>true,
        (x) =>x.itemData.Type == ItemType.Weapon,
        (x) =>x.itemData.Type == ItemType.Equip,
        (x) =>x.itemData.Type == ItemType.Consumable,
        (x) =>x.itemData.Type != ItemType.Consumable,
    };
    public readonly System.Func<Language, string>[] changeLanguages =
    {
        (data) => LanguageOptions.Korean.ToString(),
        (data) => LanguageOptions.English.ToString(),
        (data) => LanguageOptions.Japanese.ToString(),
    };
    public UiInvenSlot prefab;

    public ScrollRect scrollRect;
    private List<UiInvenSlot> uiSlotList = new List<UiInvenSlot>();


    private List<SaveItemData> saveItemDataList = new List<SaveItemData>();
    private SortingOptions sorting = SortingOptions.CreationTimeAsscding;
    private FilteringOptions filtering = FilteringOptions.None;
    private LanguageOptions language = LanguageOptions.Korean;

    public SortingOptions Sorting
    {
        get => sorting;
        set
        {
            if (sorting != value)
            {
                sorting = value;
                UpdateSlots();
            }
        }
    }
    public FilteringOptions Filtering
    {
        get => filtering;
        set
        {
            if (filtering != value)
            {
                filtering = value;
                UpdateSlots();
            }
        }
    }

    public LanguageOptions Language
    {
        get => language;
        set
        {
            if (language != value)
            {
                language = value;
                UpdateSlots();
            }
        }
    }
    
    private int selectedSlotIndex = -1;

    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveItemData> onSelectSlot;

    /*    private void OnSelectSlot(SaveItemData saveItemData)
        {
            Debug.Log(saveItemData);
        }*/
    /*    private void Start()
        {
            onSelectSlot.AddListener(OnSelectSlot);
        }

        private void OnEnable()
        {
            SetSaveItemData(SaveLoadManager.Data.itemids);
        }

        private void OnDisable()
        {
            SaveLoadManager.Data.itemids = saveItemDataList;
            SaveLoadManager.Save();
            saveItemDataList = null;
        }
    */



    public List<SaveItemData> GetSaveItemDataList()
    {
        return saveItemDataList;
    }
    public void SetSaveItemData(List<SaveItemData> source)
    {
        saveItemDataList = source.ToList();
        UpdateSlots();
    }
   
    private void UpdateSlots()
    {
        var list = saveItemDataList.Where(filterings[(int)filtering]).ToList();
        list.Sort(comparisons[(int)sorting]);

        if (uiSlotList.Count <list.Count)
        {
            for(int i = uiSlotList.Count; i < list.Count; i++)
            {
                var newSlot = Instantiate(prefab,scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    selectedSlotIndex = newSlot.slotIndex;
                    onSelectSlot?.Invoke(newSlot.saveItemData);
                });
                uiSlotList.Add(newSlot);
            }
        }
        for(int i = 0; i < uiSlotList.Count; i++)
        {
            if (i < list.Count)
            {
                uiSlotList[i].gameObject.SetActive(true);
                uiSlotList[i].SetItem(list[i]);
            }
            else
            {
                uiSlotList[i].gameObject.SetActive(false);
                uiSlotList[i].SetEmpty();
            }
        }
        selectedSlotIndex = -1;
        onUpdateSlots?.Invoke();
    }
    public void AddRandomItem()
    {
        saveItemDataList.Add(SaveItemData.GetRandomItem());
        UpdateSlots();
    }
    public void RemoveItem()
    {
        if(selectedSlotIndex == -1)
        {
            return;
        }

        saveItemDataList.Remove(uiSlotList[selectedSlotIndex].saveItemData);
        UpdateSlots();
    }
    public void UiInfo(UiItemInfo uiItemInfo)
    {
        if(selectedSlotIndex == -1)
        {
            return;
        }
        uiItemInfo.SetSaveItemData(uiSlotList[selectedSlotIndex].saveItemData);
    }
}
