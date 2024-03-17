using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스텟에 직접적으로 영향을 주는 무기,방어구,장신구의 정보 저장 스크립트
public enum ITEMTYPE
{
    WEAPON, // 무기
    ARMOR, // 방어구
    ACCE // 장신구(악세)
}

[System.Serializable]
public struct ItemStat
{
    [Header("아이템의 타입")][Tooltip("아이템의 타입을 결정합니다.")]public ITEMTYPE ItemType; // 아이템 타입
    [Header("아이템(무기)의 공격력")] [Tooltip("무기의 공격력을 결정합니다.")] public int Ap; // 공격력(무기)
    [Header("아이템(방어구)의 추가 체력")] [Tooltip("방어구 추가 체력 개수를 결정합니다.")] public int PlusHeart; // 추가 체력(방어구)
    [Header("아이템(장신구)의 추가 이속")] [Tooltip("장신구의 추가 이동속도를 결정합니다.")] public float PlusSpeed; // 추가 이속
    // 이 아래로 위와 같이 추가
}
public class ItemProperty : MonoBehaviour
{
    [SerializeField] protected ItemStat itemStat;

    #region GEt함수
    public ITEMTYPE GetItemType()
    {
        return itemStat.ItemType;
    }
    public int GetAp()
    {
        return itemStat.Ap;
    }

    public int GetPlusHeart()
    {
        return itemStat.PlusHeart;
    }

    public float GetPlusSpeed()
    {
        return itemStat.PlusSpeed;
    }
    #endregion
}
