using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()=> this.gameObject.SetActive(false);

    // Update is called once per frame
    void Update()
    {
        using UnityEngine;
        using UnityEngine.UI;

public class ShopManager : MonoBehaviour
    {

        public void SetItem(ShopItem item)
        {
            currentItem = item;
            itemNameText.text = item.itemName;
            itemPriceText.text = "Price: " + item.price.ToString();
        }

        public void BuyItem()
        {
            if (currentItem != null && playerInventory != null)
            {
                if (playerInventory.BuyItem(currentItem))
                {
                    Debug.Log("Item purchased: " + currentItem.itemName);
                   
                }
                else
                {
                    Debug.Log("Not enough money to buy: " + currentItem.itemName);
                   
                }
            }
        }
    }
}
