using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Enemy : BattleSystem
{
    public enum State
    {
        Create, Normal, Roaming, Battle, Death
    }
    public Transform player;
    public float detectionRange = 4f; //몬스터가 플레이어를 감지하는 영역
    public float moveSpeed = 1f; // 몬스터 스피드
    public float attackCooldown = 5f; // 몬스터 공격 딜레이
    public float attackRange = 2f;// 몬스터의 공격 범위
    public float returnSpeed = 2f; // 몬스터가 제자리로 복귀하는 속도
    public float deathDelay = 2f; // 몬스터가 죽어서 사라지는 시간
    public float MonsterHP = 5f; //몬스터 체력
    public Animator myanim;
    public LayerMask groundLayer;
   

    [SerializeField] float curtHP;
    private bool isChasing = false;
    private bool isAttacking = false;
    private Vector3 startPosition;
    private float lastAttackTime = 0f;

    private void Start()
    {
        startPosition = transform.position;
        curtHP = MonsterHP;
        Initialize();
    }
    
    private void Update()
    {
        if (player == null)
            return;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
            myanim.SetBool("Ismoving", true);
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            if (distanceToPlayer < 1.0f) 
            {
                Battle();
                if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
                {
                    // 플레이어가 공격 범위 내에 있고, 공격 쿨다운이 지난 경우
                    Battle();
                }
            }
        }
            if (distanceToPlayer > detectionRange)
            {
                //플레이어가 몬스터의 영역을 벗어나면 몬스터가 시작위치로 복귀
                transform.position = Vector3.MoveTowards(transform.position, startPosition, returnSpeed * Time.deltaTime);

                if (transform.position == startPosition)
                {
                    isChasing = false;
                }
                myanim.SetBool("Ismoving", false);
            }
    }
    private void Battle()
    {
        isAttacking = true;
        myanim.SetTrigger("Attack");
        lastAttackTime = Time.time;
        
    }
    public new void TakeDamage(float dmg)
    {
        
        base.TakeDamage(dmg);
        if(curHp <= 0) // 죽었다.
        {
            // 땅으로 사라진다.
            Debug.Log("죽음 실행");
            Destroy(gameObject, deathDelay);
            DisApear();
           
        }
    }
    public void Die()
    {
        myanim.SetTrigger("Die");
        Destroy(gameObject, deathDelay);
    }
    public void DisApear()
    {
        StartCoroutine(DisApearing(2.0f));
    }
    IEnumerator DisApearing(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);

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
    private void FixedUpdate()
    {
        // 몬스터가 땅의 끝에 있는지 확인
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            if (hit.distance < 0.1f)
            {
                // 땅 끝이 너무 가까우면 위치 조정 필요
                transform.position -= Vector3.forward * Time.deltaTime;
            }
        }
    }
}
