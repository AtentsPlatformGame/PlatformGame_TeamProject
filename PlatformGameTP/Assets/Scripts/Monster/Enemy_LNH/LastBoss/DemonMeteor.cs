using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonMeteor : MonoBehaviour
{
    public LayerMask playerMask;
    public Transform explosionEffect;
    public float meteorDmg;
    private void OnCollisionEnter(Collision collision)
    {
        if((1 << collision.gameObject.layer & playerMask) != 0)
        {
            BattleSystem bs = collision.gameObject.GetComponent<BattleSystem>();
            if(bs != null)
            {
                bs.TakeDamage(meteorDmg);
            }
            Instantiate(explosionEffect, collision.contacts[0].point, Quaternion.identity, null); // collision.contacts[0].point -> 충돌 위치
            Destroy(this.gameObject);
        }
        
    }
}
