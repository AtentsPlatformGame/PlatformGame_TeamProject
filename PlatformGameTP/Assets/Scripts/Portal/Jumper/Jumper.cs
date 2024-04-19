using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Jumper : MonoBehaviour
{
    /*
     스테이지 1에서 다른 특수방으로 이동하는 함수들을 바인딩하기 편하도록 묶어놓은 스크립트
     savePlayerPositionAct에 위치 저장하는 오브젝트에 있는 CharacterSave 스크립트에서 Save함수를 바인딩해서 쓰면 됨
     */
    //public UnityEvent<Transform> savePlayerPositionAct;
    public UnityEvent<Transform> JumpToDestinationAct;
    public Transform player;
    [Header("마을 위치"), Space(5)] public Transform villagePos;
    [Header("아이템방 위치"), Space(5)] public Transform itemRoomPos;
    [Header("기믹방 위치"), Space(5)] public Transform gimicRoomPos;
    [Header("보스방 위치"), Space(5)] public Transform bossRoomPos;
    Transform savePoint;

    public void SetSavePoint(Transform _savePoint)
    {
        this.savePoint = _savePoint;
    }

    IEnumerator JumpAfterFade(Transform _player)
    {
        yield return null;
    }

    public void JumpToMainStage()
    {
    }
    public void JumpToVillage()
    {
    }
    public void JumpToItemRoom()
    {
    }
    public void JumpToGimicRoom()
    {
    }
    public void JumpToBoss()
    {
    }
}
