using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveToNextStage : MonoBehaviour
{
    public Button acceptBt;
    private void OnEnable()
    {
        if (SceneChanger.instance != null)
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name.Equals("TutorialStage")) acceptBt.onClick.AddListener(SceneChanger.instance.GoToStage1);
            else if (scene.name.Equals("Stage1")) acceptBt.onClick.AddListener(SceneChanger.instance.GoToStage1Boss);
            else if (scene.name.Equals("Stage1_Boss")) acceptBt.onClick.AddListener(SceneChanger.instance.GoToStage2);
            else if (scene.name.Equals("Stage2")) acceptBt.onClick.AddListener(SceneChanger.instance.GoToStage2Boss);
            else if (scene.name.Equals("Stage2_Boss")) acceptBt.onClick.AddListener(SceneChanger.instance.GoToStage2_5);
            else if (scene.name.Equals("Stage2.5")) acceptBt.onClick.AddListener(SceneChanger.instance.GoToLastBoss);

        }
    }
    public void CloseWindow()
    {
        this.gameObject.SetActive(false);
    }
}
