using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace LGH
{
    public class Shop : MonoBehaviour
    {
        public UnityEvent<ItemStat> updateStatAct;
        GameObject itemToBuy;
        public void OnPurchase()
        {
            itemToBuy = EventSystem.current.currentSelectedGameObject; // 선택한 아이템 정보
            ShopItem_LNH shopItme = itemToBuy.GetComponent<ShopItem_LNH>(); // 선택한 아이템이 가지고 있는 스크립트
            if (shopItme != null) // 만약 그 스크립트가 존재한다면
            {
                ItemStat buyItemStat = shopItme.GetItemStat();
                updateStatAct?.Invoke(buyItemStat);
                Debug.Log($"{buyItemStat.ItemType} 타입, 공격력 {buyItemStat.Ap}, 추가 체력 {buyItemStat.PlusHeart}, 이속 {buyItemStat.PlusSpeed}");
            } // 위에서 어떠한 처리를 하면 되겠습니다. 현재는 단순히 정보 출력만 합니다.

        }
    }
}

