using UnityEngine;

using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using Unity.Android.Gradle.Manifest;
using System;
public class UiCharaterList : MonoBehaviour
{

    public enum SortingOptions
    {
        CreationTimeAsscding,
        CreationTimeDeccending,
        NameAccending,
        NameDeccending,
        AttackDeccending,
        AttackAccending,
    }
    public enum FilteringOptions
    {
        None,
        UpHealth,
        DownHealth,
        UpDefense,
        DownDefense,
    }
    public enum LanguageOptions
    {
        Korean,
        English,
        Japanese,
    }

    public readonly System.Func<SaveCharaterData, bool>[] filterings =
    {
        (x) => true,
        (x) => x.CharacterData.Health > 150,
        (x) => x.CharacterData.Health <= 150,
        (x) =>x.CharacterData.Defense > 5,
        (x) =>x.CharacterData.Defense <= 5,
    };

    public readonly System.Comparison<SaveCharaterData>[] comparisons =
    {
        (lhs,rhs) =>lhs.creationTime.CompareTo(rhs.creationTime),
        (lhs,rhs) =>rhs.creationTime.CompareTo(lhs.creationTime),
        (lhs,rhs) =>lhs.CharacterData.StringName.CompareTo(rhs.CharacterData.StringName),
        (lhs,rhs) =>rhs.CharacterData.StringName.CompareTo(lhs.CharacterData.StringName),
        (lhs,rhs) =>rhs.CharacterData.AttackDamage.CompareTo(lhs.CharacterData.AttackDamage),
        (lhs,rhs) =>lhs.CharacterData.AttackDamage.CompareTo(rhs.CharacterData.AttackDamage),
    };
    public readonly System.Func<Language, string>[] changeLanguages =
    {
        (data) => LanguageOptions.Korean.ToString(),
        (data) => LanguageOptions.English.ToString(),
        (data) => LanguageOptions.Japanese.ToString(),
    };
    public UiCharaterSlot prefab;

    public ScrollRect scrollRect;
    private List<UiCharaterSlot> uiSlotList = new List<UiCharaterSlot>();

    private List<SaveCharaterData> saveCharaterDataList = new List<SaveCharaterData>();
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
    public UnityEvent<SaveCharaterData> onSelectSlot;




    public List<SaveCharaterData> GetSaveCharaterDataList()
    {
        return saveCharaterDataList;
    }
    public void SetSaveItemData(List<SaveCharaterData> source)
    {
        var allChars = DataTableManager.CharacterTable.GetAll();
        saveCharaterDataList = new List<SaveCharaterData>();
        foreach (var ch in allChars)
        {
            var found = source?.FirstOrDefault(s => s.CharacterData != null && s.CharacterData.Id == ch.Id);
            if (found != null)
            {
                saveCharaterDataList.Add(found);
            }
            else
            {
                var sc = new SaveCharaterData();
                sc.CharacterData = ch;
                saveCharaterDataList.Add(sc);
            }
        }
        UpdateSlots();
    }

    private void UpdateSlots()
    {
        var list = saveCharaterDataList.Where(filterings[(int)filtering]).ToList();
        list.Sort(comparisons[(int)sorting]);

        if (uiSlotList.Count < list.Count)
        {
            for (int i = uiSlotList.Count; i < list.Count; i++)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    selectedSlotIndex = newSlot.slotIndex;
                    onSelectSlot?.Invoke(newSlot.saveCharaterData);
                });
                uiSlotList.Add(newSlot);
            }
        }
        for (int i = 0; i < uiSlotList.Count; i++)
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
        saveCharaterDataList.Add(SaveCharaterData.GetRandomCharater());
        UpdateSlots();
    }
    public void RemoveItem()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }

        saveCharaterDataList.Remove(uiSlotList[selectedSlotIndex].saveCharaterData);
        UpdateSlots();
    }
}

