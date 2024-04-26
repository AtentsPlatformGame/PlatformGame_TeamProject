using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

[System.Serializable]
public struct BattleStat
{
    public float AP; // 공격력
    public float MaxHp; // 최대 체력
    public float AttackRange; // 공격 사거리
    public float AttackDelay; // 공격 속도
    public float ProjectileSpeed; // 투사체 속도
    public float MoveSpeed; // 이동속도
    // 위까지는 어느정도 몬스터와 플레이어가 공유하는 부분
    // 아래부터는 완전 플레이어 전용 스텟들

    /*
        크기 증가 -> int 변수 만들어서 처리
        공격을 한번 더 함 -> bool 변수 만들어서 처리
        체력 회복 -> bool 변수 만들어서 처리
        죽을 시 체력 3으로 부활 -> bool 변수 추가
        피격 반칸 고정 -> bool 변수 추가
     */
    public int AttackSize; // 평타 크기
    public bool AttackTwice; // 이중평타 여부
    public bool HealAfterAttack; // 흡혈 여부
    public bool ResurrectionOneTime; // 부활 여부
    public bool HitOnlyHalf; // 피격 반칸 여부

    public bool CA_AttackPenalty; // 공격력 +2, 피격뎀 *2
    public bool CA_GoldPenalty; // 골드 획득 3배, 피격시 모든 골드 소실
    public bool CA_HPPenalty; // 공1 체력1 증가, 방 이동마다 피격당함

    public Transform StatWindows;
}

public interface IDamage
{
    void TakeDamage(float _dmg);
}

public class BattleSystem : CharacterProperty, IDamage
{
    public UnityEvent<float> changeHpAct;
    [SerializeField] protected BattleStat battleStat;
    [SerializeField] float _curHP = 0.0f;
    public event UnityAction deathAlarm; // event 키워드가 붙으면 이 클래스 밖에서 초기화, 실행이 불가능함. 접근 지정 제한자와 비슷한 느낌? 실수 방지
    
    public Transform attackPoint;
    public LayerMask enemyMask;
    public AudioClip attackSound;
    public AudioClip damageSound;

    protected float curHp; // 수정부분 protected float curHp = 0.0f
    protected float battleTime = 0.0f;
    Transform _target = null;

    SoundManager soundManager;
    AudioSource myAudioSource;
    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        /*soundManager = FindObjectOfType<SoundManager>();
        if(soundManager != null && myAudioSource != null)
            soundManager.SetVolumeAct?.AddListener(SetAudioSourceVolume);*/
        if (SoundManager.Instance != null && myAudioSource != null)
            SoundManager.Instance.SetVolumeAct?.AddListener(SetAudioSourceVolume);
    }
    protected float curHP
    {
        get => _curHP;
        set
        {
            _curHP = value;
            changeHpAct?.Invoke(_curHP / battleStat.MaxHp);
        }
    }

    protected Transform myTarget
    {
        get => _target;
        set
        {
            _target = value;
            if (_target != null)
            {
                BattleSystem bs = _target.GetComponent<BattleSystem>();
                if (bs != null)
                {
                    bs.deathAlarm += TargetDead;
                }
            }
        }
    }
    #region get,set
    public float GetAp()
    {
        return this.battleStat.AP;
    }

    public float GetAttackRange()
    {
        return this.battleStat.AttackRange;
    }

    public float GetProjectileSpeed()
    {
        return this.battleStat.ProjectileSpeed;
    }

    public int GetAttackSize()
    {
        return this.battleStat.AttackSize;
    }

    public bool GetAttackTwice()
    {
        return this.battleStat.AttackTwice;
    }

    public bool GetHealAfterAttack()
    {
        return this.battleStat.HealAfterAttack;
    }

    public bool GetResurrectionOneTime()
    {
        return this.battleStat.ResurrectionOneTime;
    }

    public bool GetHitOnlyHalf()
    {
        return this.battleStat.HitOnlyHalf;
    }

    public float GetCurHP()
    {
        return this.curHP;
    }

    public float GetMaxHP()
    {
        return this.battleStat.MaxHp;
    }

    public float GetMoveSpeed()
    {
        return this.battleStat.MoveSpeed;
    }
    public bool GetCA_AttackPenalty()
    {
        return this.battleStat.CA_AttackPenalty;
    }

    public bool GetCA_HpPenalty()
    {
        return this.battleStat.CA_HPPenalty;
    }

    public bool GetCA_GoldPenalty()
    {
        return this.battleStat.CA_GoldPenalty;
    }

    #endregion

    private void TargetDead()
    {
        StopAllCoroutines();
    }

    protected void Initialize()
    {
        curHP = battleStat.MaxHp;
    }

    

    public virtual void TakeDamage(float _dmg)
    {
        curHP -= _dmg;
        Debug.Log(curHP);
        if (curHP <= 0.0f)
        {

            // 체력이 다 해 쓰러짐
            OnDead();
            myAnim.SetTrigger("Dead");
        }
        else
        {
            myAnim.SetTrigger("Damage");
            StartCoroutine(DamagingEffect(Color.red));
        }
    }

    IEnumerator DamagingEffect(Color effColor)
    {
        foreach (Renderer renderer in allRenderer)
        {
            renderer.material.color = effColor;
        }
        yield return new WaitForSeconds(0.3f);
        foreach (Renderer renderer in allRenderer)
        {
            renderer.material.color = Color.white;
        }
    }

    public void OnAttack()
    {
        myAudioSource.clip = attackSound;
        myAudioSource.Play();
        //AudioSource.PlayClipAtPoint(attackSound, transform.position);
        if (myTarget == null) return;
        BattleSystem bs = myTarget.GetComponent<BattleSystem>();
        if (bs != null)
        {
            bs.TakeDamage(battleStat.AP);
        }
        
    }


    protected virtual void OnDead()
    {

        deathAlarm?.Invoke();

        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
    }


    public bool isAlive()
    {
        return curHP > 0.0f;
    }

    void SetAudioSourceVolume(float soundValue)
    {
        myAudioSource.volume = soundValue;
    }

}

