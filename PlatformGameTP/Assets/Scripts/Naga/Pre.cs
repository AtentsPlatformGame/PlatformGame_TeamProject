using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pre : MonoBehaviour
{
    public GameObject waveAlarm;
    // Start is called before the first frame update
    void Start()
    {
        waveAlarm.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) 
        {
            Debug.Log("A");
            waveAlarm.SetActive(true); 
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            Debug.Log("F"); 
        }
    }
}
