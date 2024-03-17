using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Life : CharacterProperty
{
    public int playerLife = 2;
    public GameDirector gameDirector;
    public Transform transform;
    private int maxlife = 2;
    public Vector3 respawnPoint;
    public Vector3 endPoint;
    public GameObject ReStart;
    public GameObject Spawn;
    public int Currentlife;

    // Start is called before the first frame update
    void Start()
    {
        Currentlife = playerLife;
        this.gameDirector.Init(this.playerLife);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerLife--;
            this.gameDirector.Init(this.playerLife);
            RestartPlayer();
        }
        if(Currentlife <= 0)
        {
            Currentlife = 0;
        }
    }

    public void TimeAttack()
    {
        if (playerLife <= 0)
        {
            StopAllCoroutines();
        }
    }
    void RestartPlayer()
    {
        ReStart.transform.position = respawnPoint;
    }

    void End()
    {
        Spawn.transform.position = endPoint;
    }

}
