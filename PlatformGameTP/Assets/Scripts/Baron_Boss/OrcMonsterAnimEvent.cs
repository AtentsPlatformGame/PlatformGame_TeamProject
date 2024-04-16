using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrcMonsterAnimEvent : AnimEvent_Orc
{
    public UnityEvent verticalAttackAct;
    public UnityEvent horizontalAttackAct;
    public UnityEvent buffAct;
    public UnityEvent gethitAct;
    public UnityEvent ClearImg;
    public void VerticalAttackEvent()
    {
        verticalAttackAct?.Invoke();
    }

    public void HorizontalAttackEvent()
    {
        horizontalAttackAct?.Invoke();
    }

    public void BuffactEvent()
    {
        buffAct?.Invoke();
    }

    public void GethitEvent()
    {
        gethitAct?.Invoke();
    }

    public void clearImg()
    {
        ClearImg?.Invoke();
    }
}
