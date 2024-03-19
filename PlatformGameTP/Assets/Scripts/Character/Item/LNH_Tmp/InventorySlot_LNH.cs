using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot_LNH : ItemProperty
{
    int slotNumber;
    Image myImg;
    // Start is called before the first frame update
    void Start()
    {
        myImg = GetComponent<Image>(); // 아이템 아이콘 이미지
        slotNumber = GetSlotNumber(); // 자신이 부모로부터 몇번째 자식인지 나타내는 정수
        Inventory_LNH myInventory = FindObjectOfType<Inventory_LNH>(); // 인벤토리 스크립트를 찾아서
        if(myInventory != null) // 널이 아니라면
            myInventory.updateItemStat[slotNumber].AddListener(SetItemStat); // UnityEvent에 바인딩
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // UnityEvent에 바인딩할 함수. 현재 칸의 아이템 정보를 새로 받은 아이템 정보로 갱신합니다.
    void SetItemStat(ItemStat _itemStat)
    {
        this.itemStat = _itemStat;
        SetInventorySlot();
    }

    void SetInventorySlot() // 아이템 슬롯의 이미지를 바꿉니다.
    {
        myImg.sprite = this.itemStat.itemIcon;
    }

    /*
     아래 함수는 부모로부터 자신이 몇번째 자식인지 알아내는 코드
    UnityEvent 연결시에 사용합니다
     */
    int GetSlotNumber()
    {
        var cnt = -1;
        for (var i = 0; i < transform.parent.childCount; i++)
        {
            var view = transform.parent.GetChild(i);
            if (view.gameObject.activeSelf)
            {
                cnt++;
                if (view.transform == transform) return cnt;
            }
        }
        return cnt;
    }
}
