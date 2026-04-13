using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.LightTransport;

public class NewMonoBehaviourScript : MonoBehaviour
{
    /*    	### 문제 2. FileStream으로 간이 파일 암호화

    `FileStream`의 `Position`과 바이트 조작을 활용하여 텍스트 파일을 간단히 암호화/복호화하는 스크립트를 작성하시오.

    ** 요구사항**

    1. **원본 파일 생성**: `File.WriteAllText`로 `secret.txt`에 영문 메시지를 저장할 것(예: `"Hello Unity World"`)
    2. **암호화**: `FileStream`으로 `secret.txt`를 열어 모든 바이트를 한 바이트씩 읽고, 각 바이트를 특정 키 값(예: `0xAB`)과 XOR 연산한 뒤 `encrypted.dat`에 쓸 것
       - `ReadByte()`로 읽고 `-1`(EOF) 이면 종료
       - 각 바이트에 XOR 연산(`^`)을 적용하여 `WriteByte()`로 저장
    3. **복호화**: `encrypted.dat`를 읽어 각 바이트에 동일한 키(`0xAB`)로 다시 XOR 연산하여 `decrypted.txt`에 쓸 것
       - XOR은 같은 키로 두 번 적용하면 원본이 복원되는 성질을 이용
    4. 원본, 암호화 결과, 복호화 결과를 각각 출력할 것
    5. 
    **예상 출력**

    ```
    원본: Hello Unity World
    암호화 완료(파일 크기: 17 bytes)
    복호화 완료
    복호화 결과: Hello Unity World
    원본과 일치: True
    ```*/

    void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, "testFile");
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        string text = "Hello Unity World";
        string secretFile = Path.Combine(path, "secret.txt");
        string encryptedFile = Path.Combine(path, "encrypted.dat");
        string decryptedFile = Path.Combine(path, "decrypted.txt");
        byte key = 0xAB;
        File.WriteAllText(secretFile, text);
        Debug.Log($"원본: {text}");

        using (FileStream fsIn = new FileStream(secretFile, FileMode.Open))
        using (FileStream fsOut = new FileStream(encryptedFile, FileMode.Create))
        {
            int data;
            while((data = fsIn.ReadByte()) != -1)
            {
                byte encryptedByte = (byte)(data ^ key);
                fsOut.WriteByte(encryptedByte);
            }
            Debug.Log($"암호화 완료(파일 크기: {fsOut.Length} bytes)");
        }
        using (FileStream fsIn = new FileStream(encryptedFile, FileMode.Open))
        using (FileStream fsOut = new FileStream(decryptedFile, FileMode.Create))
        {
            int data;
            while ((data = fsIn.ReadByte()) != -1)
            {
                byte decryptedByte = (byte)(data ^ key);
                fsOut.WriteByte(decryptedByte);
            }
            Debug.Log("복호화 완료");
        }
        string read = File.ReadAllText(decryptedFile);
        Debug.Log($"복호화 결과: {read}");
        Debug.Log($"원본과 일치: {text == read}");
    }
    private void Update()
    {
    }
}
