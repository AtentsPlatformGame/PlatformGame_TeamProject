using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OrcMonsterController : EnemyState
{
    public GameObject PatternPillar;
    public GameObject PatternStons;
    public GameObject OrcMonster;

    public GameObject Call;
    public GameObject WarningP;
    public GameObject WarningA;
    public GameObject WarningS;
    public GameObject Clear;
    public GameObject Orcreward;

    public Transform virticalAttackEffect;
    public Transform horizontalAttackEffect;
    public Transform buffEffect;
    public Transform getHitEffect;
    public Transform ClearImg;

    public Transform hitPoint;
    public Transform spawnPoint;

    public Collider InlineCollider;
    public Collider OutlineCollider;
    public Collider OrcCollider;

    [SerializeField] bool isPhaseChanged = false;

    public LayerMask player;
    static int ClearCounts = 0;
    static int callCounts = 0;
    public int phasecount;
    public int PatternDamage = 5; //패턴발생 했을때 충돌 데미지
    public Vector3 PatternPos;

    [Header("평타1대 사운드")]
    public AudioClip attack1Sound; // 평타1대 사운드
    [Header("평타3대 사운드")]
    public AudioClip attack3Sound; // 평타3대 사운드
    [Header("낙석 사운드")]
    public AudioClip stoneSound; // 낙석 사운드
    [Header("낙석 착지 사운드")]
    public AudioClip landingSound; // 낙석 착지 사운드
    [Header("분노 사운드")]
    public AudioClip roarSound; // 분노(포효) 사운드
    [Header("기둥쓰러트리기 사운드")]
    public AudioClip knockdownSound; // 기둥쓰러트리기 사운드
    [Header("피격 사운드")]
    public AudioClip hitSound; // 피격 사운드
    [Header("사망 사운드")]
    public AudioClip deadSound; // 사망 사운드
    [Header("배경음")]
    public AudioClip bgSound; // 배경음
    [Header("다음 맵으로 넘어가기")]
    public GameObject nextStagePortal;


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
                break;
            default:
                break;
        }
    }
    void Start()
    {
        base.Initialize();

        InlineCollider = GetComponent<Collider>();
        OutlineCollider = GetComponent<Collider>();

        startPos = transform.position;
        base.ChangeState(State.Normal);
        PatternPos = new(0.0f, 0.0f, 27.0f);

        PatternPillar.SetActive(false);
        PatternStons.SetActive(false);

        Call.SetActive(false);
        WarningP.SetActive(false);
        WarningA.SetActive(false);
        WarningS.SetActive(false);
        Clear.SetActive(false);
        Orcreward.SetActive(false);
        phasecount = 0;
    }

    void Update()
    {
        base.StateProcess();
        if (this.myState == State.Death) return;
        if (!isPhaseChanged && this.curHp <= (this.battleStat.MaxHp * 0.7))
        {
            ChangeState(State.Phase);
            isPhaseChanged = true;
        }
        if (myState != State.Death)
        {
            base.IsGround();
        }
        if (myState == State.Battle && callCounts == 0)
        {
            Call.SetActive(true);
            Invoke("TurnOffCall", 2.0f);
        }
        Debug.Log(this.curHP);
    }

    public new void OnAttack()
    {
        PlaySound(attack1Sound);
        Collider[] list = Physics.OverlapSphere(attackPoint.position, 3.0f, enemyMask);

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
            int pattern = Random.Range(0, 2);
            Vector3 dir = target.position - transform.position;
            float dist = dir.magnitude - battleStat.AttackRange;
            if (dist < 0.0f) dist = 0.0f;
            float delta;
            if (!myAnim.GetBool("IsAttacking")) battleTime += Time.deltaTime;



            if (this.curHP <= (this.battleStat.MaxHp * 0.8) && phasecount == 0)
            {

                Debug.Log("돌떨구기");
                this.transform.position = PatternPos;
                myAnim.SetBool("IsRunning", false);
                WarningS.SetActive(true);
                InlineCollider.enabled = false;
                OutlineCollider.enabled = false;
                OrcCollider.enabled = false;
                yield return new WaitForSeconds(3.0f);
                myAnim.SetTrigger("PatternS");
                PatternStons.SetActive(true);
                PlaySound(stoneSound);
                yield return new WaitForSeconds(2.0f);
                PlaySound(landingSound);
                // 오크의 체력이 20%남았을때 낙석 패턴
                yield return new WaitForSeconds(3.0f);
                WarningS.SetActive(false);
                PatternStons.SetActive(false);
                InlineCollider.enabled = true;
                OutlineCollider.enabled = true;
                OrcCollider.enabled = true;
                phasecount = 1;

            }

            if (this.curHP <= (this.battleStat.MaxHp * 0.5) && phasecount == 1)
            {
                //this.transform.position = PatternPos;
                myAnim.SetBool("IsRunning", false);
                WarningA.SetActive(true);
                InlineCollider.enabled = false;
                OutlineCollider.enabled = false;
                OrcCollider.enabled = false;
                yield return new WaitForSeconds(3.0f);
                WarningA.SetActive(false);
                myAnim.SetTrigger("Howling");
                // 오크의 체력이 절반이 되었을때 능력치를 버프하는 패턴 
                PlaySound(roarSound);
                this.battleStat.AP += 2;
                this.battleStat.MoveSpeed += 2;
                phasecount = 2;
                Debug.Log("공업 이속업");
                yield return new WaitForSeconds(3.0f);
                InlineCollider.enabled = true;
                OutlineCollider.enabled = true;
                OrcCollider.enabled = true;
            }
            if (this.curHP <= (this.battleStat.MaxHp * 0.3) && phasecount == 2)
            {

                this.transform.position = PatternPos;
                myAnim.SetBool("IsRunning", false);
                WarningP.SetActive(true);
                InlineCollider.enabled = false;
                OutlineCollider.enabled = false;
                OrcCollider.enabled = false;
                yield return new WaitForSeconds(5.0f);
                WarningP.SetActive(false);
                PatternPillar.SetActive(true);
                Debug.Log("기둥 무너짐"); 
                yield return new WaitForSeconds(1.0f);
                PlaySound(knockdownSound);
                // 오크의 체력이 80%일때 기둥을 쓰러트리는 패턴
                yield return new WaitForSeconds(3.0f);
                InlineCollider.enabled = true;
                OutlineCollider.enabled = true;
                OrcCollider.enabled = true;
                //Destroy(PatternPillar);
                PatternPillar.SetActive(false);

                phasecount = 3;

            }


            if (Mathf.Approximately(dist, 0.0f))
            {
                myAnim.SetBool("IsRunning", false);
                if (battleTime >= battleStat.AttackDelay)
                {
                    battleTime = 0.0f;
                    if (pattern == 0)
                    {
                        myAnim.SetTrigger("Attack1");
                    }
                    else
                    {
                        myAnim.SetTrigger("Attack2");
                    }
                }
            }
            else
            {
                dir.Normalize();
                delta = battleStat.MoveSpeed * Time.deltaTime;
                if (delta > dist) delta = dist;
                transform.Translate(dir * delta, Space.World);
                if (Mathf.Approximately(dist, 0.0f))
                {
                    myAnim.SetBool("IsRunning", false);
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


    protected override void OnDead()
    {
        PlaySound(deadsound);
        base.OnDead();
        dropGoldAct?.Invoke(dropGold);
        Orcreward.SetActive(true);
        ChangeState(State.Death);
        nextStagePortal.gameObject.SetActive(true);
    }
    public void OnClear()
    {
        if (myState == State.Death && ClearCounts == 0)
        {
            StartCoroutine(ClearCanvasOn());
        }
    }

    public void VirticalAttackEffect()
    {
        Instantiate(virticalAttackEffect, hitPoint.transform.position, Quaternion.Euler(-60.0f, 0.0f, -90.0f), null);
    }

    public void HorizontalAttackEffect()
    {
        Instantiate(virticalAttackEffect, hitPoint.transform.position, Quaternion.identity, null);
    }


    public void BuffEffect()
    {
        Instantiate(buffEffect, transform.position, Quaternion.identity, null);
    }

    public void GetHitEffect()
    {
        Instantiate(getHitEffect, transform.position, Quaternion.identity, null);
    }

    public void TurnOffCall()
    {
        Call.SetActive(false);
        callCounts = 1;
    }

    public void TurnOffClear()
    {
        Clear.SetActive(false);
        ClearCounts = 1;
    }

    IEnumerator ClearCanvasOn()
    {
        Debug.Log("클리어 코루틴 시작");
        Clear.SetActive(true);
        yield return new WaitForSeconds(3.5f);

        Clear.SetActive(false);
        Debug.Log("클리어 코루틴 끝");
    }
}