using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anemy : AnemyMovemente
{
    public enum State
    {
        Create, Normal, Missing ,Roaming, Battle, Death
    }
    public State myState = State.Create;

    [SerializeField] Vector3 forwardLimitPos;
    [SerializeField] Vector3 backLimitPos;
    Vector3 limitPos;
    Vector3 startPos;
    float playTime = 0.0f;

    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Normal:
                bool isHit = false;
                RaycastHit hit;
                if(Physics.Raycast(transform.position, Vector3.down, out hit, 10.0f, groundMask))
                {
                    RaycastHit subHit;
                    if (Physics.Raycast(hit.point + Vector3.down * 1.0f, Vector3.forward, out subHit, 10.0f, groundMask))
                    {
                        forwardLimitPos = subHit.point;
                        isHit = true;
                    }
                    if(Physics.Raycast(hit.point + Vector3.down * 1.0f, Vector3.back, out subHit, 10.0f, groundMask))
                    {
                        backLimitPos = subHit.point;
                        isHit = true;
                    }

                    if(Random.Range(0,2)==0)
                    {
                        dir = Vector3.forward;
                        limitPos = forwardLimitPos;
                    }
                    else
                    {
                        dir = Vector3.back;
                        limitPos = backLimitPos;
                    }
                    base.UpdateAnimState();
                }
                break;
            case State.Battle:
                myPlayTime = 0.0f;
                break;
               
                
        }

    }

    void StateProcess()
    {
        switch (myState)
        {
            case State.Normal:
                float dist = Mathf.Abs(transform.position.z - limitPos.z);
                if(dist < Time.deltaTime * moveSpeed)
                {
                    dir = -dir;
                    if(dir.z > 0.0f)
                    {
                        limitPos = forwardLimitPos;
                    }
                    else if(dir.z < 0.0f)
                    {
                        limitPos = backLimitPos;
                    }
                    base.UpdateAnimState();
                }
                break;
            case State.Battle:
                if (!myAnim.GetBool("IsAttacking")) myPlayTime += Time.deltaTime;
                float temp = myTarget.position.z - transform.position.z;
                if(temp > 0.0f)
                {
                    dir = Vector3.forward;
                }
                else if(temp < 0.0f)
                {
                    dir = Vector3.back;
                }
                else
                {
                    dir = Vector3.zero;
                }

                if(Mathf.Abs (temp) <= battleStat.AttackRange)
                {
                    dir = Vector3.zero;
                    if(myPlayTime >= battleStat.AttackDelay)
                    {
                        base.OnAttack();
                    }
                }

                UpdateAnimState();

                break;
        }
    }

    void Start()
    {
        base.Initialize();
    }

   
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        StateProcess();
        base.UpdatePosition();
    }
    private void OnCollisionEnter(Collision collision)
    {
        base.CrashEnter(collision);
        if (myState == State.Create)
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if ((1 << contact.otherCollider.gameObject.layer & groundMask) != 0)
                {
                    ChangeState(State.Normal);
                   
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        base.CrashExit(collision);
    }
   
    public void OnFindEnemy(Transform target)
    {
        if (myState == State.Death) return;
        myTarget = target;
        ChangeState(State.Battle);
    }
    public void OnLostEnemy()
    {
        if (myState == State.Death) return;
        ChangeState(State.Normal);
    }
}
