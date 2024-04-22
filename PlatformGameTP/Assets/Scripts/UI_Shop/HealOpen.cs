using InventorySystem;
using LGH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOpen : MonoBehaviour
{
    public LayerMask NPC;
    public GameObject shopUI;
    public GameObject shopNPC;
    public GameObject GKeyPopup;
    public bool isNPC = false;
    public bool isShop = false;

    private void Start()
    {
        GKeyPopup.SetActive(false);
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, NPC))
        {
            isNPC = true;
            GKeyPopup.SetActive(true);
        }
        else
        {
            isNPC = false;
            GKeyPopup.SetActive(false);
        }
        if (isNPC && Input.GetKeyDown(KeyCode.G))
        {
            if (isShop)
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
        GKeyPopup.SetActive(false);
        isShop = true;
    }
    void CloseShop()
    {
        shopUI.SetActive(false);
        isShop = false;
    }
}
