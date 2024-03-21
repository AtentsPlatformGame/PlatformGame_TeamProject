using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Life : CharacterProperty
{
    [SerializeField] GameObject countDown;
    [SerializeField] GameObject countDown2;
    public GameObject ReStart;
    public GameObject End;

    private Event settime;

    public Vector3 respawnPoint;
    public Vector3 endPoint;
    public Vector3 EndObject;

    public GameDirector gameDirector;
    public Transform transform;

    public int playerLife = 2;
    public int Currentlife;
    float originTime;
    Countdown time;

    // Start is called before the first frame update
    void Start()
    {
        time = FindObjectOfType<Countdown>();
        Currentlife = playerLife;
        this.gameDirector.Init(this.playerLife);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Currentlife--;
            this.gameDirector.Init(this.Currentlife);
            RestartPlayer();
        }
        if(Currentlife <= 0)
        {
            Currentlife = 0;
            EndPlayer();
            countDown.SetActive(false);
            time.SetTime();

            /* 라이프가 0일때 점프맵이 종료되면서 전맵으로 이동한다.
             * 전맵으로 이동하고 나서도 시간을 갱신해야한다.
             * 현재 반복중.
             */
        }

    }

    void RestartPlayer()
    {
        ReStart.transform.position = respawnPoint;
    }

    void EndPlayer()
    {
        End.transform.position = EndObject;
    }

     public void Time()
    {
        Currentlife--;
        this.gameDirector.Init(this.Currentlife);
        RestartPlayer();
    }

}
