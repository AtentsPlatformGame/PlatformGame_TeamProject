using InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : BattleSystem
{
    [SerializeField][Header("플레이어 회전 속도")] float rotSpeed = 1.0f;
    [SerializeField][Header("플레이어 점프 세기")] float jumpForce = 2.0f;
    [SerializeField][Header("플레이어 스펠")] Transform spellObject;
    [SerializeField]
    [Header("플레이어 2D 이동 방식 토글")] bool controll2D = true;
    [SerializeField] Vector2 rotYRange = new Vector2(0.0f, 180.0f);
    [SerializeField, Header("플레이어 컨트롤 제어")] bool canMove = true;
    [SerializeField, Header("텔레포트 이펙트")] Transform teleportVFX;
    [SerializeField, Header("텔레포트 잔상 이펙트"), Space(5)] Transform teleportFogVFX;
    [SerializeField, Header("플레이어 최초 스텟")] PlayerStatData playerStatData;

    [SerializeField, Header("스펠 초기화를 위한 인벤토리 업데이트 함수")] UnityEvent<ItemStat> invenUpdate;
    [SerializeField, Header("스펠 초기화를 위한 empty spell")] OriginSpellData spellData;
    [Header("평타 쿨타임 이미지")] public Image fireballCoolTimeImg;
    [Header("텔레포트 쿨타임 이미지")] public Image teleportCoolTimeImg;

    public UnityEvent OnStatsChanged;
    public GameObject orgFireball;
    public LayerMask groundMask;
    public Transform leftAttackPoint;
    public Transform rightAttackPoint;
    public UnityEvent<int> switchTrackedOffset;
    public GameObject DeathIMG;
    

    public bool isSpellReady = false;
    public bool is3d = true;
    public float jumpCoolTime = 0.5f;
    public float teleportCoolTime = 3.0f;

    float curRotY;
    float ap;
    bool isGround;
    bool canAttack = true;
    float attackDeltaTime = 0.0f;
    public float teleportDeltaTime = 0.0f;
    GoldManager goldManager;

    //스텟 정보 델리게이트
    /*public delegate void StatsChangedEvent(float _ap, float _spd);
    public static event StatsChangedEvent OnStatsChanged;*/
    
    //스텟값을 가져오는 함수들
    /*private float GetAP() { return battleStat.AP; }
    private new float GetMoveSpeed() { return battleStat.MoveSpeed; }
    //스텟을 변경하는 함수
    public void ModfyAP(float amount) { battleStat.AP += amount; }
    public void ModfySPD(float amount) { battleStat.MoveSpeed += amount; }*/
    //여기까지 새로 추가 함

    BattleStat originalStat;
    Rigidbody rigid;
    Fireball fireBall;
    Coroutine attackDelay;
    Coroutine teleportDelay;
    Coroutine rotating;

    private void Awake()
    {
        OriginalStatInit(playerStatData.GetPlayerStatInfo());
        //Initialize();
        NotifyStatsChanged();
    }

    private void NotifyStatsChanged()
    {
        //스텟 변경 시 이벤트 호출하는 함수
        OnStatsChanged?.Invoke();
    }

    void Start()
    {
        curRotY = transform.localRotation.eulerAngles.y;
        rigid = this.GetComponent<Rigidbody>();
        DeathIMG.GetComponent<CanvasGroup>().alpha = 0.0f;
        goldManager = FindObjectOfType<GoldManager>();
        if (SoundManager.Instance != null && myAudioSource != null)
        {
            myAudioSource.volume = SoundManager.Instance.soundValue;
            SoundManager.Instance.SetVolumeAct.AddListener(SetVolumeSlider);
            Debug.Log("Player Start, Sound check");
        }
        teleportDelay = null;
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ShowStat();
            Debug.Log($"curHP = {curHP}");
        }
        
        if (isAlive())
        {
            if (canMove)
            {
                if (controll2D) // 앞뒤 2방향으로 움직이고 점프하는 코드
                {
                    IsGround();
                    TryJump();
                    Rotate();
                    Move(); // 회전과 동시에 움직이기
                            //if (canMove) Move(); // 회전이 끝나면 움직이기
                }
                else // 앞뒤, 양옆 4방향으로 움직이는 코드,
                {
                    // Rotate3D();
                    IsGround();
                    TryJump();
                    Move3D();
                }
            }
        }
    }

    public void Initialize(float _hp)
    {
        curHP = _hp;
        PlayerUIwindows.Instance.UpdateHpbar();
    }

    public void Initialize(float _plusHp, bool isArmor)
    {
        if (isArmor)
        {
            curHP = _plusHp + this.originalStat.MaxHp;
            
            PlayerUIwindows.Instance.UpdateHpbar();
        }
        
    }

    public void Initialize(PlayerStatData basicStat)
    {
        this.battleStat.AP = basicStat.GetPlayerStatInfo().AP;
        this.battleStat.MaxHp = basicStat.GetPlayerStatInfo().MaxHp;
        this.battleStat.AttackRange = basicStat.GetPlayerStatInfo().AttackRange;
        this.battleStat.AttackDelay = basicStat.GetPlayerStatInfo().AttackDelay;
        this.battleStat.ProjectileSpeed = basicStat.GetPlayerStatInfo().ProjectileSpeed;
        this.battleStat.MoveSpeed = basicStat.GetPlayerStatInfo().MoveSpeed;
        this.battleStat.AttackSize = basicStat.GetPlayerStatInfo().AttackSize;
        this.battleStat.AttackTwice = basicStat.GetPlayerStatInfo().AttackTwice;
        this.battleStat.HealAfterAttack = basicStat.GetPlayerStatInfo().HealAfterAttack;
        this.battleStat.ResurrectionOneTime = basicStat.GetPlayerStatInfo().ResurrectionOneTime;
        this.battleStat.HitOnlyHalf = basicStat.GetPlayerStatInfo().HitOnlyHalf;
        this.battleStat.CA_AttackPenalty = basicStat.GetPlayerStatInfo().CA_AttackPenalty;
        this.battleStat.CA_GoldPenalty = basicStat.GetPlayerStatInfo().CA_GoldPenalty;
        this.battleStat.CA_HPPenalty = basicStat.GetPlayerStatInfo().CA_HPPenalty;

        
        Initialize(this.battleStat.MaxHp);

        
    }

    void OriginalStatInit(PlayerBattleStat pb)
    {
        this.originalStat.AP = pb.AP;
        this.originalStat.MaxHp = pb.MaxHp;
        this.originalStat.AttackRange = pb.AttackRange;
        this.originalStat.AttackDelay = pb.AttackDelay;
        this.originalStat.ProjectileSpeed = pb.ProjectileSpeed;
        this.originalStat.MoveSpeed = pb.MoveSpeed;
        this.battleStat.AttackSize = pb.AttackSize;
        this.battleStat.AttackTwice = pb.AttackTwice;
        this.battleStat.HealAfterAttack = pb.HealAfterAttack;
        this.battleStat.ResurrectionOneTime = pb.ResurrectionOneTime;
        this.battleStat.HitOnlyHalf = pb.HitOnlyHalf;
        this.battleStat.CA_AttackPenalty = pb.CA_AttackPenalty;
        this.battleStat.CA_GoldPenalty = pb.CA_GoldPenalty;
        this.battleStat.CA_HPPenalty = pb.CA_HPPenalty;
    }
    #region ControllChange
    public void SwitchControllType2D(bool _type)
    {
        controll2D = _type;
        if (controll2D)
        {
            Constraints2D();

        }
        else
        {
            Constraints3D();

        }
    }

    void Constraints2D()
    {
        rigid.constraints = RigidbodyConstraints.None;
        rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    void Constraints3D()
    {
        rigid.constraints = RigidbodyConstraints.None;
        rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
    #endregion

    #region MoveOn2D
    void Move()
    {
        Constraints2D();
        float x = Input.GetAxis("Horizontal");
        Vector3 deltaPos = transform.forward * x * Time.deltaTime * battleStat.MoveSpeed;

        if (!Mathf.Approximately(x, 0.0f) && Input.GetKeyDown(KeyCode.LeftShift) && Mathf.Approximately(teleportDeltaTime, 0.0f)) // 텔레포트, 추후 쿨타임 추가 예정
        {
            if (teleportDelay != null)
            {
                StopCoroutine(teleportDelay);
                teleportDelay = null;
            }
            teleportDelay = StartCoroutine(CoolingTelePort());
            deltaPos += deltaPos.normalized * 1.5f;
            if (Physics.Raycast(new Ray(transform.position + new Vector3(0.0f, 0.5f, 0.0f), transform.forward), out RaycastHit hit,
                1.5f, groundMask))
            {
                Debug.Log("벽에 막힘");
                deltaPos = deltaPos.normalized * hit.distance;
            }
            if(teleportVFX != null)
            {
                teleportVFX.GetComponent<ParticleSystem>().Play();
                Instantiate(teleportFogVFX, transform.position, Quaternion.identity);
            }
        }

        transform.Translate(deltaPos); // 앞뒤 이동.
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        myAnim.SetFloat("SpeedX", Mathf.Abs(x));

    }

    void Rotate()
    {

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // 오른쪽으로 회전, +
        {
            curRotY = 0.0f; // 곧바로 방향 전환
            switchTrackedOffset?.Invoke(1); // 
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // 왼쪽으로 회전, -
        {
            curRotY = 180.0f; // 곧바로 방향 전환
            switchTrackedOffset?.Invoke(-1);
        }
        curRotY = Mathf.Clamp(curRotY, rotYRange.x, rotYRange.y); // rotYRange안에 값으로 제한됨
        transform.localRotation = Quaternion.Euler(0, curRotY, 0);

    }

    void IsGround()
    {

        isGround = Physics.Raycast(transform.position + new Vector3(0.0f, 1.0f, 0.0f), Vector3.down, 1.1f + Mathf.Epsilon, groundMask);
        //Debug.DrawRay(transform.position + new Vector3(0, 1, 0), Vector3.down, Color.blue);

        myAnim.SetBool("IsGround", isGround);
        if (isGround)
        {
            jumpCoolTime += Time.deltaTime;
            Debug.Log("hit");
            myAnim.ResetTrigger("Jumping");
        }
    }

    void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("점프 트라이 중");
            if (isGround && jumpCoolTime >= 0.15f)
            {
                Debug.Log("점프");
                // 점프 사운드 Loop, Play on Awake 꺼야됨
                if (myAudioSource != null)
                {
                    myAudioSource.clip = jumpClip;
                    myAudioSource.PlayOneShot(jumpClip);
                }
                jumpCoolTime = 0.0f;
                Jump();
                myAnim.SetTrigger("Jumping");
                if (myAudioSource.isPlaying) Debug.Log("점프 효과음");
            }
        }
    }

    void Jump()
    {

        Vector3 jumpVelocity = Vector3.up * Mathf.Sqrt(jumpForce * -Physics.gravity.y);
        rigid.AddForce(jumpVelocity, ForceMode.Impulse);
    }
    #endregion

    #region MoveOn3D
    void Move3D()
    {

        Constraints3D();
        float x = Input.GetAxis("Vertical");
        float y = Input.GetAxis("Horizontal");


        Vector3 deltaXPos = Vector3.forward * x * Time.deltaTime * battleStat.MoveSpeed;
        Vector3 deltaYPos = Vector3.right * y * Time.deltaTime * battleStat.MoveSpeed;

        if ((!Mathf.Approximately(x, 0.0f) || !Mathf.Approximately(y, 0.0f)) &&
            Input.GetKeyDown(KeyCode.LeftShift) && Mathf.Approximately(teleportDeltaTime, 0.0f)) // 텔레포트, 추후 쿨타임 추가 예정
        {
            if (teleportDelay != null)
            {
                StopCoroutine(teleportDelay);
                teleportDelay = null;
            }
            teleportDelay = StartCoroutine(CoolingTelePort());
            deltaXPos += deltaXPos.normalized * 1.5f;
            deltaYPos += deltaYPos.normalized * 1.5f;
            if (Physics.Raycast(new Ray(transform.position + new Vector3(0.0f, 0.5f, 0.0f), transform.forward), out RaycastHit hit,
                1.5f, groundMask))
            {
                Debug.Log("벽에 막힘");
                deltaXPos = deltaXPos.normalized * hit.distance;
                deltaYPos = deltaYPos.normalized * hit.distance;
            }
            if (teleportVFX != null)
            {
                teleportVFX.GetComponent<ParticleSystem>().Play();
                Instantiate(teleportFogVFX, transform.position, Quaternion.identity);
            }
        }

        transform.Translate(deltaXPos + deltaYPos, Space.World);
        myAnim.SetFloat("SpeedX", Mathf.Abs(x));
        myAnim.SetFloat("SpeedY", Mathf.Abs(y));
        Rotate3D(x, y);

    }

    void Rotate3D(float x, float y)
    {
        // 월드 기준으로 회전한다.

        if (!Mathf.Approximately(x, 0.0f) || !Mathf.Approximately(y, 0.0f))
        {
            Vector3 lookDir = new Vector3(y, 0, x);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * rotSpeed);
        }

    }

    IEnumerator Rotating(Vector3 dir)
    {
        float angle = Vector3.Angle(transform.forward, dir);
        float rotDir = 1.0f;
        if (Vector3.Dot(transform.right, dir) < 0.0f)
        {
            rotDir = -1.0f;
        }

        while (!Mathf.Approximately(angle, 0.0f))
        {
            float delta = rotSpeed * Time.deltaTime;
            if (delta > angle)
            {
                delta = angle;
            }
            angle -= delta;
            transform.Rotate(Vector3.up * rotDir * delta);
            yield return null;
        }
    }
    #endregion

    public void ControllPlayerAttack(bool _isPossible)
    {
        canAttack = _isPossible;
    }

    public bool CanAttack() 
    { 
        return canAttack; 
    }

    // 이 아래부턴 나중에 스크립트 분리할 수도 있음
    protected void Attack() // 공격 함수, 정면을 정확히 바라볼때만 공격 가능
    {
        if (Mathf.Approximately(attackDeltaTime, 0.0f)) // 쿨타임 계산, 마음에 안드는데 일단 후순위
        {
            if (attackDelay != null)
            {
                StopCoroutine(attackDelay);
            }
            //attackDelay = StartCoroutine(CoolingTime(battleStat.AttackDelay, attackDeltaTime));
            attackDelay = StartCoroutine(CoolingAttack());
        }
        else
        {
            return;
        }
        if (controll2D)
        {
            float angle_F = Vector3.Angle(transform.forward, Vector3.forward); // -> 방향을 볼때 0, 반대는 180
            if (Mathf.Approximately(angle_F, 0.0f) || Mathf.Approximately(angle_F, 180.0f))
                myAnim.SetTrigger("Attack");
        }
        else
        {
            myAnim.SetTrigger("Attack");
        }
    }

    public new void OnAttack()
    {
        StartCoroutine(Attacking());
    }

    IEnumerator Attacking()
    {
        // 애니메이션 이벤트
        // 파이어볼(?)이 생성되어 앞으로 발사되는 함수
        // 불꽃 발사 사운드 Loop, Play on Awake 꺼야됨
        if (myAudioSource != null)
        {
            myAudioSource.clip = fireballClip;
            myAudioSource.PlayOneShot(fireballClip);
            if (myAudioSource.isPlaying) Debug.Log("파이어볼 효과음");
        }
        GameObject obj = Instantiate(orgFireball, rightAttackPoint);
        obj.transform.SetParent(null);
        obj.GetComponent<Fireball>().SetFireBallAP(GetAp()); // 파이어볼 공격력 설정
        obj.GetComponent<Fireball>().SetAttackRange(GetAttackRange()); // 파이어볼 공격 사거리 결정
        obj.GetComponent<Fireball>().SetProjectileSpeed(GetProjectileSpeed()); // 파이어볼 투사체 속도 결정
        obj.GetComponent<Fireball>().SetFireBallScale(GetAttackSize()); // 파이어볼 크기 결정
        obj.GetComponent<Fireball>().SetFireCanConsume(GetHealAfterAttack()); // 파이어볼 흡혈 여부 결정
        yield return new WaitForSeconds(0.25f);
        if (GetAttackTwice())
        {
            GameObject obj2 = Instantiate(orgFireball, rightAttackPoint);
            obj2.transform.SetParent(null);
            obj2.GetComponent<Fireball>().SetFireBallAP(GetAp()); // 파이어볼 공격력 설정
            obj2.GetComponent<Fireball>().SetAttackRange(GetAttackRange()); // 파이어볼 공격 사거리 결정
            obj2.GetComponent<Fireball>().SetProjectileSpeed(GetProjectileSpeed()); // 파이어볼 투사체 속도 결정
            obj2.GetComponent<Fireball>().SetFireBallScale(GetAttackSize()); // 파이어볼 크기 결정
            obj2.GetComponent<Fireball>().SetFireCanConsume(GetHealAfterAttack()); // 파이어볼 흡혈 여부 결정
        }

    }

    // 아래로 중복코드, 수정 필요함
    IEnumerator CoolingAttack()
    {

       // while (!Mathf.Approximately(battleStat.AttackDelay, attackDeltaTime))
        while (battleStat.AttackDelay >= attackDeltaTime)
        {
            attackDeltaTime += Time.deltaTime;
            fireballCoolTimeImg.fillAmount = attackDeltaTime / (battleStat.AttackDelay );
            yield return null;
        }
        attackDeltaTime = 0f;
    }

    IEnumerator CoolingTelePort()
    {

        while (teleportDeltaTime <= teleportCoolTime)
        {
            teleportDeltaTime += Time.deltaTime;
            teleportCoolTimeImg.fillAmount = teleportDeltaTime / teleportCoolTime;
            yield return null;
        }
        teleportDeltaTime = 0f;
    }

    public void SetSpell(ItemStat _itemStat)
    {
        this.spellObject = _itemStat.SpellObject;
    }
    public void ReadyToUseSpell(bool isReady)
    {
        isSpellReady = isReady;
        myAnim.SetBool("IsSpellReady", isReady);
    }

    public void UsingSpell(Vector3 spellPoint) // 여기서 스펠을 사용한다.
    {
        if (spellObject != null)
        {
            //스펠 사용 사운드 Loop, Play on Awake 꺼야됨
            myAnim.SetTrigger("UseSpell");
            if (spellObject.gameObject.tag == "AttackSpell")
            {
                if (controll2D)
                {
                    Instantiate(spellObject, new Vector3(0.0f, spellPoint.y + 0.1f, spellPoint.z), Quaternion.identity);
                }
                else
                {
                    Instantiate(spellObject, new Vector3(spellPoint.x, spellPoint.y + 0.1f, spellPoint.z), Quaternion.identity);
                }
            }
            else Instantiate(spellObject, this.transform);
            spellObject = null;

            ItemStat tmpStat = spellData.GetSpellDataInfo();
            invenUpdate?.Invoke(tmpStat);
        }

    }

    public void ResetSpellTrigger()
    {
        myAnim.ResetTrigger("UseSpell");
    }

    public Transform GetCurrentSpell()
    {
        return this.spellObject;
    }

    public void HealBuff()
    {
        this.curHP += this.battleStat.MaxHp * 0.5f;
        if (this.curHP >= this.battleStat.MaxHp)
        {
            this.curHP = this.battleStat.MaxHp;
        }
        PlayerUIwindows.Instance.UpdateHpbar();
        Debug.Log("치료 주문");
        //힐 사운드, Loop Play on Awake 꺼야됨
        if (myAudioSource != null)
        {
            myAudioSource.clip = healbuffClip;
            myAudioSource.PlayOneShot(healbuffClip);
        }
    }

    public void HealWithFullHealth()
    {
        this.curHP = this.battleStat.MaxHp;
        PlayerUIwindows.Instance.UpdateHpbar();
        if (myAudioSource != null)
        {
            myAudioSource.clip = healbuffClip;
            myAudioSource.PlayOneShot(healbuffClip);
        }
    }


    public void HealWithConsume()
    {
        this.curHP++;
        if (this.curHP >= this.battleStat.MaxHp)
        {
            this.curHP = this.battleStat.MaxHp;
        }
        PlayerUIwindows.Instance.UpdateHpbar();
    }

    public void SpeedBuff()
    {
        StartCoroutine(SpeedBuffActing());
        if (myAudioSource != null)
        {
            myAudioSource.clip = speedbuffClip;
            myAudioSource.PlayOneShot(speedbuffClip);
        }
        if (myAudioSource.isPlaying) Debug.Log("스피드 버프 효과음");
        // 스피드 버프 사운드 Loop Play on Awake 꺼야됨
    }
    IEnumerator SpeedBuffActing()
    {
        float originSpeed = this.battleStat.MoveSpeed;
        this.battleStat.MoveSpeed += this.battleStat.MoveSpeed * 0.5f;
        yield return new WaitForSeconds(5f);
        this.battleStat.MoveSpeed = originSpeed;
    }
    public void UpdatePlayerStat(BattleStat _itemStat) // 여기서 _itemStat은 인벤토리에서 자기 자식들의 stat을 더한 총합을 넣어야함
    {
        // 위처럼 하지 말고 인벤토리에 들어있는 아이템이 바뀔 경우에만 인벤토리 안에 있는 아이템들의 스텟 총합을 더해서 아래의 5줄짜리 코드를 적용함
        this.battleStat.AP = this.originalStat.AP + _itemStat.AP;
        this.battleStat.MaxHp = this.originalStat.MaxHp + _itemStat.MaxHp;
        this.battleStat.MoveSpeed = this.originalStat.MoveSpeed + _itemStat.MoveSpeed;
        this.battleStat.AttackRange = this.originalStat.AttackRange + _itemStat.AttackRange;
        this.battleStat.ProjectileSpeed = this.originalStat.ProjectileSpeed + _itemStat.ProjectileSpeed;
        this.battleStat.AttackDelay = this.originalStat.AttackDelay - _itemStat.AttackDelay;
        this.battleStat.AttackSize = this.originalStat.AttackSize + _itemStat.AttackSize;

        this.battleStat.AttackTwice = _itemStat.AttackTwice;
        this.battleStat.HealAfterAttack = _itemStat.HealAfterAttack;
        this.battleStat.ResurrectionOneTime = _itemStat.ResurrectionOneTime;
        this.battleStat.HitOnlyHalf = _itemStat.HitOnlyHalf;
        this.battleStat.CA_AttackPenalty = _itemStat.CA_AttackPenalty;
        this.battleStat.CA_GoldPenalty = _itemStat.CA_GoldPenalty;
        this.battleStat.CA_HPPenalty = _itemStat.CA_HPPenalty;

        if (this.battleStat.CA_AttackPenalty)
        {
            this.battleStat.AP += 2;
        }
        if (this.battleStat.CA_HPPenalty)
        {
            this.battleStat.AP += 1;
            this.battleStat.MaxHp += 20;
        }

        
        //Initialize();
       PlayerUIwindows.Instance.UpdateHpbar();
        Debug.Log(this.battleStat.AP + " 공격력 변화 일어남");
        
        //NotifyStatsChanged();
    }

    public bool GetControllType()
    {
        return this.controll2D;
    }

    public bool GetMovePossible()
    {
        return this.canMove;
    }

    public void MoveTrue()
    {
        this.canMove = true;
    }

    public void MoveFalse()
    {
        this.canMove = false;
    }
    public override void TakeDamage(float _dmg)
    {
        if (GetCA_GoldPenalty()) goldManager.ChangeGold(0);
        if (GetHitOnlyHalf())
        {
            //curHP -= _dmg*0.5f;
            _dmg *= 0.5f;
        }

        if (GetCA_HpPenalty())
        {
            curHP -= _dmg * 2f;
        }
        else
        {
            curHP -= _dmg;
        }
        PlayerUIwindows.Instance.UpdateHpbar();

        if (curHP <= 0.0f)
        {
            if (GetResurrectionOneTime())
            {
                curHP = 3.0f;
                PlayerUIwindows.Instance.UpdateHpbar();
                this.battleStat.ResurrectionOneTime = false;
            }
            else
            {
                // 체력이 다 해 쓰러짐
                OnDead();
                myAnim.SetTrigger("Dead");
                if (myAudioSource != null)
                {
                    myAudioSource.clip = deadClip;
                    myAudioSource.PlayOneShot(deadClip);
                }
                if (myAudioSource.isPlaying) Debug.Log("으앙주금");
            }
        }
        else
        {
            myAnim.SetTrigger("Damage");
            if (myAudioSource != null)
            {
                myAudioSource.clip = hitClip;
                myAudioSource.PlayOneShot(hitClip);
                
            }
            if (myAudioSource.isPlaying) Debug.Log("아야");
        }
        Debug.Log($"플레이어 맞음, 현재 체력 {this.curHP}");
    }
    protected override void OnDead()
    {
        //rigid.velocity = Vector3.zero;
        base.OnDead();
        StartCoroutine(ChangeAlpha());
    }

    public IEnumerator ChangeAlpha()
    {
        yield return new WaitForSeconds(2.0f);
        DeathIMG.GetComponent<CanvasGroup>().alpha = 1.0f;
        DeathIMG.GetComponent<CanvasGroup>().blocksRaycasts = true;

    }


    // 추후 삭제 예정
    void ShowStat()
    {
        Debug.Log($"공격력 : {this.battleStat.AP}");
        Debug.Log($"현재 체력 : {this.curHP}");
        Debug.Log($"최대 체력 : {this.battleStat.MaxHp}");
        Debug.Log($"공격 사거리 : {this.battleStat.AttackRange}");
        Debug.Log($"공격 속도 : {this.battleStat.AttackDelay}");
        Debug.Log($"투사체 속도 : {this.battleStat.ProjectileSpeed}");
        Debug.Log($"이동속도 : {this.battleStat.MoveSpeed}");
    }

    
}
