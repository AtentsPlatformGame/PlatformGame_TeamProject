using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 스텟에 직접적으로 영향을 주는 무기,방어구,장신구의 정보 저장 스크립트
public enum ITEMTYPE
{
    NONE, // 아이템이 아님
    WEAPON, // 무기
    ARMOR, // 방어구
    ACCE, // 장신구(악세)
    SPELL, // 스펠
    PASSIVE, // 패시브 아이템
    CURSEDACCE // 저주받은 장신구
}

[System.Serializable]
public struct ItemStat
{
    [Header("아이템의 아이콘")]
    [Tooltip("아이템 이미지를 바인딩해주세요")] public Image itemIcon;
    [Header("아이템의 타입")]
    [Tooltip("아이템의 타입을 결정합니다.")]public ITEMTYPE ItemType; // 아이템 타입
    [Header("아이템(무기)의 공격력")] 
    [Tooltip("무기의 공격력을 결정합니다.")] public int Ap; // 공격력(무기)
    [Header("아이템(방어구)의 추가 체력")] 
    [Tooltip("방어구 추가 체력 개수를 결정합니다.")] public float PlusHeart; // 추가 체력(방어구)
    [Header("아이템(장신구)의 추가 이속")] 
    [Tooltip("장신구의 추가 이동속도를 결정합니다.")] public float PlusSpeed; // 추가 이속
    // 이 아래로 위와 같이 추가
}
public class ItemProperty : MonoBehaviour
{
    [SerializeField] protected ItemStat itemStat;

    #region GEt함수 아래 함수들을 이용해 필요한 정보들을 얻어갑니다.

    public ItemStat GetItemStat()
    {
        return this.itemStat;
    }
    /*public Image GetItemIcon()
    {
        return this.itemStat.itemIcon;
    }
    public ITEMTYPE GetItemType()
    {
        return itemStat.ItemType;
    }
    public int GetAp()
    {
        return itemStat.Ap;
    }

    public float GetPlusHeart()
    {
        return itemStat.PlusHeart;
    }

    public float GetPlusSpeed()
    {
        return itemStat.PlusSpeed;
    }*/
    #endregion
}
