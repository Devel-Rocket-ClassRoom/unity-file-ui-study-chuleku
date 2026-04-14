using System;
using System.Collections.Generic;
using TMPro;
using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;
[ExecuteInEditMode]

public class LocalizationText : MonoBehaviour
{
    [ExecuteInEditMode]
#if UNITY_EDITOR
    public Language editlanguage;
#endif
    public string id;
    public TextMeshProUGUI text;



    private void OnEnable()
    {
        Variables.OnLanguageChanged += OnChangeLanguage;
        OnChangeLanguage(editlanguage);  
    }
    private void OnDisable()
    {

            Variables.OnLanguageChanged -= OnChangeLanguage;
            
    }
    private void OnValidate()
    {
#if UNITY_EDITOR
        OnChangeLanguage(editlanguage);
#else
        OnChangeLanguage();
#endif
        
    }

    public void OnChangeId()
    {
        text.text = DataTableManager.StringTable.Get(id);
    }

    public void OnChangeLanguage()
    {
        text.text = DataTableManager.StringTable.Get(id);
    }

    public void OnChangeLanguage(Language language)
    {
        var stringTable = DataTableManager.GetStringTable(language);
        text.text = stringTable.Get(id);
    }
#if UNITY_EDITOR
    [ContextMenu("ChangeLanguage")]
    private void ChangeLanguage()
    {
        LocalizationText[] allTexts = GameObject.FindObjectsOfType<LocalizationText>();
        foreach (LocalizationText text in allTexts)
        {
            text.editlanguage = Language.Korean;
            text.OnChangeLanguage(text.editlanguage);
        }
    }
#endif

    /// 기존에 했던 방식
    /*    [ExecuteInEditMode]
        public StringTable textTable = new StringTable();
        public TextMeshProUGUI text;
        public int id;
        public string key;
        public static readonly string[] stringTableIds = new string[]
    {
            "StringTableKr",
            "StringTableEn",
            "StringTableJp"
    };

        private void Start()
        {
            Debug.Log("1번: 한국어, 2번: 영어, 3번: 일본어");
            Debug.Log("Q:Hello, W:Bye, E:You Die");
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                id = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                id = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                id = 2;
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                key = "Hello";
            }
            else if(Input.GetKeyDown(KeyCode.W))
            {
                key = "Bye";
            }
            else if(Input.GetKeyDown(KeyCode.E))
            {
                key = "You Die";
            }
            OnChangeId();
        }
        private void OnChangeId()
        {
            OnlanguageChange();
            text.text = textTable.Get(key);

        }
        private void OnlanguageChange()
        {
            if(id>=3||id<0)
            {
                Debug.Log("테이블 없음");
                return;
            }
            textTable.Load(stringTableIds[id]);
        }*/
}
