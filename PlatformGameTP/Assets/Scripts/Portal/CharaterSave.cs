using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharaterSave : MonoBehaviour
{
    // 캐릭터의 Transform 컴포넌트
    public Transform characterTransform;
    public Transform savePoint;
    // JSON 파일로 저장할 경로
    public string savePath = "character_position.json";
    public Vector3 startingPosition = new Vector3(0f, 1f, 0f);

    void Start()
    {
        // 플레이어 오브젝트의 위치를 시작 위치로 설정

    }

    void SaveCharacterPosition()
    {
        // 캐릭터의 위치 정보를 담을 데이터 구조 생성
        CharacterPositionData positionData = new CharacterPositionData();
        positionData.position = characterTransform.position;
        positionData.rotation = characterTransform.rotation.eulerAngles;

        // 데이터를 JSON 형식으로 직렬화
        string json = JsonUtility.ToJson(positionData);

        // JSON 파일로 저장
        File.WriteAllText(savePath, json);
    }

    void LoadCharacterPosition()
    {
        // JSON 파일로부터 데이터 읽기
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);

            // JSON을 데이터 구조로 역직렬화
            CharacterPositionData positionData = JsonUtility.FromJson<CharacterPositionData>(json);

            // 캐릭터 위치 설정
            characterTransform.position = positionData.position;
            characterTransform.rotation = Quaternion.Euler(positionData.rotation);

            Debug.Log("Character position loaded from " + savePath);
        }
        else
        {
            Debug.LogWarning("No saved character position found at " + savePath);
        }
    }



    // 키보드 입력으로 저장 및 불러오기 실행
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveCharacterPosition();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            LoadCharacterPosition();
        }
    }
}

// 캐릭터 위치 정보를 담을 데이터 구조
[System.Serializable]
public class CharacterPositionData
{
    public Vector3 position;
    public Vector3 rotation;
}
