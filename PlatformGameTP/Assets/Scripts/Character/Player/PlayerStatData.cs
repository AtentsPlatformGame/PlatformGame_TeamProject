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
