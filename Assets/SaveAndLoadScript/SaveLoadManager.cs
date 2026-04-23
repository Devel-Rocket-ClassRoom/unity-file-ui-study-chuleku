using UnityEngine;
using SaveDataVC = SaveDataV4;
using Newtonsoft.Json;
using System.Resources;
using System.IO;
using Mono.Cecil.Cil;
public static class SaveLoadManager
{
    public enum SaveMode
    {
        Text,   // Json 텍스트일때는(개발용) .json 으로
        Encrypted  // aes 암호화 (배포용) .dat으로
    }
    public static SaveMode Mode { get; set; } = SaveMode.Text;

    private static readonly string SaveDirectory = $"{Application.persistentDataPath}/save";

    private static readonly string[] SaveFileNames =
    {
        "SaveAuto",
        "Save1",
        "Save2",
        "Save3"
    };


    private static string GetSaveFilePath(int slot)
    {
        return GetSaveFilePath(slot, Mode);
    }
    public static string GetSaveFilePath(int slot, SaveMode mode)
    {
        string ext = mode == SaveMode.Text ? ".json" : ".dat";
        return Path.Combine(SaveDirectory, $"{SaveFileNames[slot]}{ext}");
    }
    public static int SaveDataVersion { get; } = 4;
    private static byte[] encrypted;
    public static SaveDataVC Data { get; set; } = new SaveDataVC();

    static SaveLoadManager()
    {
        if(!Load())
        {
            Debug.LogError("세이브 파일 로드 실패!");
        }
    }

    private static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All,
    };

    public static bool Save(int slot = 0)
    {
        return Save(slot, Mode);
    }
    public static bool Save(int slot,SaveMode mode)
    {
        if (Data == null || slot < 0 || slot >= SaveFileNames.Length)
        {
            return false;
        }
        try
        {
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }
            
            var json = JsonConvert.SerializeObject(Data, settings);
            string path = GetSaveFilePath(slot,mode);
     
               switch(Mode)
            {
                case SaveMode.Text:
                    File.WriteAllText(path, json);
                    Debug.Log("저장됨");

                    break;
                case SaveMode.Encrypted:
                    File.WriteAllBytes(path, CryptoUtil.Encrypt(json));
                    Debug.Log("저장됨");
                    break;
            }
            return true;
        }
        catch
        {
            Debug.LogError("Save 예외");
            return false;
        }
    }


   
    public static bool Load(int slot =0)
    {
        return Load(slot, Mode);
    }
    public static bool Load(int slot, SaveMode mode)
    {

        if (slot < 0 || slot >= SaveFileNames.Length)
        {
            return false;
        }
        string path = GetSaveFilePath(slot, mode);
    /*    string path = Path.Combine(SaveDirectory, SaveFileNames[slot]);*/
        if (!File.Exists(path))
        {
            return Save();
        }
        try
        {
            var json = string.Empty;
            switch(mode)
            {
                case SaveMode.Text:
                     json = File.ReadAllText(path);
                    break;

                case SaveMode.Encrypted:
                    json = CryptoUtil.Decrypt(File.ReadAllBytes(path));
                    break;
            }

            var saveData = JsonConvert.DeserializeObject<SaveData>(json, settings);
            while (saveData.Version < SaveDataVersion)
            {
                Debug.Log(saveData.Version);
                saveData = saveData.VersionUp();
                Debug.Log(saveData.Version);
            }
            Data = saveData as SaveDataVC;
            return true;
            
        }
        catch
        {
            Debug.LogError("Load 예외");
            return false;
        }

    }


}
