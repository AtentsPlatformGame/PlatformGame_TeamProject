using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory_LNH : MonoBehaviour
{
    public UnityEvent<ItemStat> updatePlayerStatAct;
    public UnityEvent<ItemStat>[] updateItemStat;
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
        
        switch (_itemStat.ItemType)
        {
            case ITEMTYPE.WEAPON:
                updateItemStat[0]?.Invoke(_itemStat);
                updatePlayerStatAct?.Invoke(_itemStat);
                break;
            case ITEMTYPE.ARMOR:
                updateItemStat[1]?.Invoke(_itemStat);
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
