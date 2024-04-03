using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBoss_FlyingDemonKing : EnemyState
{
    public Transform clawAttackEffect; // 할퀴기 공격 vfx
    public Transform biteAttackEffect; // 깨물기 공격 vfx
    public Transform fireBallEffect; // 원거리 공격 vfx

    public Transform clawAttackPoint; // 할퀴기 공격 포인트
    public Transform biteAttackPoint; // 깨물기 공격 포인트
    public Transform fireBallPoint; // 원거리 공격 발사 포인트

    [SerializeField] Transform spawnMonster1;
    [SerializeField] Transform spawnMonster2;
    [SerializeField] bool isPhaseChanged = false;

    protected override void ChangeState(State s)
    {
        base.ChangeState(s);
        switch (myState)
        {
            case State.Phase:
                StopAllCoroutines();
                myAnim.SetBool("IsRoaming", false);
                myAnim.SetBool("IsRunning", false);
                myAnim.SetTrigger("Spawn");
                //StartCoroutine(SpawnSkeleton(spawnPoint));
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
