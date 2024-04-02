using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DemonWarriorAnimEvent : AnimEvent_LNH
{
    public Transform attackPoint;
    public UnityEvent verticalAttackAct;
    public UnityEvent horizontalAttackAct;
    public void VerticalAttackEvent()
    {
        verticalAttackAct?.Invoke();
    }

    public void HorizontalAttackEvent() 
    {
        horizontalAttackAct?.Invoke();
    }

}
