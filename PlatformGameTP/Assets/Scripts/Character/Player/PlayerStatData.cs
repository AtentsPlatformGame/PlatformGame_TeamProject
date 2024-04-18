using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerBattleStat
{
    public float AP; // ���ݷ�
    public float MaxHp; // �ִ� ü��
    public float AttackRange; // ���� ��Ÿ�
    public float AttackDelay; // ���� �ӵ�
    public float ProjectileSpeed; // ����ü �ӵ�
    public float MoveSpeed; // �̵��ӵ�
    public int AttackSize; // ��Ÿ ũ��
    public bool AttackTwice; // ������Ÿ ����
    public bool HealAfterAttack; // ���� ����
    public bool ResurrectionOneTime; // ��Ȱ ����
    public bool HitOnlyHalf; // �ǰ� ��ĭ ����
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