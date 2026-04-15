using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class PlayerInfo
{
    public string playerName;
    public int lives;
    public float health;
    public Vector3 position;
    public Dictionary<string, int> scores = new Dictionary<string, int>
    {
        {"Stage1",100 },
        {"Stage2",200 }
    };
}

public class JsonUtilityTest : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //save
            PlayerInfo obj = new PlayerInfo
            {
                playerName = "ABC",
                lives = 10,
                health = 10.999f,
                position = new Vector3(1f, 2f, 3f)
            };
            string pathFolder = Path.Combine(Application.persistentDataPath,"JsonTest");
            
            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }
            string path = Path.Combine(pathFolder, "player.Json");
            string json = JsonUtility.ToJson(obj, prettyPrint: true);
            Debug.Log(path);
            Debug.Log(json);
            File.WriteAllText(path, json);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Load
            string path = Path.Combine(Application.persistentDataPath,"JsonTest","player.Json");
            string json = File.ReadAllText(path);
            /*PlayerInfo obj = JsonUtility.FromJson<PlayerInfo>(json);*/
            PlayerInfo obj = new PlayerInfo();
            JsonUtility.FromJsonOverwrite(json, obj);
            Debug.Log(json);
            Debug.Log(obj.playerName);
            Debug.Log(obj.lives);
            Debug.Log(obj.health);
            Debug.Log(obj.position);
        }
    }
}
