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

    public LayerMask groundMask;
    protected Vector3 dir;
    protected int jumpCount = 0;
    protected float myPlayTime;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected new void Initialize()
    {
        curHp = battleStat.MaxHp; // BattleSystem 수정 부분
    }
    protected void CrashEnter(Collision other)
    {
        if ((1 << other.gameObject.layer & groundMask) != 0)
        {
            Vector3 rayDir = Vector3.zero;
            if (dir.x > 0.0f)
            {
                rayDir = Vector3.back;
            }
            else if (dir.x < 0.0f)
            {
                rayDir = Vector3.forward;
            }

            if (CheckWall(rayDir)) return;

            
            jumpCount = 0;
        }
    }


    protected void CrashExit(Collision other)
    {
        if ((1 << other.gameObject.layer & groundMask) != 0)
        {
            Vector3 rayDir = Vector3.zero;
            if (dir.x > 0.0f)
            {
                rayDir = Vector3.forward;
            }
            else if (dir.x < 0.0f)
            {
                rayDir = Vector3.back;
            }
            if (CheckWall(rayDir)) return;

            
        }
    }
    bool CheckWall(Vector3 rayDir)
    {
        for (int i = 0; i < 3; ++i)
        {
            RaycastHit hit;
                if(Physics.Raycast((Vector3)transform.position+Vector3.up * 1.0f
                    * (float)i, rayDir, out hit, 1.0f+ 0.1f,groundMask))
            {
                return true;
            }
        }
        return false;
    }
    protected void UpdatePosition()
    {
        if (!myAnim.GetBool("IsAttacking"))
            transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);
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

    protected void UpdateAnimState()
    {
        if (Mathf.Approximately(dir.x, 0.0f))
        {
            myAnim.SetBool("IsMoving", false);
        }
        else if (dir.x > 0.0f)
        {
            (allRenderer[0] as SpriteRenderer).flipX = false;
            myAnim.SetBool("IsMoving", true);
        }
        else if (dir.x < 0.0f)
        {
            (allRenderer[0] as SpriteRenderer).flipX = true;
            myAnim.SetBool("IsMoving", true);
        }
    }
}
