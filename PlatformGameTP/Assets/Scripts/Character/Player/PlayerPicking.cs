using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPicking : MonoBehaviour
{
    public UnityEvent attackAct;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attackAct?.Invoke();
            Debug.Log("Click");
        }
    }
}
