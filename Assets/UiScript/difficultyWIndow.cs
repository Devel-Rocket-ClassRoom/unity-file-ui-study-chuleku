using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;

public class DiffSave
{
    public string diffName;
    public int diffVersion;
}
public class difficultyWIndow : GenericWindow
{
    public Toggle[] toggles;

    public int selected;
    private string currentDiffName;
    private int currentDiff;

    private string FileName = "diffSaveFile.json";
    private string SaveDirectory => Path.Combine(Application.persistentDataPath, "diffSave");
    private string Filepath => Path.Combine(SaveDirectory, FileName);

    private void Awake()
    {
        toggles[0].onValueChanged.AddListener(OnEasy);
        toggles[1].onValueChanged.AddListener(OnNormal);
        toggles[2].onValueChanged.AddListener(OnHard);
    }
    public override void Open()
    {
        base.Open();
        if (!Directory.Exists(SaveDirectory))
        {
            Directory.CreateDirectory(SaveDirectory);
        }
        if (!File.Exists(Filepath))
        {
            selected = 0;
            currentDiff = 0;
            currentDiffName = "Easy";
            toggles[selected].isOn = true;
        }
        else
        {
            var json = File.ReadAllText(Filepath);
            DiffSave obj = JsonConvert.DeserializeObject<DiffSave>(json);
            selected = obj.diffVersion;
            toggles[selected].isOn = true;
            currentDiff = obj.diffVersion;
            currentDiffName = obj.diffName;
            Debug.Log($"불러오기 완료 현재 난이도: {currentDiffName}");
        }
    }
    public override void Close()
    {
        base.Close();
    }
    public void OnEasy(bool active)
    {
        if(active)
        {
            Debug.Log("OnEasy");
            currentDiff = 0;
            currentDiffName = "Easy";
        }
    }
    public void OnNormal(bool active)
    {
        if (active)
        {
            Debug.Log("OnNormal");
            currentDiff = 1;
            currentDiffName = "Normal";
        }
    }
    public void OnHard(bool active)
    {
        if (active)
        {
            Debug.Log("OnHard");
            currentDiff = 2;
            currentDiffName = "Hard";
        }
    }
    public void OnCancel()
    {
        windowManager.Open(0);
    }
    public void OnApply()
    {
        DiffSave difsave = new DiffSave()
        {
            diffName = currentDiffName,
            diffVersion = currentDiff
        };
        if (!Directory.Exists(SaveDirectory))
        {
            Directory.CreateDirectory(SaveDirectory);
        }
        string json = JsonConvert.SerializeObject(difsave);
        File.WriteAllText(Filepath, json);
        Debug.Log($"저장 완료 저장 난이도 : {currentDiffName}");
        windowManager.Open(0);
    }
}
