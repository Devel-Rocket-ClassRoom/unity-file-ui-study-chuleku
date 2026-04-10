using UnityEngine;
using System.IO;

public class FileStreams : MonoBehaviour
{
    /*    ### 문제 1. 세이브 파일 관리자

    `Application.persistentDataPath` 아래의 세이브 폴더를 탐색하여 저장된 파일 정보를 출력하고, 특정 파일을 복사/삭제할 수 있는 스크립트를 작성하시오.

    ** 요구사항**

    1. **세이브 폴더 준비**: `SaveData` 폴더를 만들고, `File.WriteAllText`로 테스트용 파일 3개를 생성할 것
       - `save1.txt`, `save2.txt`, `save3.txt` (내용은 자유)
    2. **파일 목록 출력**: `Directory.GetFiles`로 폴더 내 모든 파일을 조회하고, 각 파일의 이름과 확장자를 출력할 것
    3. **파일 복사**: `save1.txt`를 `save1_backup.txt`로 복사할 것(`File.Copy`)
    4. **파일 삭제**: `save3.txt`를 삭제할 것(`File.Delete`)
    5. **최종 확인**: 작업 후 파일 목록을 다시 출력하여 결과를 확인할 것

    * *예상 출력**

    ```
    === 세이브 파일 목록 ===
    - save1.txt(.txt)
    - save2.txt(.txt)
    - save3.txt(.txt)
    save1.txt → save1_backup.txt 복사 완료
    save3.txt 삭제 완료
    === 작업 후 파일 목록 ===
    - save1.txt (.txt)
    - save1_backup.txt(.txt)
    - save2.txt(.txt)
    ```*/
    private string[] save = new string[3];
    private string[] path = new string[3];
    private string testpath;

    private void Start()
    {
        testpath = Path.Combine(Application.persistentDataPath, "SaveData");
        if (!Directory.Exists(testpath)) Directory.CreateDirectory(testpath);
        for (int i = 0; i < 3; i++)
        {
            save[i] = $"save{i + 1}.txt";
            path[i] = Path.Combine(testpath, save[i]);

            string masage = "내용은 자유";
            File.WriteAllText(path[i],masage);
        }
        Debug.Log("확인은R,복사E,삭제D,");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            for(int i = 0; i < save.Length; i++)
            {
                string[] loaded = Directory.GetFiles(testpath);
                foreach (string file in loaded)
                {
                    Debug.Log($"파일 이름: {Path.GetFileName(file)}");
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            string dest = Path.Combine(testpath, "save1_backup.txt");
            File.Copy(path[0], dest);
            Debug.Log("파일복사");
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            File.Delete(path[path.Length-1]);
            Debug.Log("파일 삭제");
        }
    }
}
