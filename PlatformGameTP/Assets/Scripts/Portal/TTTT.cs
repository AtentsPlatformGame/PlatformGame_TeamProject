using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TTTT : MonoBehaviour
{
    
    // 캐릭터의 Transform 컴포넌트
    public Transform characterTransform;

    // JSON 파일로 저장할 경로
    public string savePath = "character_position.json";
    CharacterPositionData positionData;

    private void Awake()
    {
        ReadDataInfo();
    }
    void Start()
    {
        LoadCharacterPosition();
    }

    // Update is called once per frame
    void LoadCharacterPosition()
    {
        // JSON 파일로부터 데이터 읽기
        if (File.Exists(savePath))
        {
            // 캐릭터 위치 설정
            characterTransform.position = positionData.position;
            characterTransform.rotation = Quaternion.Euler(positionData.rotation);

            Debug.Log("저장된 플레이어의 위치를 불러옴" + savePath);
        }
        else
        {
            Debug.LogWarning("No saved character position found at " + savePath);
        }
    }

    void ReadDataInfo()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);

            // JSON을 데이터 구조로 역직렬화
            positionData = JsonUtility.FromJson<CharacterPositionData>(json);
        }
    }
}
