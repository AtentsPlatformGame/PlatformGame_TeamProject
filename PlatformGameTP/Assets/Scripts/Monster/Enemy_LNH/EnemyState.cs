using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyState : EnemyMovement
{
    public enum State
    {
        Create, Normal, Roaming, Battle, Death, Missing
    }
    public State myState = State.Create;
    public LayerMask groundMask;
    public LayerMask moveLimitMask;
    public Rigidbody rigid;
    public Transform hpViewPos;
    public float jumpForce;

    Vector3 startPos;
    Vector3 leftLimitPos;
    Vector3 rightLimitPos;
    Vector3 limitPos;

    float playTime = 0.0f;
    bool isGround = true;

    //HpBar myHpBar;
    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Missing:
                // 놓쳤으니깐 물음표를 띄운다.
                //2~5초 사이의 대기 시간을 가지고 원래 자리로 돌아간다.
                playTime = Random.Range(2.0f, 5.0f);
                MoveToOriginPos(startPos, playTime);
                // 원래 자리로 돌아가고 거기서 노말로 스테이트를 변환
                break;
            case State.Normal:
                //RaycastHit hit = Physics.Raycast(transform.position, Vector2.down, 100.0f, groundMask);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector2.down, out hit, 10.0f, groundMask))
                {
                    if (hit.transform != null)
                    {
                        RaycastHit subHit;
                        if (Physics.Raycast(hit.point + Vector3.down * 0.5f, Vector3.forward, out subHit, 1000.0f, moveLimitMask)) // 보이는걸론 오른쪽, 월드상으론 앞으로
                        {
                            if (subHit.transform != null)
                            {
                                rightLimitPos = new Vector3(0, transform.position.y, subHit.point.z);
                            }
                        }


                        if(Physics.Raycast(hit.point + Vector3.down * 0.5f, Vector3.back, out subHit, 1000.0f, moveLimitMask))
                        {
                            if (subHit.transform != null)
                            {
                                leftLimitPos = new Vector3(0, transform.position.y, subHit.point.z);
                            }
                        }
                        
                    }
                    
                    float leftDist = Vector3.Distance(transform.position, leftLimitPos);
                    float rightDist = Vector3.Distance(transform.position, rightLimitPos);

                    limitPos = (leftDist > rightDist) ? leftLimitPos: rightLimitPos ;

                }
                
                playTime = Random.Range(1.0f, 3.0f);
                // dir을 반대로, 회전도 시킨다.
                StartCoroutine(DelayChangeState(State.Roaming, playTime)); // 돌아간 뒤에 로밍으로 스테이트를 변환
                //base.UpdateAnimState();
                // 로밍으로 바꿀 때 방향을 바꿔 반대로 로밍하게 한다. 이때 회전도 시켜야함
                break;
            case State.Roaming:
                Debug.Log("로밍 상태");
                // normal state에서 방향과 갈 수 있는 최대 위치를 지정해줬으니 그 위치까지 이동한다.
                MoveToPos(limitPos, () => ChangeState(State.Normal));
                //MoveToPos(GetRndPos(), () => ChangeState(State.Normal)); // 자기의 발 아래 레이를 쏴 해당 블록의 좌우 z 경계까지 이동한다. 끝점과 끝점까지
                // 위처럼 스테이트를 다시 노말로 바꾸고 노말에서는 방향을 바꾸어 다시 로밍으로 바꾼다.
                break;
            case State.Battle:
                AttackTarget(myTarget);
                break;
            case State.Death:
                StopAllCoroutines();
                break;
        }
    }

    Vector3 GetRndPos()
    {
        Vector3 dir = Vector3.forward;
        dir = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0) * dir;
        dir *= Random.Range(0.0f, 3.0f);
        return startPos + dir;
    }

    void StateProcess()
    {
        switch (myState)
        {
            case State.Roaming:
                break;
            case State.Battle:
                break;
        }
    }

    IEnumerator DelayChangeState(State s, float t)
    {
        yield return new WaitForSeconds(t);
        ChangeState(s);
    }

    //자신의 반경 3미터 이내의 랜덤한 위치를 계속 로밍 한다.
    // Start is called before the first frame update
    void Start()
    {
        base.Initialize();
        //GameObject.Find("HpBars");
        /*GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/UI/HpStatus"),
         SceneData.Instance.hpBarsTransform);
        myHpBar = obj.GetComponent<HpBar>();
        myHpBar.myTarget = hpViewPos;
        base.changeHpAct.AddListener(myHpBar.ChangeHpSlider);*/

        startPos = transform.position;
        ChangeState(State.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
        if (myState != State.Death)
        {
            IsGround();
        }
    }

    public void FindTarget(Transform target)
    {
        if (myState == State.Death) return;
        myTarget = target;
        ChangeState(State.Battle);
    }

    public void LostTarget()
    {
        if (myState == State.Death) return;
        myTarget = null;
        StopAttack();
        ChangeState(State.Missing);
    }

    protected override void OnDead()
    {
        base.OnDead();
        ChangeState(State.Death);
    }

    public void DisApear()
    {
        StartCoroutine(DisApearing(2.0f));
    }

    IEnumerator DisApearing(float delay)
    {
        yield return new WaitForSeconds(delay);

        //Destroy(myHpBar.gameObject);

        float dist = 2.0f;
        while (dist > 0.0f)
        {
            float delta = 0.5f * Time.deltaTime;
            dist -= delta;
            transform.Translate(Vector3.down * delta, Space.World);
            yield return null;
        }
        Destroy(gameObject);
    }

    void MoveToOriginPos(Vector3 originPos, float playTime)
    {
        StartCoroutine(MovingToOringPos(originPos, playTime));
    }

    IEnumerator MovingToOringPos(Vector3 originPos, float playTime)
    {
        Vector3 dir = originPos - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        if (rotate != null) StopCoroutine(rotate);
        rotate = StartCoroutine(Rotating(dir));


        while (!Mathf.Approximately(dist, 0.0f))
        {
            float delta = moveSpeed * Time.deltaTime;
            if (delta > dist) delta = dist;
            dist -= delta;
            transform.Translate(dir * delta, Space.World);
            // 발 앞에서 레이를 쏴 거기가 땅이라면 점프를 한다.
            if (Physics.Raycast(transform.position + new Vector3(0.0f, 0.2f, 0.0f), transform.forward, 1.1f, groundMask))
            {
                if (isGround)
                {
                    Jump();
                }
            }
            yield return null;
        }

        yield return StartCoroutine(DelayChangeState(State.Normal, playTime));
    }

    void IsGround()
    {
        isGround = Physics.Raycast(transform.position + new Vector3(0.0f, 1.0f, 0.0f), Vector3.down, 1.1f, groundMask);
        //Debug.DrawRay(transform.position + new Vector3(0, 1, 0), Vector3.down, Color.blue);

        myAnim.SetBool("IsGround", isGround);
        if (isGround)
        {
            Debug.Log("hit");
        }
    }

    void Jump()
    {
        rigid.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
