using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialPortal : MonoBehaviour
{
    //public string nextSceneName;
    public GameObject potalQuestion;
    public Button yesMove;
    public LayerMask Player;
    public float raycastDistance = 100;
    void Start()
    {
        potalQuestion.SetActive(false);
        yesMove.onClick.AddListener(NextScene);
    }

    void NextScene()
    {
        SceneManager.LoadScene("Stage1");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, Player))
        {
            potalQuestion.SetActive(true);
            //ChangeScene();
        }
    }
}
