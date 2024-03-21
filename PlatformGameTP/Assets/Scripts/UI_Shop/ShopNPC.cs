using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    public LayerMask NPC;
    public GameObject shopUI;
    public GameObject shopNPC;
    public bool isNPC = false;
    public bool isShop = false;

    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, NPC))
        {
            isNPC = true;
        }
        else
        {
            isNPC = false;
        }
        if(isNPC && Input.GetKeyDown(KeyCode.G))
        {
            if(isShop)
            {
                CloseShop();
            }
            else
            {
                OpenShop();
            }
        }
    }
    void OpenShop()
    {
        shopUI.SetActive(true);
        isShop = true;
    }
    void CloseShop()
    {
        shopUI.SetActive(false);
        isShop = false;
    }
}


