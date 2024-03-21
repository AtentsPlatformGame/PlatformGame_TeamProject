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
    public float moveSpeed = 3f; // 몬스터 스피드
    public float attackCooldown = 5f; // 몬스터 공격 딜레이
    public float attackRange = 4f;// 몬스터의 공격 범위
    public float returnSpeed = 2f; // 몬스터가 제자리로 복귀하는 속도
    public float deathDelay = 2f; // 몬스터가 죽어서 사라지는 시간
    public float MonsterHP = 5f; //몬스터 체력
    public float rotationSpeed = 360f;
    public Animator myanim;
    public LayerMask groundLayer;
   
   

    [SerializeField] float curtHP;
    private bool isChasing = false;
    private bool isAttacking = false;
    private Vector3 startPosition;
    private float lastAttackTime = 0f;
    [SerializeField]private bool isDead = false; //몬스터가 죽었는지 여부를 나타내는 변수

    private void Start()
    {
        startPosition = transform.position;
        curtHP = MonsterHP;
        Initialize();
    }
    
    private void Update()
    {
        if (isDead || player == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if ( distanceToPlayer <= detectionRange)
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
        else

        {
            // 플레이어가 몬스터의 영역을 벗어나면 몬스터가 시작 위치로 복귀
            Vector3 directionToStart = (startPosition - transform.position).normalized;

            // 몬스터가 시작 위치를 바라보도록 회전
            Quaternion targetRotation = Quaternion.LookRotation(directionToStart);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // 회전이 완료되면 제자리로 이동
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPosition, returnSpeed * Time.deltaTime);
            }
            if (Vector3.Distance(transform.position, startPosition) < 0.1f)
            {
                isChasing = false;
                myanim.SetBool("Ismoving", false);
            }

        }
    }
    private void Battle()
    {
        if (curHp > 0)
        {
            isAttacking = true;
            myanim.SetTrigger("Attack");
            lastAttackTime = Time.time;
        }
        else
        {
            // 몬스터가 이미 죽었으면 추적을 멈춤
            isDead = true;
            isChasing = false;
        }

    }
    public new void TakeDamage(float dmg)
    {
        
        base.TakeDamage(dmg);
        if(curHp <= 0 && !isDead) // 죽음
        {
            // 땅으로 사라진다.
            Debug.Log("죽음 실행");
            Die();
        }
    }
    public void Die()
    {
        myanim.SetTrigger("Die");
        //사라지기전에 추적을 멈춤
        isDead = true;
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
