using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : BattleSystem
{
    [Header("몬스터 공격 사운드")] public AudioClip monsterAttackClip;
    public float rotSpeed = 360.0f;
    protected Vector3 dir;

    Coroutine move = null;
    protected Coroutine rotate = null;
    Coroutine attack = null;
    
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Picking>().clickAct += MoveToPos;
        //GetComponent<Picking>().clickAct.AddListener(MoveToPos);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void StopAttack()
    {
        if (attack != null) StopCoroutine(attack);
        attack = null;
        myAnim.SetBool("IsRunning", false);
    }

    protected void AttackTarget(Transform target)
    {
        
        StopAllCoroutines();
        if (myAnim.GetBool("IsRoaming")) myAnim.SetBool("IsRoaming", false);
        attack = StartCoroutine(AttackingTarget(target));
    }

    protected virtual IEnumerator AttackingTarget(Transform target)
    {
        while (target != null)
        {
            if (!target.GetComponent<PlayerController>().isAlive())
            {
                target = null;
                break;
            }
            myAnim.SetBool("IsRunning", true);
            Vector3 dir = target.position - transform.position;
            float dist = dir.magnitude - battleStat.AttackRange;
            if (dist < 0.0001f) dist = 0.0f;
            float delta;
            if (!myAnim.GetBool("IsAttacking")) battleTime += Time.deltaTime;
            if (Mathf.Approximately(dist, 0.0f))
            {
                myAnim.SetBool("IsRunning", false);
                if (battleTime >= battleStat.AttackDelay)
                {
                    battleTime = 0.0f;
                    myAnim.SetTrigger("Attack");
                    if(myAudioSource != null)
                    {
                        myAudioSource.clip = monsterAttackClip;
                        myAudioSource.Play();
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

    public void MoveToPos(Vector3 target)
    {
        MoveToPos(target, null, null);
    }

    public void MoveToPos(Vector3 target, UnityAction doneAct)
    {
        MoveToPos(target, doneAct, null);
    }
    public void MoveToPos(Vector3 target, UnityAction doneAct, UnityAction animAct)
    {
        if (move != null)
        {
            StopCoroutine(move);
            move = null;
        }
        if (attack != null)
        {
            StopCoroutine(attack);
            attack = null;
        }
        move = StartCoroutine(MovingToPos(target, doneAct, animAct));
    }

    protected IEnumerator MovingToPos(Vector3 target, UnityAction doneAct, UnityAction animAct)
    {
        //Debug.Log($"MovingToPos 실행 : target : {target}");
        Vector3 dir = target - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        if (rotate != null) StopCoroutine(rotate);
        rotate = StartCoroutine(Rotating(dir));

        if (animAct == null)
        {
            myAnim.SetBool("IsRoaming", true);
        }
        else
        {
            animAct();
        }

        while (!Mathf.Approximately(dist, 0.0f))
        {
            float delta = battleStat.MoveSpeed * Time.deltaTime;
            if (delta > dist) delta = dist;
            dist -= delta;
            transform.Translate(dir * delta, Space.World);
            yield return null;
        }

        if (animAct == null)
        {
            myAnim.SetBool("IsRoaming", false);
        }
        else
        {
            animAct();
        }

        doneAct?.Invoke();
    }

    protected IEnumerator Rotating(Vector3 dir)
    {
        //float d = Vector3.Dot(transform.forward, dir);
        //float r = Mathf.Acos(d);
        // y : 180 = r : pi;
        // y / 180 = r / pi;
        // y = (r/pi) * 180;
        //float angle = r * Mathf.Rad2Deg;//(r / Mathf.PI) * 180.0f;
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

}
