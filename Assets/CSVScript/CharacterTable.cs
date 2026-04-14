using System.Collections.Generic;
using UnityEngine;
//1.Csv 파일(ID/이르름/설명/공격력../초상화or아이콘)
//2.DataTable 상속
//3.DataTableManager 등록
//4.테스트 패널

public class CharacterData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public string Health { get; set; }
    public string AttackDamage { get; set; }
    public string Defense { get; set; }
    public string Icon { get; set; }
    public string StringName => DataTableManager.StringTable.Get("Name");
    public string StringDesc => DataTableManager.StringTable.Get("Desc");
    public string StringHealth => DataTableManager.StringTable.Get("Health");
    public string StringAttack => DataTableManager.StringTable.Get("AttackDamage");
    public string StringDefense => DataTableManager.StringTable.Get("Defense");
    public Sprite IconSprite => Resources.Load<Sprite>($"Icon/{Icon}");

    public override string ToString()
    {
        return $"{Id} / {Name} / {Desc} / {Health} / {AttackDamage} / {Defense} / {Icon}";
    }
}
public class CharacterTable : DataTable
{
    private readonly Dictionary<string, CharacterData> table = new Dictionary<string, CharacterData>();
    public override void Load(string filename)
    {
        table.Clear();

        string path = string.Format(FormatPath, filename);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        List<CharacterData> list = LoadCsv<CharacterData>(textAsset.text);
        foreach (var item in list)
        {
            if (!table.ContainsKey(item.Id))
            {
                table[item.Id] = item;
            }
            else
            {
                Debug.LogError($"캐릭터 아이디 중복");
            }
        }
    }

    public CharacterData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            Debug.LogError($"캐릭터 아이디 {id} 없음");
            return null;
        }
        else
        {
            return table[id];
        }
    }

}
