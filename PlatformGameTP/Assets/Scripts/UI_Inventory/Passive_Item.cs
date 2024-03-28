using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passive_Item : ItemProperty
{
    public int[] PassiveRnd = new int[3];
    public GameObject ClickToRnd;

    //passive아이템 정보
    //public GameObject prefab1;
   
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PassiveRandom()
    {
        for (int i = 0; i < 3; i++) {
            PassiveRnd[i] = Random.Range(30, 39);          
            if(i == 1)
            {
                while (PassiveRnd[i-1] == PassiveRnd[i])
                {
                    PassiveRnd[i] = Random.Range(30, 39);
                }
            }
            if(i == 2)
            {
                while (PassiveRnd[i - 2] == PassiveRnd[i] || PassiveRnd[i] == PassiveRnd[i - 1])
                {
                    PassiveRnd[i] = Random.Range(30, 39);
                }
            }
         
        }
        return;

    }
}
