using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Life : MonoBehaviour
{
    [SerializeField] GameObject countDown;
    [SerializeField] GameObject life;

    public GameObject ReStart;
    public GameObject End;


    public Vector3 respawnPoint;
    public Vector3 endPoint;
    public Vector3 EndObject;

    public GameDirector gameDirector;
    public Transform transform;

    public int originLife;
    int currentLife;
    
    bool failGame = false;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        this.gameDirector.Init(this.currentLife);
    }
    void Init()
    {
        currentLife = originLife;
    } 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time();
        }
        if(currentLife <= 0 && !failGame)
        {
            currentLife = 0;
            EndPlayer();
            countDown.SetActive(false);
            
        }
    }

    void RestartPlayer() // 재시작
    {
        ReStart.transform.position = respawnPoint;
    }

    void EndPlayer() // 실패
    {
        End.transform.position = EndObject;
        failGame = true;
    }

     public void Time()
    {
        currentLife--;
        this.gameDirector.Init(this.currentLife);
        RestartPlayer();
    }
    public void DecreaseLife()
    {
        this.currentLife--;
    }

    public int GetLife()
    {
        return currentLife;
    }


}
