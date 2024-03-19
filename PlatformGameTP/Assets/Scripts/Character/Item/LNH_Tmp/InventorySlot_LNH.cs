using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot_LNH : ItemProperty
{
    Image myImg;
    // Start is called before the first frame update
    void Start()
    {
        myImg = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItemStat(ItemStat _itemStat)
    {
        this.itemStat = _itemStat;
        SetInventorySlot();
    }

    void SetInventorySlot()
    {
        myImg.sprite = this.itemStat.itemIcon.sprite;

    }
}
