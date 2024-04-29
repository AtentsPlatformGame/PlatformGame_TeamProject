using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.IO;
/*
 * 이 스크립트가 할 일은 씬이 이동할 때마다 로딩씬을 거치고 그 다음에 이동하고자 하는 씬의 정보를 저장해 로딩씬에서 해당 씬으로 이동시키는 역할
 * 아래에 씬으로 이동하는 함수들을 특정 트리거(버튼,레이캐스트 후 등등)에 바인딩 시켜 각각 원하는 씬으로 이동할 수 있도록
 */
public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance = null;
    public string nextSceneName;
    public Transform savePoint;
    public UnityEvent sceneChangeAct;
    public UnityEvent savePlayerProfileAct;
    /*[SerializeField, Header("플레이어 인벤토리 정보 파일 저장 위치")] string filepath_playerProfile;
    [SerializeField, Header("스테이지1 위치 정보 파일 저장 위치")] string filepath_stage1;
    [SerializeField, Header("스테이지2 위치 정보 파일 저장 위치")] string filepath_stage2;*/
    public string filepath_playerProfile;
    public string filepath_stage1;
    public string filepath_stage2;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            filepath_playerProfile = "PlayerProfile.json";
            filepath_stage1 = "stage1Profile.json";
            filepath_stage2 = "stage2Profile.json";
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void GoToIntro()
    {
        nextSceneName = "Intro"; // 일단 적었는데 추후 정확한 이름으로 바꿔야함
        sceneChangeAct?.Invoke();
    }

    #region 튜토리얼

    public void GoToTutorialStage()
    {
        if (File.Exists(filepath_playerProfile))
        {
            File.Delete(filepath_playerProfile);
        }
        if (File.Exists(filepath_stage1))
        {
            File.Delete(filepath_stage1);
        }
        if (File.Exists(filepath_stage2))
        {
            File.Delete(filepath_stage2);
        }

        nextSceneName = "TutorialStage"; // 일단 적었는데 추후 정확한 이름으로 바꿔야함
        sceneChangeAct?.Invoke();
    }

    #endregion

    #region Stage1
    public void GoToStage1()
    {
        savePlayerProfileAct?.Invoke();
        nextSceneName = "Stage1"; // 일단 적었는데 추후 정확한 이름으로 바꿔야함
        sceneChangeAct?.Invoke();
    }
    public void GoToStage1Village()
    {
        savePlayerProfileAct?.Invoke();
        nextSceneName = "Stage1Village"; // 일단 적었는데 추후 정확한 이름으로 바꿔야함
        sceneChangeAct?.Invoke();
    }

    public void GoToStage1GimicRoom()
    {
        Debug.Log("인스턴스의 기믹룸 점프");
        savePlayerProfileAct?.Invoke();
        nextSceneName = "Stage1GimicRoom"; // 일단 적었는데 추후 정확한 이름으로 바꿔야함
        sceneChangeAct?.Invoke();
    }

    public void GoToStage1ItemRoom()
    {
        savePlayerProfileAct?.Invoke();
        nextSceneName = "Stage1ItemRoom"; // 일단 적었는데 추후 정확한 이름으로 바꿔야함
        sceneChangeAct?.Invoke();
    }

    public void GoToStage1Boss()
    {
        savePlayerProfileAct?.Invoke();
        nextSceneName = "Stage1Boss"; // 일단 적었는데 추후 정확한 이름으로 바꿔야함
        sceneChangeAct?.Invoke();
    }
    #endregion

    #region Stage2
    public void GoToStage2()
    {
        savePlayerProfileAct?.Invoke();
        nextSceneName = "Stage2"; // 일단 적었는데 추후 정확한 이름으로 바꿔야함
        sceneChangeAct?.Invoke();
    }

    /*public void GoToStage2Village()
    {
        savePlayerProfileAct?.Invoke();
        nextSceneName = "Stage2Village"; // 일단 적었는데 추후 정확한 이름으로 바꿔야함
        sceneChangeAct?.Invoke();
    }
    public void GoToStage2GimicRoom()
    {
        savePlayerProfileAct?.Invoke();
        nextSceneName = "Stage2GimicRoom"; // 일단 적었는데 추후 정확한 이름으로 바꿔야함
        sceneChangeAct?.Invoke();
    }

    public void GoToStage2ItemRoom()
    {
        savePlayerProfileAct?.Invoke();
        nextSceneName = "Stage2ItemRoom"; // 일단 적었는데 추후 정확한 이름으로 바꿔야함
        sceneChangeAct?.Invoke();
    }

    public void GoToStage2MiddleBoss()
    {
        savePlayerProfileAct?.Invoke();
        nextSceneName = "Stage2MiddleBoss"; // 일단 적었는데 추후 정확한 이름으로 바꿔야함
        sceneChangeAct?.Invoke();
    }
*/

    public void GoToLastBoss()
    {
        savePlayerProfileAct?.Invoke();
        nextSceneName = "LastBoss"; // 일단 적었는데 추후 정확한 이름으로 바꿔야함
        sceneChangeAct?.Invoke();
    }
    #endregion

    public void GoToOuttro()
    {
        nextSceneName = "GoToOuttro"; // 일단 적었는데 추후 정확한 이름으로 바꿔야함
        sceneChangeAct?.Invoke();
    }


}
