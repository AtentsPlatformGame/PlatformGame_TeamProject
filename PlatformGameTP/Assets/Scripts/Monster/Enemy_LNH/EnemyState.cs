using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : EnemyMovement
{
    public enum State
    {
        Create, Normal, Roaming, Battle, Death
    }
    public State myState = State.Create;

    public Transform hpViewPos;

    Vector3 startPos;
    float playTime = 0.0f;

    //HpBar myHpBar;
    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Normal:
                //2~5초 사이의 대기 시간을 가지고 로밍을 한다.
                playTime = Random.Range(2.0f, 5.0f);
                StartCoroutine(DelayChangeState(State.Roaming, playTime));
                break;
            case State.Roaming:
                MoveToPos(GetRndPos(), () => ChangeState(State.Normal));
                break;
            case State.Battle:
                AttackTarget(myTarget);
                break;
            case State.Death:
                StopAllCoroutines();
                break;
        }
    }

    Vector3 GetRndPos()
    {
        Vector3 dir = Vector3.forward;
        dir = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0) * dir;
        dir *= Random.Range(0.0f, 3.0f);
        return startPos + dir;
    }

    void StateProcess()
    {
        switch (myState)
        {
            case State.Normal:
                /*
                playTime -= Time.deltaTime;
                if(playTime <= 0.0f)
                {
                    ChangeState(State.Roaming);
                }
                */
                break;
            case State.Battle:
                break;
        }
    }

    IEnumerator DelayChangeState(State s, float t)
    {
        yield return new WaitForSeconds(t);
        ChangeState(s);
    }

    //자신의 반경 3미터 이내의 랜덤한 위치를 계속 로밍 한다.
    // Start is called before the first frame update
    void Start()
    {
        base.Initialize();
        //GameObject.Find("HpBars");
        /*GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/UI/HpStatus"),
         SceneData.Instance.hpBarsTransform);
        myHpBar = obj.GetComponent<HpBar>();
        myHpBar.myTarget = hpViewPos;
        base.changeHpAct.AddListener(myHpBar.ChangeHpSlider);*/

        startPos = transform.position;
        ChangeState(State.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }

    public void FindTarget(Transform target)
    {
        if (myState == State.Death) return;
        myTarget = target;
        ChangeState(State.Battle);
    }

    public void LostTarget()
    {
        if (myState == State.Death) return;
        myTarget = null;
        StopAttack();
        ChangeState(State.Normal);
    }

    protected override void OnDead()
    {
        base.OnDead();
        ChangeState(State.Death);
    }

    public void DisApear()
    {
        StartCoroutine(DisApearing(2.0f));
    }

    IEnumerator DisApearing(float delay)
    {
        yield return new WaitForSeconds(delay);

        //Destroy(myHpBar.gameObject);

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
}
