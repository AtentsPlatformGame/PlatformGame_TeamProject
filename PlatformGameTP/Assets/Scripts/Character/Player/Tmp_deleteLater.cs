using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tmp_deleteLater : MonoBehaviour
{
    public UnityEvent change2D;
    public UnityEvent change3D;
    public UnityEvent changeTV;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            change2D?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            change3D?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            changeTV?.Invoke();
        }
    }
}
