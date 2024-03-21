using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tmp_deleteLater : MonoBehaviour
{
    public UnityEvent<bool> changeCamera;
    public UnityEvent<bool> tmpC;
    bool toggle = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            changeCamera?.Invoke(true);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            changeCamera?.Invoke(false);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            tmpC?.Invoke(toggle);
            toggle = !toggle;
        }
    }
}
