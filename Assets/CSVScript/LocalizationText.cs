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
    public Language language;
    public id Id;
#endif
    private bool changechack;
    public string idkey
    {
        get { return Id.ToString().Replace("_", " "); }
    }
    public TextMeshProUGUI text;



    private void OnEnable()
    {

            Variables.OnLanguageChanged += OnChangeLanguage;
            OnChangeLanguage();
       
#if UNITY_EDITOR

            OnChangeLanguage(language);

#endif
    }
    private void OnDisable()
    {

            Variables.OnLanguageChanged -= OnChangeLanguage;
            
    }
    private void OnValidate()
    {
#if UNITY_EDITOR
        OnChangeLanguage(language);
#else
        OnChangeLanguage();
#endif
        
    }

    private void OnChangeLanguage()
    {
        text.text = DataTableManager.StringTable.Get(idkey);
    }
#if UNITY_EDITOR
    private void OnChangeLanguage(Language language)
    {
        var stringTable = DataTableManager.GetStringTable(language);
        text.text = stringTable.Get(idkey);
        if(changechack)
        {
           NewId();
        }
    }
#endif
#if UNITY_EDITOR
    private Dictionary<LocalizationText, (Language lang, id id)> cache
    = new Dictionary<LocalizationText, (Language, id)>();
    [ContextMenu("ChangeLanguage")]
    private void ChangeLanguage()
    {

        Variables.Language = Language.Korean;
        LocalizationText[] allTexts = GameObject.FindObjectsOfType<LocalizationText>();
        cache.Clear();
        foreach (LocalizationText text in allTexts)
        {
            cache[text] = (text.language, text.Id);

            text.Id = id.Hello;
            text.OnChangeLanguage(Variables.Language);
        }
        changechack = false;

    }
    
#endif
    private void NewId()
    {
        var stringTable = DataTableManager.GetStringTable(language);
        text.text = stringTable.Get(idkey);
        
    }
    [ContextMenu("Refresh")]
    private void Refresh()
    {
        foreach (var item in cache)
        {
            item.Key.language = item.Value.lang;
            item.Key.Id = item.Value.id;
            item.Key.OnChangeLanguage(item.Value.lang);
        }
        changechack = true;
    }


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
