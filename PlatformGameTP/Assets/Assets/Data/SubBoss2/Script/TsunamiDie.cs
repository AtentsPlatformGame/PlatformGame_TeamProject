using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsunamiDie : MonoBehaviour
{
    public LayerMask LayerMask;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("쓰나미 충돌!!!!");
        if((1 << other.gameObject.layer & LayerMask) != 0)
        {
            BattleSystem bs = other.GetComponent<BattleSystem>();
            if(bs != null)
            {
                bs.TakeDamage(50.0f);
            }
            
        }
    }
  /*  private void OnTriggerStay(Collider other)
    {
        Debug.Log("쓰나미 충돌!!!!");
        if ((1 << other.gameObject.layer & LayerMask) != 0)
        {
            BattleSystem bs = other.GetComponent<BattleSystem>();
            if (bs != null)
            {
                bs.TakeDamage(999.0f);
            }
        }
    }*/
}
