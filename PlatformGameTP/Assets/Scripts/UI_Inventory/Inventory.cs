using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] Transform MyInventory;
    [SerializeField] Transform MyOptions;
    public bool checkInventory;
    public bool checkOptions;
    // Start is called before the first frame update
    void Start()
    {
        if (MyInventory != null)
        {
            checkInventory = false;
            MyInventory.gameObject.SetActive(false);
            checkOptions = false;
            MyOptions.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (checkInventory == false && checkOptions == false)
            {
                PopUp(MyInventory);
                checkInventory= true;
            }
            else
            {
                PopDown(MyInventory);
                checkInventory= false;
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (checkOptions == false && checkOptions == false)
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

