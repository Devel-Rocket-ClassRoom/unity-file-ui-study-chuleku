using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Playables;
using Random = UnityEngine.Random;
[System.Serializable]
public class SomeClass
{
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;

    public override string ToString()
    {
        return $"{pos}/{rot}/{scale}/{color}";
    }
}

public class ObjectSaveData
{
    public string prefabName;
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;
}


public class SomeTest : MonoBehaviour
{
    public string fileName = "test.josn";

    public string FileFullPath => Path.Combine(Application.persistentDataPath, "JsonTest", fileName);

    private JsonSerializerSettings jsonSetting;
/*    public GameObject Cube;
    public GameObject Cylinder;
    public GameObject Sqhere;
    public GameObject Capsule;*/
    public string[] prefabNames =
    {
        "Cube",
        "Sphere",
        "Capsule",
        "Cylinder"
    };
    private List<GameObject> list = new List<GameObject>();
    private void Awake()
    {
        jsonSetting = new JsonSerializerSettings();
        jsonSetting.Formatting = Formatting.Indented;
        jsonSetting.Converters.Add(new Vector3Converter());
        jsonSetting.Converters.Add(new QuaternionConvert());
        jsonSetting.Converters.Add(new ColorConvert());

    }
/*    public void Save()
    {
        List<SomeClass> somedata = new List<SomeClass>();
        foreach (var item in list)
        {
            SomeClass some = new SomeClass
            {
                pos = item.transform.localPosition,
                rot = item.transform.localRotation,
                scale = item.transform.localScale,
                color = item.GetComponent<Renderer>().material.color
            };
            somedata.Add(some);
        }
        string json = JsonConvert.SerializeObject(somedata, Formatting.Indented, jsonSetting);
        File.WriteAllText(FileFullPath, json);
    }
    public void Load()
    {
        string json = File.ReadAllText(FileFullPath);
        List<SomeClass> loadSome = JsonConvert.DeserializeObject<List<SomeClass>>(json, jsonSetting);
        for (int i = list.Count-1; i >= 0; i--)
        {
            if(i>=loadSome.Count)
            {
                Delete(list[i]);
                list.RemoveAt(i);
            }
            else
            {
                list[i].transform.position = loadSome[i].pos;
                list[i].transform.rotation = loadSome[i].rot;
                list[i].transform.localScale = loadSome[i].scale;
                list[i].GetComponent<Renderer>().material.color = loadSome[i].color;
            }
        }
    }
*/
    private void CreateRandomObject()
    {
        var prefabName = prefabNames[Random.Range(0, prefabNames.Length)];
        var prefab = Resources.Load<JsonTestObject>(prefabName);
        var obj = Instantiate(prefab);
        obj.transform.position = Random.insideUnitSphere * 10f;
        obj.transform.rotation = Random.rotation;
        obj.transform.localScale = Vector3.one * Random.Range(0.5f, 3f);
        obj.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 0.6f, 1f, 0.7f, 1f);
    }
    public void OnNewCreate()
    {
        for(int i = 0;i<10;i++)
        {
            CreateRandomObject();
        }
    }
    public void NewOnclickDelete()
    {
        var objs = GameObject.FindGameObjectsWithTag("TestObject");
        foreach(var obj in objs)
        {
            Destroy(obj);
        }
    
    }

    public void OnSave()
    {
        var saveList = new List<ObjectSaveData>();
        var objs = GameObject.FindGameObjectsWithTag("TestObject");
        foreach (var obj in objs)
        {
            var jsonTestObj = obj.GetComponent<JsonTestObject>();
            saveList.Add(jsonTestObj.GetSaveData());
        }
        var json = JsonConvert.SerializeObject(saveList,jsonSetting);
        File.WriteAllText(FileFullPath, json);
    }

    public void OnLoad()
    {
        NewOnclickDelete();
        var json = File.ReadAllText(FileFullPath);
        var saveList = JsonConvert.DeserializeObject<List<ObjectSaveData>>(json,jsonSetting);
        foreach (var obj in saveList)
        {
            var prefab = Resources.Load<JsonTestObject>(obj.prefabName);
            var jsonTestobj = Instantiate(prefab);
            jsonTestobj.Set(obj);
        }
   
    }

  /*  public void Create()
    {
        float posx = Random.Range(-10, 10);
        float posy = Random.Range(-5, 6);
        float posz = Random.Range(-10, 10);
        Quaternion ranq = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        float ranc = Random.value;
        Vector3 posv = new Vector3(posx, posy, posz);
        if (ranc < 0.25f)
        {
            GameObject newobj = Instantiate(Cube, posv, ranq);
            newobj.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 0.6f, 1f, 0.7f, 1f);
            list.Add(newobj);
        }
        else if (ranc < 0.5f && ranc > 0.25f)
        {
            GameObject newobj = Instantiate(Cylinder, posv, ranq);
            newobj.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 0.6f, 1f, 0.7f, 1f);
            list.Add(newobj);
        }
        else if (ranc < 0.75f && ranc > 0.5f)
        {
            GameObject newobj = Instantiate(Sqhere, posv, ranq);
            newobj.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 0.6f, 1f, 0.7f, 1f);
            list.Add(newobj);
        }
        else
        {
            GameObject newobj = Instantiate(Capsule, posv, ranq);
            newobj.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 0.6f, 1f, 0.7f, 1f);
            list.Add(newobj);
        }
    }
    public void Delete(GameObject obj)
    {
        GameObject.Destroy(obj);
    }

    public void OnClickDelete()
    {
        foreach (var obj in list)
        {
            Delete(obj);
        }
        list.Clear();
    }

    public void OnClickCreate()
    {
        for (int i = 0; i < 10; i++)
        {
            Create();
        }
    }*/
}
