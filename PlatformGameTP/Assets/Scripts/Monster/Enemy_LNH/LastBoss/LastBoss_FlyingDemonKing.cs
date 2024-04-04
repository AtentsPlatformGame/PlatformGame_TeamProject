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

    [SerializeField] Transform spawnTombStone;
    [SerializeField] Collider bossCollider;
    [SerializeField] bool isPhaseChanged = false;
    [SerializeField] bool isSpawnStart = false;
    [SerializeField, Header("아웃라인 퍼셉션")] Transform outLinePerception;
    [SerializeField, Header("인라인 퍼셉션")] Transform inLinePerception;

    Vector3 beforeSpawnPos;

    protected override void ChangeState(State s)
    {
        base.ChangeState(s);
        switch (myState)
        {
            case State.Phase:
                StopAllCoroutines();
                bossCollider.enabled = false;
                myAnim.SetBool("IsRoaming", false);
                myAnim.SetBool("IsRunning", false);
                myAnim.SetTrigger("Spawn");
                // 비석을 소환함
                // 비석을 소환하고 자기는 하늘 위로 올라간다.
                // 시간 내로 비석 기믹을 클리어하지 못하면 피를 일정이상 회복하고 성공하면 다시 아래로 내려와 기절함
                break;
            case State.Phase2:
                StopAllCoroutines();
                myAnim.SetBool("IsRoaming", false);
                myAnim.SetBool("IsRunning", false);
                myAnim.SetTrigger("Phase2");
                // 페이즈2 코루틴 시작하기, 여기선 플레이어 컨트롤 방식 바꾸고 카메라 바꾸고 페이드아웃도 해야하고 할게 많음
                // 방식 바꿀 때 컨트롤 못하게 했다가 특정 버튼이나 뭔가를 클릭하는 걸 둬서 그걸 누르면 다시 움직일 수 있다.
                // 페이즈2에선 일반 패턴 3개랑 일정 쿨타임이 있는 랜덤 위치에 메테오 사용하기 패턴을 추가한다.
                // 페이즈2에선 일반 패턴 데미지나 속도를 수정한다.
                break;
            case State.Dizzy:
                myAnim.SetTrigger("Dizzy");
                bossCollider.enabled = true;
                StartCoroutine(DelayChangeState(State.Battle, 5f));
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Initialize();
        startPos = transform.position;
        base.ChangeState(State.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        base.StateProcess();
        if (this.myState == State.Death) return;
        if (!isSpawnStart && this.curHp <= (this.battleStat.MaxHp * 0.6))
        {
            ChangeState(State.Phase);
            isSpawnStart = true;
        }
        /*if (!isPhaseChanged && this.curHp <= (this.battleStat.MaxHp) * 0.4)
        {
            ChangeState(State.Phase2);
            isPhaseChanged = true;
        }*/
        if (myState != State.Death)
        {
            base.IsGround();
        }
    }

    public void OnClawAttack()
    {
        Collider[] list = Physics.OverlapSphere(clawAttackPoint.position, 3.0f, enemyMask);

        foreach (Collider col in list)
        {
            IDamage act = col.GetComponent<IDamage>();
            if (act != null)
            {
                act.TakeDamage(this.battleStat.AP);
            }
        }
    }

    public void OnBiteAttack()
    {
        Collider[] list = Physics.OverlapSphere(biteAttackPoint.position, 3.0f, enemyMask);

        foreach (Collider col in list)
        {
            IDamage act = col.GetComponent<IDamage>();
            if (act != null)
            {
                act.TakeDamage(this.battleStat.AP);
            }
        }
    }

    protected override IEnumerator AttackingTarget(Transform target)
    {
        while (target != null)
        {
            myAnim.SetBool("IsRunning", true);
            int pattern = Random.Range(0, 3);
            Vector3 dir = target.position - transform.position;
            float dist = dir.magnitude - battleStat.AttackRange;
            if (dist < 0.00001f) dist = 0.0f;
            float delta;
            if (!myAnim.GetBool("IsAttacking")) battleTime += Time.deltaTime;
            if (pattern != 2) // 원거리 공격이 아니라면
            {
                if (Mathf.Approximately(dist, 0.0f)) // 근접 공격
                {
                    myAnim.SetBool("IsRunning", false);
                    if (battleTime >= battleStat.AttackDelay)
                    {
                        battleTime = 0.0f;
                        if (pattern == 0)
                        {
                            myAnim.SetTrigger("Attack1");
                        }
                        else if (pattern == 1)
                        {
                            myAnim.SetTrigger("Attack2");
                        }
                    }
                }
                else
                {
                    dir.Normalize();
                    delta = moveSpeed * Time.deltaTime;
                    if (delta > dist) delta = dist;
                    transform.Translate(dir * delta, Space.World);
                    if (Mathf.Approximately(dist, 0.0f))
                    {
                        myAnim.SetBool("IsRunning", false);
                    }
                }
            }
            else
            {
                myAnim.SetBool("IsRunning", false);
                if (battleTime >= battleStat.AttackDelay)
                {
                    battleTime = 0.0f;
                    myAnim.SetTrigger("Attack3");
                }

            }

            float angle = Vector3.Angle(transform.forward, dir);
            float rotDir = Vector3.Dot(transform.right, dir) < 0.0f ? -1.0f : 1.0f;
            delta = rotSpeed * Time.deltaTime;
            if (delta > angle) delta = angle;
            transform.Rotate(Vector3.up * rotDir * delta);


            yield return null;
        }
        myAnim.SetBool("IsRunning", false);
    }

    protected override IEnumerator DisApearing(float delay)
    {
        yield return new WaitForSeconds(delay);

        //Destroy(myHpBar.gameObject);
        float _fillAmount = 0.0f;
        while (_fillAmount < 1.0f)
        {
            _fillAmount = Mathf.Clamp(_fillAmount + Time.deltaTime, 0.0f, 1.0f);
            foreach (Renderer renderer in allRenderer)
            {
                renderer.material.SetFloat("_DissolveAmount", _fillAmount);
            }
            yield return null;
        }
        Destroy(gameObject);
    }

    public void ClawAttackEffect()
    {
        Transform obj;
        obj = Instantiate(clawAttackEffect, clawAttackPoint.transform.position, Quaternion.Euler(-60.0f, 0.0f, -90.0f), null);
        obj.localScale = new Vector3(2, 2, 2);
    }

    public void BiteAttackEffect()
    {
        Transform obj;
        obj = Instantiate(biteAttackEffect, biteAttackPoint.transform.position, Quaternion.identity, null);
        obj.localScale = new Vector3(2, 2, 2);
    }

    public void FireBallAttackEffect()
    {
        Debug.Log(myTarget.transform.position);
        if (myTarget != null)
        {
            Vector3 dir = myTarget.position - fireBallPoint.position;
            dir.Normalize();
            float angle = Vector3.Angle(dir, fireBallPoint.forward);
            Debug.Log(angle);
            Transform obj;
            obj = Instantiate(fireBallEffect, fireBallPoint.transform.position, Quaternion.Euler(angle, 0.0f, 0.0f), null);
            obj.localScale = new Vector3(2, 2, 2);
        }
    }

    public void SpawnTombStone()
    {
        StartCoroutine(SpawningTombStone());
    }

    IEnumerator SpawningTombStone()
    {
        beforeSpawnPos = transform.position;
        outLinePerception.gameObject.SetActive(false);
        inLinePerception.gameObject.SetActive(false);
        ChangeState(State.Create);
        rigid.useGravity = false;
        Vector3 upPos = transform.position + new Vector3(0, 20, 0);
        Vector3 dir = upPos - fireBallPoint.position;
        float dist = dir.magnitude;
        dir.Normalize();
        float delta = Time.deltaTime * 20.0f;
        while (!Mathf.Approximately(dist, 0.0f))
        {
            if (dist < delta) dist = delta;
            dist -= delta;
            transform.Translate(dir * delta, Space.World);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        if (spawnTombStone != null) spawnTombStone.gameObject.SetActive(true);
        yield return null;
    }

    public void Dizzy()
    {
        StartCoroutine(Dizzing());
    }

    IEnumerator Dizzing()
    {
        Vector3 dir = beforeSpawnPos - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        float delta = Time.deltaTime * 20.0f;
        while (!Mathf.Approximately(dist, 0.0f))
        {
            if (dist < delta) dist = delta;
            dist -= delta;
            transform.Translate(dir * delta, Space.World);
            yield return null;
        } // 다 내려옴
        ChangeState(State.Dizzy);
        yield return new WaitForSeconds(5f);
        outLinePerception.gameObject.SetActive(true);
        inLinePerception.gameObject.SetActive(true);
        rigid.useGravity = true;
    }
}
