using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnemyMovemente : BattleSystem
{
    public float moveSpeed = 2.0f;
    public float rotSpeed = 360.0f;
    Coroutine move = null;
    Coroutine rotate = null;
    Coroutine attack = null;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
