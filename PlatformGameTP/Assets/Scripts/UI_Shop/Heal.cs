using InventorySystem;
using LGH;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Heal : MonoBehaviour
{
    public UnityEvent<ItemStat> updateStatAct;
    public GameObject shopUI;
    public GameObject shopBuyQues;
    public GameObject CheckBuyItems;
    public GameObject NoMoney;
    public GameObject FinishBuy;
    public int NowGold = 0;

    public int healthToRestore = 100; // 회복할 체력 양

    public PlayerController player;
    private GoldManager playerGoldManager;
    private int playerGold;

    private void Start()
    {
        shopUI.SetActive(false);
        shopBuyQues.SetActive(false);
        CheckBuyItems.SetActive(false);
        NoMoney.SetActive(false);
        FinishBuy.SetActive(false);

        // 플레이어의 골드 관리자를 찾아 할당합니다.
        playerGoldManager = FindObjectOfType<GoldManager>();
        //player = FindObjectOfType<PlayerController>();
        // 현재 골드를 가져옵니다.
        playerGold = playerGoldManager.GetPlayerGold();
        
    }

    public void OnPurchase()
    {
        
        int healthItemPrice = 0; // 체력 회복 아이템의 가격 (예시)
        playerGoldManager.ChangeGold(-100);
        Debug.Log("현재 소지 금액 : ");
        Debug.Log(playerGold);
        if (playerGoldManager.GetPlayerGold() > healthItemPrice)
        {
            // 충분한 골드가 있는 경우
            //UpdatePlayerGold(-healthItemPrice); // 골드 차감
            Nomoney();
            Debug.Log("현재 소지 금액 : ");
            Debug.Log(playerGold);
            RestoreHealth(); // 체력 회복
            CheckBuyItems.SetActive(false);
            FinishBuy.SetActive(true);
        }
        else
        {
            // 골드가 부족한 경우
            NoMoney.SetActive(true);
            Debug.Log("체력 회복 아이템을 구매할 골드가 부족합니다.");
        }
    }

    // 플레이어의 골드를 업데이트합니다.
    private void UpdatePlayerGold(int amount)
    {
        playerGold += amount;
        playerGoldManager.ChangeGold(amount);
    }

    public void Nomoney()
    {
        playerGold = 0;
    }

    // 플레이어의 체력을 회복합니다.
    public void RestoreHealth()
    {
        if (player != null) player.HealWithFullHealth();
        CheckBuyItems.SetActive(true);
        Debug.Log("플레이어 체력 회복 : " + player.GetCurHP());
    }

    private void Update()
    {
        
    }
}
