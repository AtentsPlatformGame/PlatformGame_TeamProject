using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : BattleSystem
{
    public float moveSpeed = 2.0f;
    public float rotSpeed = 1.0f;
    public float jumpForce = 2.0f;
    public float jumpCharge = 1.0f;
    public Vector2 rotYRange = new Vector2(0.0f, 180.0f);
    public LayerMask groundMask;
    float curRotY;
    bool isGround;
    Rigidbody rigid;
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
        isGround = Physics.Raycast(transform.position + new Vector3(0,1,0), Vector3.down, 1.0f, groundMask);
        myAnim.SetBool("IsGround", isGround);
        if (isGround)
        {
            Debug.Log("hit");
        }
    }

    void TryJump()
    {
        if(isGround && Input.GetKey(KeyCode.Space))
        {
            jumpCharge += Time.deltaTime;
        }

        if (isGround && Input.GetKeyUp(KeyCode.Space))
        {
            Jump();
            myAnim.SetTrigger("Jumping");
        }
    }

    void Jump()
    {
        if (jumpCharge >= 2.0f) jumpCharge = 2.0f;
        rigid.AddForce(transform.up * jumpForce * jumpCharge, ForceMode.Impulse);
        jumpCharge = 1.0f;
    }
}
