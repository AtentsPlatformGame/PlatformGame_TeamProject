using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class TestData : MonoBehaviour
{
    public string loadFileName = "positionData.json"; // 불러올 JSON 파일 이름

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadPositionFromJson();
    }

    private void LoadPositionFromJson()
    {
        // JSON 파일 경로
        string filePath = Path.Combine(Application.persistentDataPath, loadFileName);

        if (File.Exists(filePath))
        {
            // JSON 파일로부터 데이터 읽기
            string jsonData = File.ReadAllText(filePath);

            // JSON 데이터를 벡터로 변환
            Vector3 position = JsonUtility.FromJson<Vector3>(jsonData);

            // 오브젝트 위치 이동
            transform.position = position;

            Debug.Log("Position loaded from: " + filePath);
        }
        else
        {
            Debug.LogWarning("Position data file not found at: " + filePath);
        }
    }
}
