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
    public UnityEvent<Transform> savePlayerPositionAct;
    Transform savePoint;
    #region Stage1 Jumper
    public void JumpToStage1()
    {
        SceneChanger.instance.GoToStage1();
    }

    public void JumpToStage1Gimic()
    {
        Debug.Log("기믹으로 점프");
        savePlayerPositionAct?.Invoke(savePoint);
        SceneChanger.instance.GoToStage1GimicRoom();
    }

    public void JumpToStage1Item()
    {
        savePlayerPositionAct?.Invoke(savePoint);
        SceneChanger.instance.GoToStage1ItemRoom();
    }

    public void JumpToStage1Boss()
    {
        savePlayerPositionAct?.Invoke(savePoint);
        SceneChanger.instance.GoToStage1Boss();
    }
    #endregion

    public void SetSavePoint(Transform _savePoint)
    {
        this.savePoint = _savePoint;
    }
}
