using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPortal : MonoBehaviour
{
    public Transform destinationPortal;
    public Transform gKeyPopUp;
    public LayerMask playerMask;

    private void OnTriggerEnter(Collider other)
    {
        if((1 << other.gameObject.layer & playerMask) != 0)
        {
            gKeyPopUp.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.G))
            {
                other.gameObject.transform.position = destinationPortal.position;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            gKeyPopUp.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.G))
            {
                other.gameObject.transform.position = destinationPortal.position;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            gKeyPopUp.gameObject.SetActive(false);
        }
    }

}
