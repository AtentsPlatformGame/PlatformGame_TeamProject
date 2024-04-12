using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatternSkillEvent : MonoBehaviour
{
    public Transform patternHitPoint;

    public Transform pillarHitEffect;
    public Transform stonHitEffect;

    public UnityAction pillarAct;
    public UnityAction stonAct;

    public void PillarHitEffect()
    {
        Instantiate(pillarHitEffect, transform.position, Quaternion.identity, null);
    }   
    
    public void StonHitEffect()
    {
        Instantiate(stonHitEffect, transform.position, Quaternion.identity, null);
    }

    public void PillarActEvent()
    {
        pillarAct?.Invoke();
    }

    public void StonActEvent()
    {
        stonAct?.Invoke();
    }
}
