using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class DiePattern : GameManager
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ãß¶ô");
        Tp(Player, GimicEnd);
        FailGimic();
        CountDownEnd();
        DieFalling();
    }
 
    
}
