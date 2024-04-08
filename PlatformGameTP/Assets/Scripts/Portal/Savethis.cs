using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Savethis : MonoBehaviour
{
    public string saveFileName = "positionData.json"; // 저장할 JSON 파일 이름

    private void SavePositionToJson()
    {
        // 오브젝트의 위치를 가져옴
        Vector3 position = transform.position;

        // 위치 정보를 JSON 형식으로 저장
        string jsonData = JsonUtility.ToJson(position);

        // JSON 파일로 저장
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
        File.WriteAllText(filePath, jsonData);

        Debug.Log("Position data saved to: " + filePath);
    }
}
