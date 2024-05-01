using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LastBoss_FlyingDemonKing : EnemyState
{
    public UnityEvent changePlayerCameraSet;
    [Header("할퀴기 공격 vfx"), Space(5)]
    public Transform clawAttackEffect; // 할퀴기 공격 vfx
    [Header("깨물기 공격 vfx"), Space(5)]
    public Transform biteAttackEffect; // 깨물기 공격 vfx
    [Header("원거리 공격 vfx"), Space(5)]
    public Transform fireBallEffect; // 원거리 공격 vfx

    [Header("할퀴기 사운드")]
    public AudioClip clawSound; // 할퀴기 사운드
    [Header("깨물기 사운드")]
    public AudioClip biteSound; // 깨물기 사운드
    [Header("불쏘기 사운드")]
    public AudioClip fireBallSound; // 불쏘기 사운드
    [Header("포효 사운드")]
    public AudioClip roarSound; // 포효 사운드
    [Header("메테오 사운드")]
    public AudioClip meteorSound; // 메테오 사운드
    [Header("비석 소환 사운드")]
    public AudioClip zeoliteSound; // 비석소환 사운드
    [Header("피격 사운드")]
    public AudioClip hitSound; // 피격 사운드
    [Header("사망 사운드")]
    public AudioClip deadSound; // 사망 사운드
    [Header("배경음")]
    public AudioClip bgSound; // 배경음 사운드


    [Header("할퀴기 공격 포인트"), Space(5)]
    public Transform clawAttackPoint; // 할퀴기 공격 포인트
    [Header("깨물기 공격 포인트"), Space(5)]
    public Transform biteAttackPoint; // 깨물기 공격 포인트
    [Header("원거리 공격 발사 포인트"), Space(5)]
    public Transform fireBallPoint; // 원거리 공격 발사 포인트
    
    [SerializeField] Transform spawnTombStone;
    [SerializeField] Collider bossCollider;
    [SerializeField] bool isPhaseChanged = false;
    [SerializeField] bool isSpawnStart = false;
    [SerializeField, Header("아웃라인 퍼셉션")] Transform outLinePerception;
    [SerializeField, Header("인라인 퍼셉션")] Transform inLinePerception;
    [SerializeField, Header("메테오 공격 쿨타임")] float meteorDelay = 1.0f;
    [SerializeField, Header("클리어 포탈")] Transform clearPortal;
    [SerializeField, Header("보스 피통 UI")] GameObject bossHpBar;
    [SerializeField, Header("보스 피통 슬라이더")] Slider bossHpSlider;

    float meteorCoolTime;
    Vector3 beforeSpawnPos;

    #region ChangeState
    protected override void ChangeState(State s)
    {
        base.ChangeState(s);
        switch (myState)
        {
            case State.Phase:
                StopAllCoroutines();
                
                foreach (Renderer renderer in allRenderer)
                {
                    Color tmpColor = new Color(0, 0, 0, 0);
                    renderer.material.color = tmpColor;
                }
                
                rigid.useGravity = false;
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
                foreach (Renderer renderer in allRenderer)
                {
                    Color tmpColor = new Color(0, 0, 0, 0);
                    renderer.material.color = tmpColor;
                }
                myAnim.SetBool("IsRoaming", false);
                myAnim.SetBool("IsRunning", false);
                myAnim.SetTrigger("Phase2");
                rigid.useGravity = true;
                bossCollider.enabled = true;
                StartCoroutine(DelayChangeState(State.Battle, 5f));
                // 페이즈2 코루틴 시작하기, 여기선 플레이어 컨트롤 방식 바꾸고 카메라 바꾸고 페이드아웃도 해야하고 할게 많음
                // 방식 바꿀 때 컨트롤 못하게 했다가 특정 버튼이나 뭔가를 클릭하는 걸 둬서 그걸 누르면 다시 움직일 수 있다.
                // 페이즈2에선 일반 패턴 3개랑 일정 쿨타임이 있는 랜덤 위치에 메테오 사용하기 패턴을 추가한다.
                // 페이즈2에선 일반 패턴 데미지나 속도를 수정한다.
                break;
            case State.Dizzy:
                myAnim.SetTrigger("Dizzy");
                rigid.useGravity = true;
                bossCollider.enabled = true;
                StartCoroutine(DelayChangeState(State.Battle, 5f));
                break;
            case State.Create:
                break;
            default:
                break;
        }
    }
    #endregion

    #region Enable,Start,Upate
    // Start is called before the first frame update

    private void OnEnable()
    {
        if (bossHpBar != null && !bossHpBar.activeSelf) bossHpBar.SetActive(true);
    }
    void Start()
    {
        base.Initialize();
        startPos = transform.position;
        base.ChangeState(State.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (this.myState == State.Death) return;
        base.StateProcess();

        if (!isSpawnStart && this.curHP <= (this.battleStat.MaxHp * 0.6))
        {
            ChangeState(State.Phase);
            isSpawnStart = true;
        }
        else if (isSpawnStart && !isPhaseChanged && this.curHP <= (this.battleStat.MaxHp) * 0.4)
        {
            ChangeState(State.Phase2);
            isPhaseChanged = true;
        }
        if (myState != State.Death)
        {
            base.IsGround();
        }

        
    }
    #endregion

    #region 공격 판정
    public void OnClawAttack()
    {
        PlaySound(clawSound);
        Collider[] list = Physics.OverlapSphere(clawAttackPoint.position, 4.5f, enemyMask);
       
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
            
            BattleSystem bs = target.gameObject.GetComponent<BattleSystem>();
            if(bs != null)
            {
                if (!bs.isAlive())
                {
                    ChangeState(State.Create);
                    StopAllCoroutines();
                }
            }
            myAnim.SetBool("IsRunning", true);
            int pattern = 0;
            if (isPhaseChanged)
            {
                if (meteorCoolTime >= meteorDelay)
                {
                    meteorCoolTime = 0.0f;
                    // 메테오 발사 트리거를 건다.
                    PlaySound(roarSound);
                    myAnim.SetTrigger("SpecialAttack");
                }
            }
            pattern = Random.Range(0,3);
            
            Vector3 dir = target.position - transform.position;
            float dist = dir.magnitude - battleStat.AttackRange;
            if (dist < 0.00001f) dist = 0.0f;
            float delta;
            if (!myAnim.GetBool("IsAttacking"))
            {
                battleTime += Time.deltaTime;
                meteorCoolTime += Time.deltaTime;
            }
            
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
                    delta = battleStat.MoveSpeed * Time.deltaTime;
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
                if (pattern == 2)
                {
                    if (battleTime >= battleStat.AttackDelay)
                    {
                        battleTime = 0.0f;
                        myAnim.SetTrigger("Attack3");
                    }
                }
                
            }

            float angle = Vector3.Angle(transform.forward, dir);
            float rotDir = Vector3.Dot(transform.right, dir) < 0.0f ? -1.0f : 1.0f;
            delta = rotSpeed * Time.deltaTime;
            if (delta > angle) delta = angle;
            transform.Rotate(Vector3.up * rotDir * delta);

            Debug.Log($"플레이어와의 거리 : {dist}");
            yield return null;
        }
        myAnim.SetBool("IsRunning", false);
    }
    #endregion

    #region 죽은 뒤 사라지기

    public override void TakeDamage(float _dmg)
    {
        curHP -= _dmg;
        Debug.Log(curHP);
        PlaySound(hitSound);
        if (curHP <= 0.0f)
        {
            // 체력이 다 해 쓰러짐
            //PlaySound(deadSound);
            OnDead();
            myAnim.SetTrigger("Dead");
            clearPortal.gameObject.SetActive(true);
        }
        else
        {
            myAnim.SetTrigger("Damage");
            //StartCoroutine(DamagingDemon());
        }
        bossHpSlider.value = this.curHP / this.battleStat.MaxHp;
    }
    IEnumerator DamagingDemon()
    {
        foreach (Renderer renderer in allRenderer)
        {
            Color tmpColor = new Color(255,0,0,0.3f);
            
            renderer.material.color = tmpColor;
        }
        yield return new WaitForSeconds(0.3f);
        foreach (Renderer renderer in allRenderer)
        {
            Color tmpColor = new Color(0, 0, 0, 0);
            renderer.material.color = tmpColor;
        }
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
        if(bossHpBar != null)bossHpBar.SetActive(false);
        Destroy(gameObject);
    }
    #endregion

    #region 기본공격 이펙트 생성
    public void ClawAttackEffect()
    {
        PlaySound(clawSound);
        Transform obj;
        obj = Instantiate(clawAttackEffect, clawAttackPoint.transform.position, Quaternion.Euler(-60.0f, transform.rotation.eulerAngles.y, -90.0f), null);
        obj.localScale = new Vector3(2, 2, 2);
    }

    public void BiteAttackEffect()
    {
        PlaySound(biteSound);
        Transform obj;
        obj = Instantiate(biteAttackEffect, biteAttackPoint.transform.position, Quaternion.identity, null);
        obj.localScale = new Vector3(2, 2, 2);
    }

    public void FireBallAttackEffect()
    {
        PlaySound(fireBallSound);
        Debug.Log(myTarget.transform.position);
        if (myTarget != null)
        {
            Vector3 dir = myTarget.position - fireBallPoint.position;
            dir.Normalize();
            float angle = Vector3.Angle(dir, fireBallPoint.forward);
            Debug.Log(angle);
            Transform obj;
            obj = Instantiate(fireBallEffect, fireBallPoint.transform.position, Quaternion.Euler(angle, transform.rotation.eulerAngles.y, 0.0f), null);
            obj.localScale = new Vector3(2, 2, 2);
        }
    }
    #endregion

    #region 비석 소환 패턴

    public void SpawnTombStone()
    {
        StartCoroutine(SpawningTombStone());
        Debug.Log("비석 소환 패턴 시작");
        //포효를 하고, 비석 소환
    }

    IEnumerator SpawningTombStone()
    {
        beforeSpawnPos = transform.position;
        outLinePerception.gameObject.SetActive(false);
        inLinePerception.gameObject.SetActive(false);
        ChangeState(State.Create);
        rigid.useGravity = false;
        Vector3 upPos = transform.position + new Vector3(0, 20, 0);
        yield return StartCoroutine(MoveForGimic(upPos));
        Debug.Log("이동 끝");
        yield return new WaitForSeconds(1f);
        Debug.Log("비석 소화안애만");
        if (spawnTombStone != null) spawnTombStone.gameObject.SetActive(true);
        yield return null;
    }

    public void Dizzy()
    {
        StartCoroutine(Dizzing());
    }

    IEnumerator Dizzing()
    {
        yield return StartCoroutine(MoveForGimic(beforeSpawnPos));
        
        ChangeState(State.Dizzy);
        // 스턴이 걸린듯한 사운드 추가
        yield return new WaitForSeconds(5f);
        outLinePerception.gameObject.SetActive(true);
        inLinePerception.gameObject.SetActive(true);
    }

    IEnumerator MoveForGimic(Vector3 _pos)
    {
        //Debug.Log($"{_pos}로 이동중");
        Vector3 dir = _pos - transform.position;
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
    }

    public void FailGimic()
    {
        StartCoroutine(Healing());
    }

    IEnumerator Healing()
    {
        bossCollider.enabled = true;
        // 웃는 사운드 추가
        yield return StartCoroutine(MoveForGimic(beforeSpawnPos));
        ChangeState(State.Battle);
        outLinePerception.gameObject.SetActive(true);
        inLinePerception.gameObject.SetActive(true);
        rigid.useGravity = true;
        this.curHP += this.battleStat.MaxHp * 0.2f;
    }
    #endregion

    #region 페이즈2
    public void ChangePhase2()
    {
        changePlayerCameraSet?.Invoke();
    }
    #endregion
}
