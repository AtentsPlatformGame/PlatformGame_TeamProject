using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : BattleSystem
{
    private enum MonsterState
    {
        Create, // 생성
        Idle, // 노말
        Battle, // 배틀
        Dead // 죽음
    }
    
    public int AP; // 공격력
    public int MaxHp; // 최대 체력
    public float AttackRange; // 공격 사거리
    public float AttackDelay; // 공격 속도
    public float ProjectileSpeed; // 투사체 속도
    
    private MonsterState myState = MonsterState.Create;
    private Transform PlayerTransfrom;
    public LayerMask groundMask;
    [SerializeField] Vector3 leftLimitPos;
    [SerializeField] Vector3 rightLimitPos;
    [SerializeField] public float MoveSpeed = 3.0f;
    Vector3 limitPos;
    protected Vector3 dir;
    protected float myPlayTime;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    protected override void OnDead()
    {
        ChangeState(MonsterState.Dead);
        StartCoroutine(DisApearing(2.0f));
    }
    IEnumerator DisApearing(float delay)
    {
        yield return new WaitForSeconds(delay);
        Color color = allRenderer[0].material.color;
        while (color.a > 0.0f)
        {
            color.a -= Time.deltaTime;
            allRenderer[0].material.color = color;
            yield return null;
        }
        Destroy(gameObject);
    }

    void ChangeState(MonsterState s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case MonsterState.Idle:
                RaycastHit hit; 
                if (Random.Range(0, 4) == 0)
                {
                    dir = Vector3.left;
                    limitPos = leftLimitPos;
                }
                else
                {
                    dir = Vector3.right;
                    limitPos = rightLimitPos;
                }

                break;
            case MonsterState.Battle:
                myPlayTime = 0.0f;
                break;
        }
    }

     void StateProcess()
    {
        switch (myState)
        {
            case MonsterState.Idle:
                float dist = Mathf.Abs(transform.position.z - limitPos.z);
                if (dist < Time.deltaTime * MoveSpeed)
                {
                    dir = -dir;
                    if(dir.z> 0.0f)
                    {
                        limitPos = rightLimitPos;
                    }
                    else if (dir.z < 0.0f)
                    {
                        limitPos = leftLimitPos;
                    }
                }
                break;
            case MonsterState.Battle:
                if (!myAnim.GetBool("IsAttacking")) myPlayTime += Time.deltaTime;
                float temp = myTarget.position.z - transform.position.z;
                if(temp > 0.0f) 
                {
                    dir = Vector3.right;
                }
                else if(temp < 0.0f)
                {
                    dir = Vector3.left;
                }
                else
                {
                    dir = Vector3.zero;
                }
                if (Mathf.Abs(temp) <= AttackRange)
                {
                    dir = Vector2.zero;
                    if (myPlayTime >= AttackDelay)
                    {
                        base.OnAttack();
                    }
                }
                break;
           

        }
    }
   

    

    // Update is called once per frame
    void Update()
    {

    }
}
