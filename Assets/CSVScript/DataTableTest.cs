using UnityEngine;
using UnityEngine.UI;
using CsvHelper;
using System.IO;
public class DataTableTest : MonoBehaviour
{
    public string NameStringTableKr = "StringTableKr";
    public string NameStringTableEn = "StringTableEn";
    public string NameStringTableJp = "StringTableJp";

    public void OnClickStringTableKr()
    {
        var table = new StringTable();
        table.Load(NameStringTableKr);
        Debug.Log(table.Get("Hello"));
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
