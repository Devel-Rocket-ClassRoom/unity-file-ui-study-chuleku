using UnityEngine;
using SaveDataVC = SaveDataV3;

public class SaveLoadTest1 : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveLoadManager.Data = new SaveDataVC();
            SaveLoadManager.Data.Name = "Test1234";
            SaveLoadManager.Data.Gold = 4351;
            SaveLoadManager.Data.itemids.Add("Item1");
            SaveLoadManager.Data.itemids.Add("Item2");
            SaveLoadManager.Data.itemids.Add("Item3");
            SaveLoadManager.Data.itemids.Add("Item4");
            SaveLoadManager.Save();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (SaveLoadManager.Load())
            {
                Debug.Log(SaveLoadManager.Data.Name);
                Debug.Log(SaveLoadManager.Data.Gold);
                foreach(var itemid in SaveLoadManager.Data.itemids)
                {
                    Debug.Log(DataTableManager.ItemTable.Get(itemid));
                }
            }
            else
            {
                Debug.Log("세이브 파일 없음");
            }
        }   
    }
    
}
