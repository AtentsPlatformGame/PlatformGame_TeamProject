using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
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
            Dead();
        }
    }

    public void Dead()
    {
        StopAllCoroutines();
    }

    public void TakeLife()
    {

    }

}
