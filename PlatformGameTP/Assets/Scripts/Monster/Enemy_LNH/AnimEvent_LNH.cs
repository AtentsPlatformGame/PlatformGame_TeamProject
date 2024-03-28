using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent_LNH : MonoBehaviour
{
    public UnityEvent attackAct;
    public UnityEvent deadAct;
    
    public void OnAttack()
    {
        attackAct?.Invoke();
    }

    public void OnDead()
    {
        deadAct?.Invoke();
    }
}
