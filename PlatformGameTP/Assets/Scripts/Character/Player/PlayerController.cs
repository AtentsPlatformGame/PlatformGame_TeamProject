using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : BattleSystem
{

    [SerializeField] float moveSpeed = 4.0f;
    [SerializeField] float rotSpeed = 1.0f;
    [SerializeField] float jumpForce = 2.0f;
    [SerializeField] float jumpCharge = 1.0f;
    [SerializeField] Vector2 rotYRange = new Vector2(0.0f, 180.0f);
    public GameObject orgFireball;
    public LayerMask groundMask;
    public Transform leftAttackPoint;
    public Transform rightAttackPoint;
    public UnityEvent<int> switchTrackedOffset;
    public bool isSpellReady = false;
    float curRotY;
    int ap;
    bool isGround;
    float attackDeltaTime = 0.0f;
    Rigidbody rigid;
    Fireball fireBall;
    Coroutine attackDelay;
    // Start is called before the first frame update
    private void Awake()
    {
        Initialize();
    }
    void Start()
    {
        curRotY = transform.localRotation.eulerAngles.y;
        rigid = this.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAlive())
        {
            IsGround();
            TryJump();
            Rotate();
            Move(); // 회전과 동시에 움직이기
                    //if (canMove) Move(); // 회전이 끝나면 움직이기
        }
    }


    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        Vector3 deltaPos = transform.forward * x * Time.deltaTime * moveSpeed;

        if (!Mathf.Approximately(x,0.0f) && Input.GetKeyDown(KeyCode.LeftShift)) // 텔레포트, 추후 쿨타임 추가 예정
        {
            deltaPos += deltaPos.normalized * 1.5f;
            if (Physics.Raycast(new Ray(transform.position + new Vector3(0.0f, 0.5f, 0.0f), transform.forward), out RaycastHit hit,
                1.5f, groundMask))
            {
                Debug.Log("벽에 막힘");
                deltaPos = deltaPos.normalized * hit.distance;
            }
        }
        transform.Translate(deltaPos); // 앞뒤 이동.
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        myAnim.SetFloat("Speed", Mathf.Abs(x));

    }

    void Rotate()
    {
        /*
        1. 회전과 동시에 움직이게 하기 : 회전하는걸 보여주는 라인 열어놓고 아래 긴 조건문 닫기
                                  -> Update에서 적절한 Move사용
        2. 회전 안보여주기. : 곧바로 방향 전환 라인 열어놓고 아래 긴 조건문 닫고 Update에서 적절한 Move사용
        3. 회전 후 이동하기 : 회전하는걸 보여줌 < 라인 열어놓고 조건문 열고 Update에서 적절한 Move사용
        */
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // 오른쪽으로 회전, +
        {
            //curRotY -= Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime; // 회전하는걸 보여줌
            curRotY = 0.0f; // 곧바로 방향 전환
            switchTrackedOffset?.Invoke(1); // 
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // 왼쪽으로 회전, -
        {
            //curRotY += -1 * Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime; // 회전하는걸 보여줌
            curRotY = 180.0f; // 곧바로 방향 전환
            switchTrackedOffset?.Invoke(-1);
        }
        curRotY = Mathf.Clamp(curRotY, rotYRange.x, rotYRange.y); // rotYRange안에 값으로 제한됨
        transform.localRotation = Quaternion.Euler(0, curRotY, 0);

        /*
        // 아래 조건은 한쪽 방향으로 회전이 끝났을때 움직일수 있도록 만든 조건문
        if (Mathf.Approximately(curRotY, rotYRange.x) || Mathf.Approximately(curRotY, rotYRange.y))
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
        */
    }

    void IsGround()
    {

        isGround = Physics.Raycast(transform.position + new Vector3(0.0f, 1.0f, 0.0f), Vector3.down, 1.1f, groundMask);
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), Vector3.down, Color.blue);

        myAnim.SetBool("IsGround", isGround);
        if (isGround)
        {
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
        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            myAnim.SetTrigger("Jumping");
        }
    }

    void Jump()
    {
        /*if (jumpCharge >= 2.0f) jumpCharge = 2.0f;
        rigid.AddForce(transform.up * jumpForce * jumpCharge, ForceMode.Impulse);
        jumpCharge = 1.0f;*/
        rigid.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    // 이 아래부턴 나중에 스크립트 분리할 수도 있음
    protected void Attack() // 공격 함수, 정면을 정확히 바라볼때만 공격 가능
    {
        if(Mathf.Approximately(attackDeltaTime, 0.0f)) // 쿨타임 계산, 마음에 안드는데 일단 후순위
        {
            if(attackDelay != null)
            {
                StopCoroutine(attackDelay);
            }
            attackDelay = StartCoroutine(CoolingAttack());
        }
        else
        {
            return;
        }

        float angle_F = Vector3.Angle(transform.forward, Vector3.forward); // -> 방향을 볼때 0, 반대는 180
        float angle_B = Vector3.Angle(transform.forward, Vector3.back); // -> 방향을 볼때 0, 반대는 180
        Debug.Log(angle_F);
        Debug.Log(angle_B);
        if (Mathf.Approximately(angle_F, 0.0f) || Mathf.Approximately(angle_F, 180.0f))
            myAnim.SetTrigger("Attack");
        /*
        Debug.Log(Camera.main.ScreenPointToRay(Input.mousePosition));*/
    }

    public new void OnAttack()
    {
        ap = GetAp();
        // 애니메이션 이벤트
        // 파이어볼(?)이 생성되어 앞으로 발사되는 함수
        GameObject obj = Instantiate(orgFireball, rightAttackPoint);
        obj.transform.SetParent(null);
        obj.GetComponent<Fireball>().SetFireBallAP(ap); // 파이어볼 공격력 설정
        obj.GetComponent<Fireball>().SetAttackRange(GetAttackRange()); // 파이어볼 공격 사거리 결정
        obj.GetComponent<Fireball>().SetProjectileSpeed(GetProjectileSpeed()); // 파이어볼 투사체 속도 결정
    }

    IEnumerator CoolingAttack()
    {
        
        while(!Mathf.Approximately(battleStat.AttackDelay, attackDeltaTime))
        {
            attackDeltaTime += 1f;
            yield return new WaitForSeconds(1f);
        }
        attackDeltaTime = 0f;
    }

    public void ReadyToUseSpell(bool isReady)
    {
        isSpellReady = isReady;
        myAnim.SetBool("IsSpellReady", isReady);
    }

    public void UsingSpell()
    {
        myAnim.SetTrigger("UseSpell");
    }

    public void ResetSpellTrigger()
    {
        myAnim.ResetTrigger("UseSpell");
    }
}
