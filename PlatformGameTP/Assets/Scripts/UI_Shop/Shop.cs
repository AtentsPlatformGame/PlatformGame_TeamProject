using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace LGH
{
    public class Shop : ItemProperty
    {
        public UnityEvent<ItemStat> updateStatAct;
        GameObject itemToBuy;

        public GameObject shopUI;
        public GameObject shopBuyQues;
        public GameObject CheckBuyItems;
        public GameObject NoMoney;
        public GameObject FinishBuy;
        public GameObject[] itemObj;
        public int NowGold = 0;
        

        private void Start()
        {
            shopUI.SetActive(false);
            shopBuyQues.SetActive(false);
            CheckBuyItems.SetActive(false);
            NoMoney.SetActive(false);
            FinishBuy.SetActive(false);
            
        }

        public void OnPurchase()
        {
            //itemToBuy = EventSystem.current.currentSelectedGameObject; // 선택한 아이템 정보
            ShopItem_LNH shopItem = itemToBuy.GetComponent<ShopItem_LNH>(); // 선택한 아이템이 가지고 있는 스크립트
            
            if (shopItem != null) // 만약 그 스크립트가 존재한다면
            {
                ItemStat buyItemStat = shopItem.GetItemStat();
                if (PlayerGold >= buyItemStat.ItemsPrice)
                {
                    PlayerGold -= buyItemStat.ItemsPrice;
                   
                    updateStatAct?.Invoke(buyItemStat);
                    FinishBuy.SetActive(true);
                    Debug.Log($"{buyItemStat.ItemType} 타입, 공격력 {buyItemStat.Ap}, 추가 체력 {buyItemStat.PlusHeart}, 이속 {buyItemStat.PlusSpeed}, 가격{buyItemStat.ItemsPrice}");
                }
                else
                {
                    NoMoney.SetActive(true);
                    Debug.Log("돈없음");
                }
            } // 위에서 어떠한 처리를 하면 되겠습니다. 현재는 단순히 정보 출력만 합니다.

        }

        public void CheckBuyItem()
        {
            CheckBuyItems.SetActive(true);
  
            itemToBuy = EventSystem.current.currentSelectedGameObject;

        }
    }
}
/*
    UI 버튼(지금 씬에선 WeaponBT,ArmorBT 등등으로 만듦)을 눌렀을 때 호출되는 OnClick 함수에 들어갈 함수
   버튼을 누르면 지금은 누른 버튼이 가지고 있는 아이템 정보를 출력하는데
   나중에는 버튼을 누르면 구매 여부를 묻는 창이 나타나고 그 창에서 다시 확인을 누르면 현재 제품 가격과 보유금을 비교하여
   돈이 많으면 구매 -> 아래 코드에서 updateStatAct?.Invoke(); 호출 후 버튼의 정보를 수정하거나 교체한다.
    -> Invoke는 플레이어 정보를 수정하는 함수를 구현해 넣으면 됩니다. 이 함수는 나중에 인벤토리에서도 상황에 따라 사용할 수 있을거같습니다.
   돈이 적으면 실패 -> 그냥 아무것도 안하기

   호출 후 버튼의 정보를 수정하거나 교체한다 ->
   현재 계단식 업그레이드를 구현하고자하는데, 나무 -> 돌 -> 철 -> 다이아 이런식의 업그레이드에서
   나무를 구매하면 버튼이 돌로 바뀌고, 돌을 구매하면 철로, 철에서 다이아로, 다이아에선 sold out 이미지로 단계별로 수정하는 것을 의미합니다

   단계별로 수정하기 위해서 2가지 방법이 생각나는데,
   1 : 버튼을 모두 미리 만들어놓고 차례차례 setactive를 true에서 false, false에서 true로 바꿔나가는겁니다

   2 : 버튼을 클릭해 구매가 성공했을 시 미리 저장한 단계별 정보들을 가져와 하나의 버튼에 차례차례 갱신해나가는 겁니다.

   2중 하나의 방법을 이용해 구매하는 아이템 정보를 수정하면 될 것 같습니다.
    */