using CsvHelper.Configuration.Attributes;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum id
{
    Hello,
    Bye,
    [CsvHelper.Configuration.Attributes.Name("You Die")]
    You_Die
};
public class StringTable : DataTable
{
    public class Data
    {
        public static System.Action OnIdChangedEvent;
        [Name("Id")]
        public id Id { get; set; }
        [Ignore]
        public string Name
        {
            get
            {
                OnIdChangedEvent?.Invoke();
                if (Id == id.You_Die)return "You Die";
                return Id.ToString();
            }
        }
        public string String { get; set; }
    }
    private readonly Dictionary<id, string> table = new Dictionary<id, string>();
    public override void Load(string filename)
    {
        table.Clear();

        string path = string.Format(FormatPath, filename);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCsv<Data>(textAsset.text);
        foreach (Data data in list)
        {
            if(!table.ContainsKey(data.Id))
            {
                table.Add(data.Id, data.String);
            }
            else
            {
                Debug.LogError($"키 중복 : {data.Id}");
            }
        }

    }
    public static readonly string Unknown = "키 없음";
    public string Get(string key)
    {
        string fixkey = key.Replace(" ", "_");
        System.Enum.TryParse(fixkey, out id idKey);
        {
            if (!table.ContainsKey(idKey))
            {
                return Unknown;
            }
            else
            {
                return table[idKey];
            }
        }
    }
}
