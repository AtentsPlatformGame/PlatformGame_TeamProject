using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NagaWizard : EnemyState
{
    public Transform virticalAttackEffect;
    public Transform horizontalAttackEffect;

    public Transform slashPoint;
    public Transform spawnPoint;
    public GameObject TsunamiLeft;
    public GameObject TsunamiRight;
    public GameObject BeforeTsunami;
    public GameObject WarningSign;
    public int PhaseCount = 0;
    public GameObject GreetingBoss;
    static int greetingCounts = 0;

    [SerializeField] bool isPhaseChanged = false;

    public Vector3 SpecialPatternPos;
    // Start is called before the first frame update
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
        TsunamiLeft.SetActive(false);
        TsunamiRight.SetActive(false);
        BeforeTsunami.SetActive(false);
        WarningSign.SetActive(false);
        GreetingBoss.SetActive(false) ;
        SpecialPatternPos = new (0.0f, 2.5f, 41.0f);
        startPos = transform.position;
        base.ChangeState(State.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        base.StateProcess();
        if (!isPhaseChanged && this.curHp <= (this.battleStat.MaxHp * 0.5))
        {
            ChangeState(State.Phase);
            isPhaseChanged = true;
        }
        if (myState != State.Death)
        {
            base.IsGround();
        }
        if (myState == State.Battle && greetingCounts == 0) 
        {
            GreetingBoss.SetActive(true);
            Invoke("TurnOffGreetings", 3.0f);
        
        }

    
    }

 

    public new void OnAttack()
    {
        Collider[] list = Physics.OverlapSphere(attackPoint.position, 1.0f, enemyMask);

        foreach (Collider col in list)
        {
            IDamage act = col.GetComponent<IDamage>();
            if (act != null)
            {
                act.TakeDamage(30.0f);
            }
        }
    }
    protected override IEnumerator AttackingTarget(Transform target)
    {
        while (target != null)
        {
            Debug.Log("공격을한다");
            myAnim.SetBool("IsRunning", true);
            int pattern = Random.Range(0, 5);

            Vector3 dir = target.position - transform.position;
            float dist = dir.magnitude - battleStat.AttackRange;
            if (dist < 0.0001f) dist = 0.0f;
            float delta;
            if (!myAnim.GetBool("IsAttacking")) battleTime += Time.deltaTime;
            Debug.Log(dist);
            

            //특수패턴
            if (this.curHP < 3 && PhaseCount == 0)
            {
                Debug.Log("특수패턴 발동");
               
                this.transform.position = SpecialPatternPos;
                yield return new WaitForSeconds(1.0f);
                myAnim.SetTrigger("SpecialPattern");
                WarningSign.SetActive(true);
                BeforeTsunami.SetActive(true);

                yield return new WaitForSeconds(5.0f);
                BeforeTsunami.SetActive(false);
                WarningSign.SetActive(false);
                TsunamiLeft.SetActive(true);
                TsunamiRight.SetActive(true);
                yield return new WaitForSeconds(5.0f);

                TsunamiLeft.SetActive(false);
                TsunamiRight.SetActive(false);
                PhaseCount = 1;
            }
           

            if (Mathf.Approximately(dist, 0.0f))
            {
                myAnim.SetBool("IsRunning", false);
                if (battleTime >= battleStat.AttackDelay)
                {
                    TsunamiLeft.SetActive(false);
                    TsunamiRight.SetActive(false);
                    battleTime = 0.0f;
                    if (pattern == 0 || pattern == 1 || pattern == 2 )
                    {
                        Debug.Log("일반1번");
                        myAnim.SetTrigger("Attack1");

                    }
                    else if(pattern == 3)
                    {

                    }
                    else if(pattern == 4)
                    {
                        Debug.Log("일반2번");
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
        Instantiate(virticalAttackEffect, slashPoint.transform.position, Quaternion.Euler(270.0f, transform.rotation.eulerAngles.y, 0.0f), null);
    }

    public void HorizontalAttackEffect()
    {
        Instantiate(horizontalAttackEffect, slashPoint.transform.position, Quaternion.Euler(270.0f, transform.rotation.eulerAngles.y, 0.0f), null);
    }

    public void TurnOffGreetings()
    {
        GreetingBoss.SetActive(false);
        greetingCounts = 1;
    }

}

