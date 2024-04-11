using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndingPortal : MonoBehaviour
{
    public UnityEvent turningOnPortalTextImg;
    public UnityEvent turningOffPortalTextImg;
    public UnityEvent gotoEndScene;
    public LayerMask playerMask;
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerMask) != 0)
        {
            Debug.Log("Æ÷Å»¿¡ »ç¶÷ÀÌ µé¾û¿Ô¾î¿ä");
            turningOnPortalTextImg?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerMask) != 0)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                gotoEndScene?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerMask) != 0)
        {
            Debug.Log("Æ÷Å»¿¡ »ç¶÷ÀÌ ³ª°¬¾î¿ä");
            turningOffPortalTextImg?.Invoke();
        }
        
    }

    
}
