using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrcMonsterAnimEvent : AnimEvent_Orc
{
    public UnityEvent verticalAttackAct;
    public UnityEvent horizontalAttackAct;
    public UnityEvent skillAttackAct;
    public UnityEvent gethitAct;
    public void VerticalAttackEvent()
    {
        verticalAttackAct?.Invoke();
    }

    public void HorizontalAttackEvent()
    {
        horizontalAttackAct?.Invoke();
    }

    public void SkillAttackEvent()
    {
        skillAttackAct?.Invoke();
    }

    public void GethitEvent()
    {
        gethitAct?.Invoke();
    }
}
