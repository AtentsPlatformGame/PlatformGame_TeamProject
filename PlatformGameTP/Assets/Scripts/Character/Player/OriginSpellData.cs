using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpellData
{
    [Header("아이템의 고유넘버")]
    [Tooltip("아이템 고유넘버(Weapon 0~9 / Amor 10 ~ 19 / Spell 20 ~ 29 / Passive 30 ~ 39 / Cursed 40 ~49")] public int ItemNumber;
    [Header("아이템의 아이콘")]
    [Tooltip("아이템 이미지를 바인딩해주세요")] public Sprite itemIcon;
    [Header("아이템 설명 이미지")]
    [Tooltip("아이템 설명 이미지를 바인딩해주세요")] public Sprite itemDescriptionImage;
    [Header("아이템의 타입")]
    [Tooltip("아이템의 타입을 결정합니다.")] public ITEMTYPE ItemType; // 아이템 타입
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

    [Header("평타 크기 변경")]
    [Tooltip("아이템 효과로 파이어볼 크기를 키웁니다."), Range(1, 2)] public int PlusAttackSize; // 평타 크기
    [Header("평타를 두번 발사 할 수 있는지")]
    [Tooltip("아이템 효과로 파이어볼을 한번에 두발 발사합니다.")] public bool IsAttackTwice; // 이중평타 여부
    [Header("평타로 흡혈 여부")]
    [Tooltip("아이템 효과로 평타 적중 시 30% 확률로 체력을 회복합니다.")] public bool IsHealAfterAttack; // 흡혈 여부
    [Header("부활 여부")]
    [Tooltip("아이템 효과로 1회 부활 할 수 있습니다.")] public bool IsResurrectionOneTime; // 부활 여부
    [Header("반칸 피격 고정 여부")]
    [Tooltip("아이템 효과로 피격시 체력이 반칸씩 답니다.")] public bool IsHitOnlyHalf; // 피격 반칸 여부

    // 저주받은 장신구 관련, 
    [Header("공격력 +2, 피격시 데미지 2배 ")]
    [Tooltip("아이템 효과로 공격력이 2 오르지만 피격 데미지가 2배가 됩니다.")] public bool CA_AttackPenalty; // 공격력 관련 패널티
    [Header("획득 골드량 3배, 피격시 모든 골드 소실 ")]
    [Tooltip("아이템 효과로 획득 골드량이 3배가 되지만 피격시 모든 골드를 소실합니다.")] public bool CA_GoldPenalty; // 골드 관련 패널티
    [Header("공격력 1, 체력 1이 증가하지만 방을 이동할 때 마다 피격당함")]
    [Tooltip("아이템 효과로 공격력과 체력이 1씩 오르지만 방을 이동할 때마다 피격당합니다.")] public bool CA_HpPenalty; // 체력 관련 패널티
}
[CreateAssetMenu(fileName = "SpellData", menuName = "ScriptableObject/SpellData", order = 1)]
public class OriginSpellData : ScriptableObject
{
    [SerializeField] SpellData spellDataInfo;
    public ItemStat GetSpellDataInfo()
    {
        ItemStat tmpStat = new ItemStat();
        tmpStat.ItemNumber = spellDataInfo.ItemNumber;
        tmpStat.itemIcon = spellDataInfo.itemIcon;
        tmpStat.itemDescriptionImage = spellDataInfo.itemDescriptionImage;
        tmpStat.ItemType = spellDataInfo.ItemType;
        tmpStat.Ap = spellDataInfo.Ap;
        tmpStat.PlusHeart = spellDataInfo.PlusHeart;
        tmpStat.PlusSpeed = spellDataInfo.PlusSpeed;
        tmpStat.PlusProjectileSpeed = spellDataInfo.PlusProjectileSpeed;
        tmpStat.PlusAttackDelay = spellDataInfo.PlusAttackDelay;
        tmpStat.PlusAttackRange = spellDataInfo.PlusAttackRange;
        tmpStat.ItemsPrice = spellDataInfo.ItemsPrice;
        tmpStat.SpellObject = spellDataInfo.SpellObject;
        tmpStat.PlusAttackSize = spellDataInfo.PlusAttackSize;
        tmpStat.IsAttackTwice = spellDataInfo.IsAttackTwice;
        tmpStat.IsHealAfterAttack = spellDataInfo.IsHealAfterAttack;
        tmpStat.IsResurrectionOneTime = spellDataInfo.IsResurrectionOneTime;
        tmpStat.IsHitOnlyHalf = spellDataInfo.IsHitOnlyHalf;
        tmpStat.CA_AttackPenalty = spellDataInfo.CA_AttackPenalty;
        tmpStat.CA_GoldPenalty = spellDataInfo.CA_GoldPenalty;
        tmpStat.CA_HpPenalty = spellDataInfo.CA_HpPenalty;

        return tmpStat;
    }
}
