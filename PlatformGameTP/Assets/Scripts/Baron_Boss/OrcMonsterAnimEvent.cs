using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrcMonsterAnimEvent : AnimEvent_Orc
{
    public UnityEvent verticalAttackAct;
    public UnityEvent horizontalAttackAct;
    public UnityEvent amputeateSkillAct;
    public UnityEvent buffAct;
    public UnityEvent gethitAct;
    public void VerticalAttackEvent()
    {
        verticalAttackAct?.Invoke();
    }

    public void HorizontalAttackEvent()
    {
        horizontalAttackAct?.Invoke();
    }

    public void AmputeateSkillactEvent()
    {
        amputeateSkillAct?.Invoke();
    }

    public void BuffactEvent()
    {
        buffAct?.Invoke();
    }

    public void GethitEvent()
    {
        gethitAct?.Invoke();
    }
}
