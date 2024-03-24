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
        if(Currentlife == 0)
        {
            Currentlife = 0;
            EndPlayer();
            countDown.SetActive(false);
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
