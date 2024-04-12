using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DemonWarriorAnimEvent : AnimEvent_LNH
{
    
    public UnityEvent verticalAttackAct;
    public UnityEvent horizontalAttackAct;
    public UnityEvent deadFadeAct;
    public UnityEvent patternOnAct;
    public UnityEvent patternOffAct;
    public void VerticalAttackEvent()
    {
        verticalAttackAct?.Invoke();
    }

    public void HorizontalAttackEvent() 
    {
        horizontalAttackAct?.Invoke();
    }

    public void OnDeadFadeEvent()
    {
        deadFadeAct?.Invoke();
    }

    public void OnPatternOnEvent()
    {
        patternOnAct?.Invoke();
    }

    public void OnPatternOffEvent()
    {
        patternOffAct?.Invoke();
    }

}
