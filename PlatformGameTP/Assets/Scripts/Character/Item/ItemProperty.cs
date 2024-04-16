using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 인벤토리 정보창에 들어갈 아이템 정보 저장 스크립트
public enum ITEMTYPE
{
    NONE, // 아이템이 아님
    WEAPON, // 무기
    ARMOR, // 방어구
    SPELL, // 스펠
    PASSIVE, // 패시브 아이템
    CURSEDACCE, // 저주받은 장신구
    BOSSTOKEN1,
    BOSSTOKEN2,
}

[System.Serializable]
public struct ItemStat
{
    [Header("아이템의 고유넘버")]
    [Tooltip("아이템 고유넘버(Weapon 0~9 / Amor 10 ~ 19 / Spell 20 ~ 29 / Passive 30 ~ 39 / Cursed 40 ~49")] public int ItemNumber;
    [Header("아이템의 아이콘")]
    [Tooltip("아이템 이미지를 바인딩해주세요")] public Sprite itemIcon;
    [Header("아이템 설명 이미지")]
    [Tooltip("아이템 설명 이미지를 바인딩해주세요")] public Sprite itemDescriptionImage;
    [Header("아이템의 타입")]
    [Tooltip("아이템의 타입을 결정합니다.")]public ITEMTYPE ItemType; // 아이템 타입
    [Header("아이템(무기)의 공격력")] 
    [Tooltip("무기의 공격력을 결정합니다.")] public float Ap; // 공격력(무기)
    [Header("아이템(방어구)의 추가 체력")] 
    [Tooltip("방어구 추가 체력 개수를 결정합니다.")] public float PlusHeart; // 추가 체력(방어구)
    [Header("아이템(장신구)의 추가 이속")] 
    [Tooltip("장신구의 추가 이동속도를 결정합니다.")] public float PlusSpeed; // 추가 이속
    [Header("아이템(장신구)의 추가 투사체 속도")]
    [Tooltip("장신구의 추가 투사체속도를 결정합니다.")] public float PlusProjectileSpeed; // 추가 투사체속도
    [Header("아이템(장신구)의 추가 공격속도")]
    [Tooltip("장신구의 추가 공격속도를 결정합니다.")] public float PlusAttackDelay; // 추가 공격 속도
    [Header("아이템(장신구)의 추가 사정거리")]
    [Tooltip("장신구의 추가 사정거리를 결정합니다.")] public float PlusAttackRange; // 추가 사정거리
    [Header("아이템(장신구) 가격")]
    [Tooltip("아이템의 가격을 결정합니다.")] public int ItemsPrice; // 아이템 가격
    [Header("아이템(스펠) VFX")]
    [Tooltip("만약 아이템이 스펠일 경우 스펠 프리펩을 넣어주세요.")] public Transform SpellObject; // 스펠일 때 스펠 vfx

    // 이 아래로 위와 같이 추가
    [Header("평타 크기 변경")]
    [Tooltip("아이템 효과로 파이어볼 크기를 키웁니다."), Range(1,2)] public int PlusAttackSize; // 평타 크기
    [Header("평타를 두번 발사 할 수 있는지")]
    [Tooltip("아이템 효과로 파이어볼을 한번에 두발 발사합니다.")] public bool IsAttackTwice; // 이중평타 여부
    [Header("평타로 흡혈 여부")]
    [Tooltip("아이템 효과로 평타 적중 시 30% 확률로 체력을 회복합니다.")] public bool IsHealAfterAttack; // 흡혈 여부
    [Header("부활 여부")]
    [Tooltip("아이템 효과로 1회 부활 할 수 있습니다.")] public bool IsResurrectionOneTime; // 부활 여부
    [Header("반칸 피격 고정 여부")]
    [Tooltip("아이템 효과로 피격시 체력이 반칸씩 답니다.")] public bool IsHitOnlyHalf; // 피격 반칸 여부
}
public class ItemProperty : MonoBehaviour
{
    [SerializeField] protected ItemStat itemStat;
    public static int PlayerGold;


    #region GEt함수 아래 함수들을 이용해 필요한 정보들을 얻어갑니다.

    public ItemStat GetItemStat()
    {
        return this.itemStat;
    }
   
    #endregion

}
