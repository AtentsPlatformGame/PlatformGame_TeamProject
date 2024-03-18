using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum State
    {
        Create, Normal, Battle, Death
    }

    public float moveSpeed = 5f;
    public float raycastDistance = 1f;
    public LayerMask groundMask;
    public Transform target;

    private RaycastHit2D hitLeft;
    private RaycastHit2D hitRight;
    private Vector2 moveDirection;

    void Start()
    {
        //플레이어를 타겟으로 설정
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }




    void Update()
    {
        // 몬스터가 플레이어를 추적하도록 이동 방향을 결정
         MoveDirection();

        // 이동
        transform.Translate(moveDirection *moveSpeed* Time.deltaTime);
    }

    void MoveDirection()
    {
        moveDirection = (target.position - transform.position).normalized;
        // 왼쪽 방향으로 Ray
        hitLeft = Physics2D.Raycast(transform.position, Vector2.left, raycastDistance, groundMask);
        // 오른쪽 방향으로 Ray
        hitRight = Physics2D.Raycast(transform.position, Vector2.right, raycastDistance, groundMask);

        // Ray에 맞은 여부에 따라 이동 방향을 결정함
        if (hitLeft.collider == null || hitRight.collider == null)
        {
            // 한 쪽에 땅이 없으면 반대쪽으로 이동함
            if (hitLeft.collider == null)
                moveDirection = Vector2.right;
            else
                moveDirection = Vector2.left;
        }
       
    }
    
    
}
