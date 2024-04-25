using InventorySystem;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class Inventory :ItemProperty
{

    public Transform MyInventory;
    [SerializeField] Transform MyOptions;

    public GameObject MyExplanation;
    public bool checkInventory;
    public bool checkOptions;
    public bool CheckItemBox;

    // Start is called before the first frame update
    void Start()
    {
        CheckItemBox = false;
        if (MyInventory != null)
        {
            checkInventory = false;
            MyInventory.gameObject.SetActive(false);
 
        }
        if (MyOptions != null)
        {
            checkOptions = false;
            MyOptions.gameObject.SetActive(false);
        }

        if (MyExplanation != null)
        {
           
            MyExplanation.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (checkInventory == false && checkOptions == false && CheckItemBox == false)
            {
                PopUp(MyInventory);
                checkInventory= true;
            }
            else
            {
                PopDown(MyInventory);
                checkInventory= false;
                MyExplanation.gameObject.SetActive(false);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (checkOptions == false && checkInventory == false && CheckItemBox == false)
            {
                PopUp(MyOptions);
                checkOptions= true;
            }
            else
            {
                PopDown(MyOptions);
                checkOptions= false;
            }
        }

     

    }

    public void PopUp(Transform popup)
    {
        popup.gameObject.SetActive(true);
    }
    public void PopDown(Transform popup)
    {
        popup.gameObject.SetActive(false);
    }


}

