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
    public UnityEvent spawnTombStoneAct;
    public UnityEvent phase2Act;
    public UnityEvent specialAttackAct;
    public UnityEvent fadeInAct;
    public UnityEvent fadeOutAct;
    public UnityEvent patternOnAct;
    public UnityEvent patternOffAct;
    public UnityEvent playerMoveFalseAct;
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

    public void SpawnTombStoneEvent()
    {
        spawnTombStoneAct?.Invoke();
    }

    public void ChangeToPhase2()
    {
        phase2Act?.Invoke();
    }

    public void OnSpecialAttack()
    {
        specialAttackAct?.Invoke();
    }

    public void OnFadeIn()
    {
        fadeInAct?.Invoke();
    }

    public void OnFadeOut()
    {
        fadeOutAct?.Invoke();
    }

    public void OnPatternOn()
    {
        patternOnAct?.Invoke();
    }

    public void OnClearAct()
    {
        playerMoveFalseAct?.Invoke();
        if (SceneChanger.instance != null) SceneChanger.instance.GoToOuttro();
    }
    
}
