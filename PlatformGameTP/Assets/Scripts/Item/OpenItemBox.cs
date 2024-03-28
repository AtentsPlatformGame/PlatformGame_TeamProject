using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class OpenItemBox : Inventory
{
    [SerializeField] GameObject ItemPopup;
    public bool checkBox;
    public GameObject ItemMenu;
    [SerializeField] int ItemCount;
    public GameObject Item1Outline;
    public GameObject Item2Outline;
    public GameObject Item3Outline;
    public GameObject Item1_1;
    public GameObject Item1_2;
    public GameObject Item1_3;
    public GameObject Item2_1;
    public GameObject Item2_2;
    public GameObject Item2_3;
    public GameObject Item3_1;
    public GameObject Item3_2;
    public GameObject Item3_3;
    // Start is called before the first frame update
    void Start()
    {
        ItemCount = 0;
        ItemPopup.SetActive(false);
        checkBox = false;
        Item1_1.SetActive(false);
        Item1_2.SetActive(false);
        Item1_3.SetActive(false);
        Item2_1.SetActive(false);
        Item2_2.SetActive(false);
        Item2_3.SetActive(false);
        Item3_1.SetActive(false);
        Item3_2.SetActive(false);
        Item3_3.SetActive(false);
        Item1Outline.SetActive(false);
        Item2Outline.SetActive(false);
        Item3Outline.SetActive(false);
 
    }

    // Update is called once per frame
    void Update()
    {
        if (checkBox == true && Input.GetKeyUp(KeyCode.G) && ItemCount == 0)
        {
            ItemCount = 1;
            CheckItemBox = true;
            OpenBox();
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (ItemPopup != null) ItemPopup.SetActive(true);
        checkBox = true;
    }
    void OnTriggerStay(Collider other)
    {
        if(ItemPopup != null)ItemPopup.SetActive(true);
        checkBox = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (ItemPopup != null) ItemPopup.SetActive(false);
        checkBox = false;
        CheckItemBox= false;
    }


    void OpenBox()
    {
        ItemMenu.SetActive(true);
        RandomShow();
        ItemPopup.SetActive(false) ;
        Destroy(ItemPopup);
      
    }

    public void RandomShow()
    {
        Item1Outline.SetActive(true);
        Item2Outline.SetActive(true);
        Item3Outline.SetActive(true);
        int RndNum1 = Random.Range(0, 3);
        int RndNum2 = Random.Range(0, 3);
        int RndNum3 = Random.Range(0, 3);
        if (RndNum1 == 0)
        {
            Item1_1.SetActive(true);
        }
        if (RndNum1 == 1)
        {
            Item1_2.SetActive(true);
        }
        if (RndNum1 == 2)
        {
            Item1_3.SetActive(true);
        }


        if (RndNum2 == 0)
        {
            Item2_1.SetActive(true);
        }
        if (RndNum2 == 1)
        {
            Item2_2.SetActive(true);
        }
        if (RndNum2 == 2)
        {
            Item2_3.SetActive(true);
        }


        if (RndNum3 == 0)
        {
            Item3_1.SetActive(true);
        }
        if (RndNum3 == 1)
        {
            Item3_2.SetActive(true);
        }
        if (RndNum3 == 2)
        {
            Item3_3.SetActive(true);
        }

    }
}
