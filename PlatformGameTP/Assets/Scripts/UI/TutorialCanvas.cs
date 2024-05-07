using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialCanvas : MonoBehaviour
{
    public UnityEvent playerMoveTrueAct;
    public UnityEvent playerMoveFalseAct;
    // Start is called before the first frame update
    void Start()
    {
        playerMoveFalseAct?.Invoke();
    }

    public void CloseTutoWindow()
    {
        playerMoveTrueAct?.Invoke();
    }
}
