using LGH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuy : MonoBehaviour
{
    public GameObject buyUI;
    // Start is called before the first frame update
    void Start()
    {
        if (buyUI != null)
        {
            buyUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
