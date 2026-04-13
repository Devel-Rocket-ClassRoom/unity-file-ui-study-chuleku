using UnityEngine;
using UnityEngine.UI;
using CsvHelper;
using System.IO;
public class DataTableTest : MonoBehaviour
{
    public string NameStringTableKr = "StringTableKr";
    public string NameStringTableEn = "StringTableEn";
    public string NameStringTableJp = "StringTableJp";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Variables.Language = Language.Korean;
            Debug.Log("한국어");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Variables.Language = Language.English;
            Debug.Log("영어");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Variables.Language = Language.Japanese;
            Debug.Log("일본어");
        }
    }
    public void OnClickStringTableKr()
    {
        Debug.Log(DataTableManager.StringTable.Get("Hello"));
    }
    public void OnClickStringTableEn()
    {
        var table = new StringTable();
        table.Load(NameStringTableEn);
        Debug.Log(table.Get("Hello"));
    }
    public void OnClickStringTableJp()
    {
        var table = new StringTable();
        table.Load(NameStringTableJp);
        Debug.Log(table.Get("Hello"));
    }
}
