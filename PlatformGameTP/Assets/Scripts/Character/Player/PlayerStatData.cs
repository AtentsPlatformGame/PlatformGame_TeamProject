using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerBattleStat
{
    public float AP; // 공격력
    public float MaxHp; // 최대 체력
    public float AttackRange; // 공격 사거리
    public float AttackDelay; // 공격 속도
    public float ProjectileSpeed; // 투사체 속도
    public float MoveSpeed; // 이동속도
    public int AttackSize; // 평타 크기
    public bool AttackTwice; // 이중평타 여부
    public bool HealAfterAttack; // 흡혈 여부
    public bool ResurrectionOneTime; // 부활 여부
    public bool HitOnlyHalf; // 피격 반칸 여부
    public bool CA_AttackPenalty; // 공격력 4배, 피격마다 반씩 줄어듦
    public bool CA_GoldPenalty; // 골드 획득 3배, 피격시 모든 골드 소실
    public bool CA_HPPenalty; // 공1 체력1 증가, 방 이동마다 피격당함
}

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObject/PlayerStatData", order = 1)]
public class PlayerStatData : ScriptableObject
{
    [SerializeField] PlayerBattleStat playerStatInfo;
    public PlayerBattleStat GetPlayerStatInfo()
    {
        return this.playerStatInfo;
    }
}
