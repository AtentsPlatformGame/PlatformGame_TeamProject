using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot_LNH : ItemProperty
{
    int slotNumber;
    Image myImg;
    // Start is called before the first frame update
    void Start()
    {
        myImg = GetComponent<Image>();
        slotNumber = GetSlotNumber();
        Inventory_LNH myInventory = FindObjectOfType<Inventory_LNH>();
        if(myInventory != null)
            myInventory.updateItemStat[slotNumber].AddListener(SetItemStat);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetItemStat(ItemStat _itemStat)
    {
        this.itemStat = _itemStat;
        SetInventorySlot();
    }

    void SetInventorySlot()
    {
        myImg.sprite = this.itemStat.itemIcon.sprite;
    }

    int GetSlotNumber()
    {
        var cnt = -1;
        for (var i = 0; i < transform.parent.childCount; i++)
        {
            var view = transform.parent.GetChild(i);
            if (view.gameObject.activeSelf)
            {
                cnt++;
                if (view.transform == transform) return cnt;
            }
        }
        return cnt;
    }
}
