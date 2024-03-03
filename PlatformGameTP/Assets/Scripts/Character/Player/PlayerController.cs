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
    float curRotY;
    int ap;
    bool isGround;
    Rigidbody rigid;
    Fireball fireBall;
    // Start is called before the first frame update
    void Start()
    {
        curRotY = transform.localRotation.eulerAngles.y;
        rigid = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGround();
        TryJump();
        Rotate();
        Move();
    }


    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        transform.Translate(transform.forward * x * Time.deltaTime * moveSpeed); // 앞뒤 이동.
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        myAnim.SetFloat("Speed", Mathf.Abs(x));

    }

    void Rotate()
    {
        if (Input.GetKey(KeyCode.D)) // 오른쪽으로 회전, +
        {
            curRotY -= Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A)) // 왼쪽으로 회전, -
        {
            curRotY += -1 * Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        }
        curRotY = Mathf.Clamp(curRotY, rotYRange.x, rotYRange.y); // rotYRange안에 값으로 제한됨
        transform.localRotation = Quaternion.Euler(0, curRotY, 0);
    }

    void IsGround()
    {
        isGround = Physics.Raycast(transform.position + new Vector3(0, 1, 0), Vector3.down, 1.0f, groundMask);
        myAnim.SetBool("IsGround", isGround);
        if (isGround)
        {
            Debug.Log("hit");
        }
    }

    void TryJump()
    {
        if (isGround && Input.GetKey(KeyCode.Space))
        {
            jumpCharge += Time.deltaTime;
        }

        if (isGround && Input.GetKeyUp(KeyCode.Space))
        {
            Jump();
            myAnim.SetTrigger("Jumping");
        }
        UnityEngine.Debug.Log(isGround);
    }

    void Jump()
    {
        if (jumpCharge >= 2.0f) jumpCharge = 2.0f;
        rigid.AddForce(transform.up * jumpForce * jumpCharge, ForceMode.Impulse);
        jumpCharge = 1.0f;
    }

    protected void Attack() // 공격 함수, 정면을 정확히 바라볼때만 공격 가능
    {
        float angle_F = Vector3.Angle(transform.forward, Vector3.forward); // -> 방향을 볼때 0, 반대는 180
        float angle_B = Vector3.Angle(transform.forward, Vector3.back); // -> 방향을 볼때 0, 반대는 180
        Debug.Log(angle_F);
        Debug.Log(angle_B);
        if (Mathf.Approximately(angle_F,0.0f) || Mathf.Approximately(angle_F, 180.0f))
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
}
