using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballPossibleIMG : MonoBehaviour
{
    public GameObject fireballCanAttackImg;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    public void SetPossibleIMGState(bool isPossible)
    {
        fireballCanAttackImg.SetActive(!isPossible);
    }
}
