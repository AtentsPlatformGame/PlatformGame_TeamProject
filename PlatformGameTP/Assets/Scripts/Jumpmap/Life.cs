using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Life : CharacterProperty
{
    public int playerLife = 2;
    public GameDirector gameDirector;
    public Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        this.gameDirector.Init(this.playerLife);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerLife--;
            this.gameDirector.Init(this.playerLife);

            GameStop();
            myAnim.SetTrigger("Dead");
        }
    }

    public void GameStop()
    {
        StopAllCoroutines();
    }
    public void TakeLife()
    {
        if(playerLife == 1)
            myAnim.SetTrigger("Dead");
        if(playerLife == 0)
        {
            
        }
    }

}
