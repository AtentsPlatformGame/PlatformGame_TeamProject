using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem_LNH : ItemProperty
{
    int _price; // 나중에 가격에 쓸 프로퍼티, 가격은 굳이 아이템 정보에서 필요하지 않을거 같아서 따로 뺌
    public int price {
        get => this._price;

        set
        {
            this._price = value;
        }
    }

}
