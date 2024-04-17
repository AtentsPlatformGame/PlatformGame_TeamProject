using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : Inventory
{
    public LayerMask NPC;
    public GameObject shopUI;
    public GameObject shopNPC;
    public GameObject GKeyPopup;
    public Vector3 ShopWithInventory;
    public Vector3 InventoryOrgPos;
    public bool isNPC = false;
    public bool isShop = false;

    private void Start()
    {
        InventoryOrgPos = MyInventory.transform.position;
        
        GKeyPopup.SetActive(false);
    }
    private void Update()
    {
        if (isShop == false)
        {

            MyInventory.transform.position = InventoryOrgPos;
        }
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, NPC))
        {
            isNPC = true;
            GKeyPopup.SetActive(true);
        }
        else
        {
            isNPC = false;
            GKeyPopup.SetActive(false);
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
        //MyInventory.gameObject.SetActive(true);
        //MyInventory.transform.Translate(new Vector3(250,0,0));
        GKeyPopup.SetActive(false);
        isShop = true;
    }
    void CloseShop()
    {
        shopUI.SetActive(false);
        MyInventory.gameObject.SetActive(false);
        //MyInventory.transform.Translate(new Vector3(200, 0, 0));
        isShop = false;
    }
}


