using Newtonsoft.Json;
using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class PlayerState
{
    public string playerName;
    public int lives;
    public float health;
    [JsonConverter(typeof(Vector3Converter))]
    public Vector3 position;

    public override string ToString()
    {
        return $"{playerName} / {lives} / {health}";
    }
}
public class JsonTest1 : MonoBehaviour
{
    private JsonSerializerSettings jsonSetting;
    private void Awake()
    {
        jsonSetting = new JsonSerializerSettings();
        jsonSetting.Formatting = Formatting.Indented;
        jsonSetting.Converters.Add(new Vector3Converter());
    }
    void Update()
     {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerState playerState = new PlayerState
            {
                playerName = "Player1",
                lives = 3,
                health = 100.0f,
                position = new Vector3(1f, 2f, 3f)
            };
            string pathFolder = Path.Combine(Application.persistentDataPath, "JsonTest1");
            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }
            string path = Path.Combine(pathFolder, "playerState.json");
            string json = JsonConvert.SerializeObject(playerState, Formatting.Indented,jsonSetting);
            File.WriteAllText(path, json);
            Debug.Log(json);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Load
            string path = Path.Combine(Application.persistentDataPath, "JsonTest1", "playerState.json");
            string json = File.ReadAllText(path);
            PlayerState obj = JsonConvert.DeserializeObject<PlayerState>(json,jsonSetting);
            Debug.Log(json);
            Debug.Log(obj.playerName);
            Debug.Log(obj.lives);
            Debug.Log(obj.health);
            Debug.Log(obj.position);
        }
    }
      
}
