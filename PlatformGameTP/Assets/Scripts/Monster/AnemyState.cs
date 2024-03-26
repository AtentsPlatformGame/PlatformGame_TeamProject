using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anemy : AnemyMovemente
{
    public enum State
    {
        Create, Normal, Missing ,Roaming, Battle, Death
    }
    public State myState = State.Create;
    public LayerMask groundMask;
    protected Vector3 dir;

    [SerializeField] Vector3 leftLimitPos;
    [SerializeField] Vector3 rightLimitPos;
    Vector3 limitPos;
    Vector3 startPos;
    float playTime = 0.0f;

    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Normal:
                playTime = Random.Range(1.0f, 4.0f);
                StartCoroutine(DelayChangeState(State.Roaming, playTime));
                break;
            case State.Roaming:
                break;
                
        }

        Vector3 GetRndPos()
        {
            Vector3 dir = Vector3.forward;
            dir = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0) * dir;
            dir *= Random.Range(0.0f, 3.0f);
            return startPos + dir;
        }
        IEnumerator DelayChangeState(State s, float t)
        {
            yield return new WaitForSeconds(t);
            ChangeState(s);
        }

    }
    void Start()
    {
        base.Initialize();
        startPos = transform.position;
        ChangeState(State.Normal);
    }

   
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        // 몬스터가 땅의 끝에 있는지 확인
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, groundMask))
        {
            if (hit.distance < 0.1f)
            {
                transform.position -= Vector3.forward * Time.deltaTime;
            }
                
        }
    }
}
