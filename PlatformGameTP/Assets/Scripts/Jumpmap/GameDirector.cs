using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    private int totalScore;
    public GameObject[] lifes;

    public void Init(int playerLife)
    {
        for (int i = 0; i < lifes.Length; i++)
            this.lifes[i].SetActive(false);

        for (int i = 0; i < playerLife; i++)
            this.lifes[i].SetActive(true);
    }
    
}
