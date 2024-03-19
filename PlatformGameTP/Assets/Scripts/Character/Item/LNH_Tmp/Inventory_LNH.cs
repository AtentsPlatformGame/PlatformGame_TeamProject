using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory_LNH : MonoBehaviour
{
    public UnityEvent<ItemStat> updatePlayerStatAct; // 플레이어 정보를 새로 입력받은 아이템 정보로 갱신하는 UnityEvent
    public UnityEvent<ItemStat>[] updateItemStat; // 각 인벤토리 슬롯마다 각기 달리 정보를 갱신하는 UnityEvent

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 상점에서 구매했을 때, 아이템 파밍시 모두 사용하는 코드
    public void UpdateInventory(ItemStat _itemStat) // 새로 구매, 파밍한 아이템이 들어왔다면
    {
        // 아이템 타입에 따라서 각 칸의 바인딩 된 함수를 호출하고
        // 플레이어 스텟을 갱신함
        switch (_itemStat.ItemType)
        {
            case ITEMTYPE.WEAPON:
                UpdateSlot(0, _itemStat);
                break;
            case ITEMTYPE.ARMOR:
                UpdateSlot(1, _itemStat);
                break;
            case ITEMTYPE.ACCE:
                UpdateSlot(2, _itemStat);
                break;
            case ITEMTYPE.CURSEDACCE:
                UpdateSlot(3, _itemStat);
                break;
            case ITEMTYPE.PASSIVE:
                UpdateSlot(4, _itemStat);
                break;
            case ITEMTYPE.SPELL:
                UpdateSlot(5, _itemStat);
                break;
            default:
                break;
        }
    }

    void UpdateSlot(int idx, ItemStat _itemStat)
    {
        updateItemStat[idx]?.Invoke(_itemStat);
        updatePlayerStatAct?.Invoke(_itemStat);

    }
    
}
