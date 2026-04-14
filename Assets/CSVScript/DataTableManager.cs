using System.Collections.Generic;
using UnityEngine;

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> tables = new Dictionary<string, DataTable>();

    public static StringTable StringTable => Get<StringTable>(DataTableIds.String);

    public static ItemTable ItemTable => Get<ItemTable>(DataTableIds.Item);

    public static CharacterTable CharacterTable => Get<CharacterTable>(DataTableIds.Character);

#if UNITY_EDITOR
    public static StringTable GetStringTable(Language language)
    {
        return Get<StringTable>(DataTableIds.stringTableIds[(int)language]);
     }
#endif
    static DataTableManager()
    {
        Init();
    }
    private static void Init()
    {
#if !UNITY_EDITOR
        var stringTable = new StringTable();
        stringTable.Load(DataTableIds.String);
        tables.Add(DataTableIds.String, stringTable);
     
    
#else
        foreach (var id in DataTableIds.stringTableIds)
        {
            var stringTable = new StringTable();
            stringTable.Load(id);
            tables.Add(id, stringTable);
        }

#endif
        var itemTable = new ItemTable();
        itemTable.Load(DataTableIds.Item);
        tables.Add(DataTableIds.Item, itemTable);

        var characterTable = new CharacterTable();
        characterTable.Load(DataTableIds.Character);
        tables.Add(DataTableIds.Character, characterTable);
    }
    public static void ChangeLanguage(Language language)
    {
        var stringTable = StringTable;
        stringTable.Load(DataTableIds.stringTableIds[(int)language]);
    }
    public static T Get<T>(string id) where T : DataTable
    {
        if(!tables.ContainsKey(id))
        {
            Debug.Log("테이블 없음");
            return null;
 
        }
        return tables[id] as T;
    }
}
