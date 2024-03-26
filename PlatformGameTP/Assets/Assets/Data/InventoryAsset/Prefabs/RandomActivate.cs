using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomActivate : MonoBehaviour
{
    public GameObject Kids1;
    public GameObject Kids2;
    public GameObject Kids3;
    public GameObject Parents;
    
    // Start is called before the first frame update
    void Start()
    {
        Kids1.SetActive(false);
        Kids2.SetActive(false);
        Kids3.SetActive(false);
        Parents.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomShow()
    {
        Parents.SetActive(true);
        int RndNumber = Random.Range(0, 3);
        if (RndNumber == 0)
        {
            Kids1.SetActive(true);
        }
        if (RndNumber == 1)
        {
            Kids2.SetActive(true);
        }
        if (RndNumber == 2)
        {
            Kids3.SetActive(true);
        }    
    }

}
