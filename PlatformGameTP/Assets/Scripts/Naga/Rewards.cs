using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewards : MonoBehaviour
{
    public GameObject BossRewards;
    public int rewardsCount;
    // Start is called before the first frame update
    void Start()
    {
        rewardsCount = 0;
        BossRewards.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.G) && rewardsCount == 0)
        {
            BossRewards.SetActive(true);
            rewardsCount = 1;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.G) && rewardsCount == 0)
        {
            BossRewards.SetActive(true);
            rewardsCount = 1;
        }
    }

}
