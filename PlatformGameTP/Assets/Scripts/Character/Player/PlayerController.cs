using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


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

    public GameObject orgFireball;
    public LayerMask groundMask;
    public Transform leftAttackPoint;
    public Transform rightAttackPoint;
    public UnityEvent<int> switchTrackedOffset;
    public GameObject DeathIMG;


    public bool isSpellReady = false;
    public bool is3d = true;
    public float jumpCoolTime = 0.5f;

    float curRotY;
    float ap;
    bool isGround;
    float attackDeltaTime = 0.0f;
    float teleportDeltaTime = 0.0f;

    
    BattleStat originalStat;
    Rigidbody rigid;
    Fireball fireBall;
    Coroutine attackDelay;
    Coroutine teleportDelay;
    Coroutine rotating;



    // Start is called before the first frame update
    private void Awake()
    {
        OriginalStatInit(playerStatData.GetPlayerStatInfo());
        Initialize();
    }
    void Start()
    {
        curRotY = transform.localRotation.eulerAngles.y;
        rigid = this.GetComponent<Rigidbody>();
        DeathIMG.GetComponent<CanvasGroup>().alpha = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            ShowStat();
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

    void OriginalStatInit(PlayerBattleStat pb)
    {
        this.originalStat.AP = pb.AP;
        this.originalStat.MaxHp = pb.MaxHp;
        this.originalStat.AttackRange = pb.AttackRange;
        this.originalStat.AttackDelay = pb.AttackDelay;
        this.originalStat.ProjectileSpeed = pb.ProjectileSpeed;
        this.originalStat.MoveSpeed = pb.MoveSpeed;
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

        isGround = Physics.Raycast(transform.position + new Vector3(0.0f, 1.0f, 0.0f), Vector3.down, 1.1f, groundMask);
        //Debug.DrawRay(transform.position + new Vector3(0, 1, 0), Vector3.down, Color.blue);

        myAnim.SetBool("IsGround", isGround);
        if (isGround)
        {
            jumpCoolTime += Time.deltaTime;
            Debug.Log("hit");
        }
    }

    void TryJump()
    {
        /*if (isGround && Input.GetKey(KeyCode.Space))
        {
          jumpCharge += Time.deltaTime;
        }

        if (isGround && Input.GetKeyUp(KeyCode.Space))
        {
            Jump();
            myAnim.SetTrigger("Jumping");
        }
        UnityEngine.Debug.Log(isGround);*/
        if (isGround && Input.GetKeyDown(KeyCode.Space) && jumpCoolTime >= 0.25f)
        {
            jumpCoolTime = 0.0f;
            Jump();
            myAnim.SetTrigger("Jumping");
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
        // 애니메이션 이벤트
        // 파이어볼(?)이 생성되어 앞으로 발사되는 함수
        GameObject obj = Instantiate(orgFireball, rightAttackPoint);
        obj.transform.SetParent(null);
        obj.GetComponent<Fireball>().SetFireBallAP(GetAp()); // 파이어볼 공격력 설정
        obj.GetComponent<Fireball>().SetAttackRange(GetAttackRange()); // 파이어볼 공격 사거리 결정
        obj.GetComponent<Fireball>().SetProjectileSpeed(GetProjectileSpeed()); // 파이어볼 투사체 속도 결정
    }

    // 아래로 중복코드, 수정 필요함
    IEnumerator CoolingAttack()
    {

        while (!Mathf.Approximately(battleStat.AttackDelay, attackDeltaTime))
        {
            attackDeltaTime += 1f;
            yield return new WaitForSeconds(1f);
        }
        attackDeltaTime = 0f;
    }

    IEnumerator CoolingTelePort()
    {

        while (!Mathf.Approximately(3.0f, teleportDeltaTime))
        {
            teleportDeltaTime += 1f;
            yield return new WaitForSeconds(1f);
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
        Debug.Log("힐 스펠 사용");
    }

    public void SpeedBuff()
    {
        StartCoroutine(SpeedBuffActing());
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
        /*BattleStat tmpBattleStat = new BattleStat();
        if (_itemStat.ItemType == ITEMTYPE.NONE) return;
        switch (_itemStat.ItemType)
        {
            case ITEMTYPE.WEAPON:
                if (_itemStat.Ap != 0) tmpBattleStat.AP += _itemStat.Ap; // 공격력 증가
                break;
            case ITEMTYPE.ARMOR:
                if (!Mathf.Approximately(_itemStat.PlusHeart, 0.0f)) tmpBattleStat.MaxHp += _itemStat.PlusHeart; // 최대 체력 증가
                break;
            case ITEMTYPE.SPELL:
                if (_itemStat.SpellObject != null) this.spellObject = _itemStat.SpellObject; // 스펠 적용
                break;
            case ITEMTYPE.PASSIVE:
                if (!Mathf.Approximately(_itemStat.PlusSpeed, 0.0f)) tmpBattleStat.MoveSpeed += _itemStat.PlusSpeed; // 이동속도 증가
                if (!Mathf.Approximately(_itemStat.PlusAttackRange, 0.0f)) tmpBattleStat.AttackRange += _itemStat.PlusAttackRange; // 사정거리 증가
                if (!Mathf.Approximately(_itemStat.PlusProjectileSpeed, 0.0f)) tmpBattleStat.ProjectileSpeed += _itemStat.PlusProjectileSpeed; // 투사체 속도 증가
                break;
            case ITEMTYPE.CURSEDACCE:
                if (_itemStat.Ap != 0) tmpBattleStat.AP += _itemStat.Ap; // 공격력 증가
                if (!Mathf.Approximately(_itemStat.PlusHeart, 0.0f)) tmpBattleStat.MaxHp += _itemStat.PlusHeart; // 최대 체력 증가
                if (!Mathf.Approximately(_itemStat.PlusSpeed, 0.0f)) tmpBattleStat.MoveSpeed += _itemStat.PlusSpeed; // 이동속도 증가
                if (!Mathf.Approximately(_itemStat.PlusAttackRange, 0.0f)) tmpBattleStat.AttackRange += _itemStat.PlusAttackRange; // 사정거리 증가
                if (!Mathf.Approximately(_itemStat.PlusProjectileSpeed, 0.0f)) tmpBattleStat.ProjectileSpeed += _itemStat.PlusProjectileSpeed; // 투사체 속도 증가
                break;
            default:
                break;
        }
        this.battleStat.AP = this.originalStat.AP + tmpBattleStat.AP;
        this.battleStat.MaxHp = this.originalStat.MaxHp + tmpBattleStat.MaxHp;
        this.battleStat.MoveSpeed = this.originalStat.MoveSpeed + tmpBattleStat.MoveSpeed;
        this.battleStat.AttackRange = this.originalStat.AttackRange + tmpBattleStat.AttackRange;
        this.battleStat.ProjectileSpeed = this.originalStat.ProjectileSpeed + tmpBattleStat.ProjectileSpeed;*/

        // 위처럼 하지 말고 인벤토리에 들어있는 아이템이 바뀔 경우에만 인벤토리 안에 있는 아이템들의 스텟 총합을 더해서 아래의 5줄짜리 코드를 적용함
        this.battleStat.AP = this.originalStat.AP + _itemStat.AP;
        this.battleStat.MaxHp = this.originalStat.MaxHp + _itemStat.MaxHp;
        this.battleStat.MoveSpeed = this.originalStat.MoveSpeed + _itemStat.MoveSpeed;
        this.battleStat.AttackRange = this.originalStat.AttackRange + _itemStat.AttackRange;
        this.battleStat.ProjectileSpeed = this.originalStat.ProjectileSpeed + _itemStat.ProjectileSpeed;
        

        //Initialize();
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

    protected override void OnDead()
    {
        

        base.OnDead();

        StartCoroutine(ChangeAlpha());
  
    }

    public IEnumerator ChangeAlpha()
    {
        yield return new WaitForSeconds(2.0f);
        DeathIMG.GetComponent<CanvasGroup>().alpha = 1.0f;
        
       
             
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
