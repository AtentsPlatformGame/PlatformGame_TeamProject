using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory_LNH : MonoBehaviour
{
    public UnityEvent<BattleStat> updatePlayerStatAct; // 플레이어 정보를 새로 입력받은 아이템 정보로 갱신하는 UnityEvent
    public UnityEvent<ItemStat> updatePlayerSpell;
    public UnityEvent<ItemStat>[] updateItemStat; // 각 인벤토리 슬롯마다 각기 달리 정보를 갱신하는 UnityEvent
    public UnityEvent<float, bool> updatePlayerHP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*private void OnEnable()
    {
        BattleStat calStat = new BattleStat();
        calStat = CalculateInven();
        updatePlayerStatAct?.Invoke(calStat);
    }*/

    // 상점에서 구매했을 때, 아이템 파밍시 모두 사용하는 코드
    public void UpdateInventory(ItemStat _itemStat) // 새로 구매, 파밍한 아이템이 들어왔다면
    {
        BattleStat calStat = new BattleStat();
        // 아이템 타입에 따라서 각 칸의 바인딩 된 함수를 호출하고
        // 플레이어 스텟을 갱신함
        switch (_itemStat.ItemType)
        {
            case ITEMTYPE.WEAPON:
                UpdateSlot(0, _itemStat);
                break;
            case ITEMTYPE.ARMOR:
                UpdateSlot(1, _itemStat);
                updatePlayerHP?.Invoke(_itemStat.PlusHeart, true);
                break;
            case ITEMTYPE.CURSEDACCE:
                UpdateSlot(2, _itemStat);
                break;
            case ITEMTYPE.PASSIVE:
                UpdateSlot(3, _itemStat);
                break;
            case ITEMTYPE.SPELL:
                UpdateSlot(4, _itemStat);
                updatePlayerSpell?.Invoke(_itemStat);
                break;
            case ITEMTYPE.BOSSTOKEN1:
                UpdateSlot(5, _itemStat);
                break;
            case ITEMTYPE.BOSSTOKEN2:
                UpdateSlot(6, _itemStat);
                break;

            default:
                break;
        }

        calStat = CalculateInven();
        updatePlayerStatAct?.Invoke(calStat);
    }

    void UpdateSlot(int idx, ItemStat _itemStat)
    {
        updateItemStat[idx]?.Invoke(_itemStat);

    }

    BattleStat CalculateInven()
    {
        BattleStat tmpStat = new BattleStat();

        for(int i = 0; i < 4; i++)
        {
            InventorySlot_LNH inventoryItemProperty = this.transform.GetChild(i).GetComponent<InventorySlot_LNH>();
            
            if (inventoryItemProperty != null)
            {
                ItemStat inventoryItemStat = inventoryItemProperty.GetItemStat();
                tmpStat.AP += inventoryItemStat.Ap;
                tmpStat.MaxHp += inventoryItemStat.PlusHeart;
                tmpStat.AttackRange += inventoryItemStat.PlusAttackRange;
                tmpStat.ProjectileSpeed += inventoryItemStat.PlusProjectileSpeed;
                tmpStat.AttackDelay += inventoryItemStat.PlusAttackDelay;
                tmpStat.MoveSpeed += inventoryItemStat.PlusSpeed;
                tmpStat.AttackSize += inventoryItemStat.PlusAttackSize;
                if(inventoryItemStat.IsAttackTwice) tmpStat.AttackTwice = inventoryItemStat.IsAttackTwice;
                if(inventoryItemStat.IsHealAfterAttack) tmpStat.HealAfterAttack = inventoryItemStat.IsHealAfterAttack;
                if(inventoryItemStat.IsResurrectionOneTime) tmpStat.ResurrectionOneTime = inventoryItemStat.IsResurrectionOneTime;
                if(inventoryItemStat.IsHitOnlyHalf) tmpStat.HitOnlyHalf = inventoryItemStat.IsHitOnlyHalf;
                if (inventoryItemStat.CA_AttackPenalty) tmpStat.CA_AttackPenalty = inventoryItemStat.CA_AttackPenalty;
                if (inventoryItemStat.CA_GoldPenalty) tmpStat.CA_AttackPenalty = inventoryItemStat.CA_GoldPenalty;
                if (inventoryItemStat.CA_HpPenalty) tmpStat.CA_AttackPenalty = inventoryItemStat.CA_HpPenalty;
            }
        }
        return tmpStat;
    }


    
}
