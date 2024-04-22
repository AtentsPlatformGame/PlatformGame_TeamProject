using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePopUp : MonoBehaviour
{
    public GameObject messagePopup;
    public LayerMask playerMask;
    // Start is called before the first frame update
    void Start()
    {
        messagePopup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)  messagePopup.SetActive(true);
    }
    private void OnTriggerStay(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)  messagePopup.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)  messagePopup.SetActive(false);
    }
}
