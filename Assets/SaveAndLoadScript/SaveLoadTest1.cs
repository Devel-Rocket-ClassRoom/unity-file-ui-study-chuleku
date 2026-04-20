using Unity.VisualScripting;
using UnityEngine;
using SaveDataVC = SaveDataV4;

public class SaveLoadTest1 : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveLoadManager.Data = new SaveDataVC();
            SaveItemData sd = new SaveItemData();
            sd.itemData = DataTableManager.ItemTable.Get("Item1");
            SaveLoadManager.Data.Name = "Test1234";
            SaveLoadManager.Data.Gold = 4351;
            SaveLoadManager.Data.itemids.Add(sd);
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
                    Debug.Log(itemid.instanceId);
                    Debug.Log(itemid.itemData.Name);
                    Debug.Log(itemid.itemData.Id);
                }
            }
            else
            {
                Debug.Log("세이브 파일 없음");
            }
        }   
    }
    
}
