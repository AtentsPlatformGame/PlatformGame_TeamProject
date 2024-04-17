using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternDamage : MonoBehaviour
{
    public LayerMask hitmask;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("패턴 충돌");
        if((1 << other.gameObject.layer & hitmask) !=0)
        {
            BattleSystem bs = other.GetComponent<BattleSystem>();
            if(bs != null)
            {
                bs.TakeDamage(10.0f);
            }
        }     

    }



}
