using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrcMonsterPortal : MonoBehaviour
{
    public Transform portal;
    public int portalCount = 0;

    public void PortalEffect()
    {
        if (portalCount == 0)
        {
            Instantiate(portal, transform.position, Quaternion.identity, null);
            portalCount = 1;
        }
    }

   
}
