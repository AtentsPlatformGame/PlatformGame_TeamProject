using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory_LNH : MonoBehaviour
{
    public UnityEvent<ItemStat> updatePlayerStatAct;
    public InventorySlot_LNH[] inventorySlots;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 상점에서 구매했을 때, 아이템 파밍시 모두 사용하는 코드
    public void UpdateInventory(ItemStat _itemStat)
    {
        Debug.Log($"전달 받은 아이템 : {_itemStat.ItemType} 타입, 공격력 {_itemStat.Ap}, 추가 체력 {_itemStat.PlusHeart}, 이속 {_itemStat.PlusSpeed}");
        switch (_itemStat.ItemType)
        {
            case ITEMTYPE.WEAPON:
                inventorySlots[0].SetItemStat(_itemStat);
                updatePlayerStatAct?.Invoke(_itemStat);
                break;
            case ITEMTYPE.ARMOR:
                inventorySlots[1].SetItemStat(_itemStat);
                updatePlayerStatAct?.Invoke(_itemStat);
                break;
            case ITEMTYPE.ACCE:
                break;
            case ITEMTYPE.CURSEDACCE:
                break;
            case ITEMTYPE.PASSIVE:
                break;
            case ITEMTYPE.SPELL:
                break;
        }
    }

    
}
