using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonWarriorController : EnemyState
{
    public Transform virticalAttackEffect;
    public Transform horizontalAttackEffect;
    public Transform attackPoint;
    public Transform spawnPoint;

    [SerializeField] Transform skeleton;
    [SerializeField] bool isPhaseChanged = false;

    protected override void ChangeState(State s)
    {
        base.ChangeState(s);
        switch (myState)
        {
            case State.Phase:
                StopAllCoroutines();
                myAnim.SetBool("IsRoaming", false);
                myAnim.SetBool("IsRunning", false);
                myAnim.SetTrigger("Spawn");
                StartCoroutine(SpawnSkeleton(spawnPoint));
                break;
            default:
                break;
        }
    }
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
        base.ChangeState(State.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        base.StateProcess();
        if(!isPhaseChanged && this.curHp <= (this.battleStat.MaxHp * 0.5))
        {
            ChangeState(State.Phase);
            isPhaseChanged = true;
        }
        if (myState != State.Death)
        {
            base.IsGround();
        }
    }

    IEnumerator SpawnSkeleton(Transform spawnPoint)
    {
        //GameObject obj = Instantiate(skeleton, spawnPoint.transform.position, Quaternion.identity, spawnPoint) as GameObject;
        Instantiate(skeleton, spawnPoint.transform.position, Quaternion.identity, spawnPoint);
        //skeleton.transform.SetParent(null);
        yield return StartCoroutine(DelayChangeState(State.Normal, 1.5f));
    }

    protected override IEnumerator AttackingTarget(Transform target)
    {
        while (target != null)
        {
            myAnim.SetBool("IsRunning", true);
            int pattern = Random.Range(0, 2);
            Vector3 dir = target.position - transform.position;
            float dist = dir.magnitude - battleStat.AttackRange;
            if (dist < 0.0f) dist = 0.0f;
            float delta;
            if (!myAnim.GetBool("IsAttacking")) battleTime += Time.deltaTime;
            if (Mathf.Approximately(dist, 0.0f))
            {
                myAnim.SetBool("IsRunning", false);
                if (battleTime >= battleStat.AttackDelay)
                {
                    battleTime = 0.0f;
                    if (pattern == 0)
                    {
                        myAnim.SetTrigger("Attack1");
                    }
                    else
                    {
                        myAnim.SetTrigger("Attack2");
                    }
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

    public void VirticalAttackEffect()
    {
        Instantiate(virticalAttackEffect, attackPoint.transform.position, Quaternion.Euler(-60.0f, 0.0f, -90.0f), null);
    }

    public void HorizontalAttackEffect()
    {
        Instantiate(virticalAttackEffect, attackPoint.transform.position, Quaternion.identity, null);
    }
}
