using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Savethis : MonoBehaviour
{
    // 캐릭터의 Transform 컴포넌트
    public LayerMask Player;
    public float raycastDistance = 100;
    public Transform characterTransform;
    public string savePath = "character_position.json";

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트의 레이어를 확인
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SaveCharacterPosition();
            Debug.Log("현재 위치에 플레이어가 저장됨");
        }
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


    // 캐릭터 위치 정보를 담을 데이터 구조
    [System.Serializable]
    public class CharacterPositionData
    {
        public Vector3 position;
        public Vector3 rotation;
    }
}
