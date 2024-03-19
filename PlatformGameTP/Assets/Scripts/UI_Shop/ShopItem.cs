using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : ItemProperty
{
    int _price;
    public int price
    {
        get => this._price;

        set
        {
            this._price = value;
        }
    }
}
