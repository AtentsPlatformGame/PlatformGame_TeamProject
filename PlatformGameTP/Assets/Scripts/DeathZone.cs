using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    public LayerMask playerMask;

    private void OnTriggerEnter(Collider other)
    {
        if((1 << other.gameObject.layer & playerMask)!= 0)
        {
            BattleSystem bs = player.GetComponent<BattleSystem>();
            if(bs != null)
            {
                bs.TakeDamage(100f);
                playerCam.GetComponent<CinemachineVirtualCamera>().Follow = null;
            }
        }
    }
}
