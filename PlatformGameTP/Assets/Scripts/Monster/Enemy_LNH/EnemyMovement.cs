using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : BattleSystem
{
    public float moveSpeed = 2.0f;
    public float rotSpeed = 360.0f;
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
        myAnim.SetBool("IsMoving", false);
    }

    protected void AttackTarget(Transform target)
    {
        StopAllCoroutines();
        attack = StartCoroutine(AttackingTarget(target));
    }

    IEnumerator AttackingTarget(Transform target)
    {
        while (target != null)
        {
            myAnim.SetBool("IsMoving", true);
            Vector3 dir = target.position - transform.position;
            float dist = dir.magnitude - battleStat.AttackRange;
            if (dist < 0.0f) dist = 0.0f;
            float delta;
            if (!myAnim.GetBool("IsAttacking")) battleTime += Time.deltaTime;
            if (Mathf.Approximately(dist, 0.0f))
            {
                myAnim.SetBool("IsMoving", false);
                if (battleTime >= battleStat.AttackDelay)
                {
                    battleTime = 0.0f;
                    myAnim.SetTrigger("Attack");
                }
            }
            else
            {
                dir.Normalize();
                delta = moveSpeed * Time.deltaTime;
                if (delta > dist) delta = dist;
                transform.Translate(dir * delta, Space.World);
                if (Mathf.Approximately(dist, 0.0f))
                {
                    myAnim.SetBool("IsMoving", false);
                }
            }
            float angle = Vector3.Angle(transform.forward, dir);
            float rotDir = Vector3.Dot(transform.right, dir) < 0.0f ? -1.0f : 1.0f;
            delta = rotSpeed * Time.deltaTime;
            if (delta > angle) delta = angle;
            transform.Rotate(Vector3.up * rotDir * delta);

            yield return null;
        }
        myAnim.SetBool("IsMoving", false);
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
        Vector3 dir = target - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        if (rotate != null) StopCoroutine(rotate);
        rotate = StartCoroutine(Rotating(dir));

        if (animAct == null)
        {
            myAnim.SetBool("IsMoving", true);
        }
        else
        {
            animAct();
        }

        while (!Mathf.Approximately(dist, 0.0f))
        {
            float delta = moveSpeed * Time.deltaTime;
            if (delta > dist) delta = dist;
            dist -= delta;
            transform.Translate(dir * delta, Space.World);
            yield return null;
        }

        if (animAct == null)
        {
            myAnim.SetBool("IsMoving", false);
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
