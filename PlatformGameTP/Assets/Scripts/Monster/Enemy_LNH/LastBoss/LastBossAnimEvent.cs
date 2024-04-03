using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LastBossAnimEvent : AnimEvent_LNH
{
    public UnityEvent clawOnAttackAct;
    public UnityEvent biteOnAttackAct;
    public UnityEvent clawAttackAct;
    public UnityEvent biteAttackAct;
    public UnityEvent fireballAttackAct;

    public void OnClawAttack()
    {
        clawOnAttackAct?.Invoke();
    }

    public void OnBiteAttack()
    {
        biteOnAttackAct?.Invoke();
    }
    public void ClawAttackEvent()
    {
        clawAttackAct?.Invoke();
    }

    public void BiteAttackEvent()
    {
        biteAttackAct?.Invoke();
    }

    public void FireballAttackEvent()
    {
        fireballAttackAct?.Invoke();
    }
}
