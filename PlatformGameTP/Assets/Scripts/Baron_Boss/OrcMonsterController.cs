using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcMonsterController : EnemyState
{
    public Transform virticalAttackEffect;
    public Transform horizontalAttackEffect;
    public Transform skillAttackEffect;
    public Transform getHitEffect;

    public Transform hitPoint;
    public Transform spawnPoint;

    [SerializeField] bool isPhaseChanged = false;
    [SerializeField] bool isRushChanged = false;
    [SerializeField] bool isRushing = false;
    public int phasecount = 0;
    private float rushTimer = 0f;
    private const float rushDuration = 3f;

    public float rushSpeed = 5f;

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

        startPos = transform.position;
        base.ChangeState(State.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        base.StateProcess();
        if (this.myState == State.Death) return;
        if (!isPhaseChanged && this.curHp <= (this.battleStat.MaxHp * 0.7))
        {
            ChangeState(State.Phase);
            isPhaseChanged = true;
        }
        if (myState != State.Death)
        {
            base.IsGround();
        }


        /*
        if(!isRushChanged && this.curHP <=(this.battleStat.MaxHp * 0.5))
        {
            //ChangeState(State.Rush);
            myAnim.SetTrigger("Howling");
            isRushing = true;
            rushTimer = 0;
            isRushChanged = true;
        }
        if (isRushing)
        {
            rushTimer += Time.deltaTime;

            if(rushTimer >= rushDuration)
            {
                myAnim.SetBool("StartRush", false);
                isRushing = false;
                
                ChangeState(State.Rush);
            }
            else
            {
                myAnim.SetBool("StartRush", true);
                myAnim.SetTrigger("Rush");
                transform.Translate(Vector3.forward * rushSpeed * Time.deltaTime);
                
            }
            return;
    }*/
        
    }


    public new void OnAttack()
    {
        Collider[] list = Physics.OverlapSphere(attackPoint.position, 3.0f, enemyMask);

        foreach (Collider col in list)
        {
            IDamage act = col.GetComponent<IDamage>();
            if (act != null)
            {
                act.TakeDamage(this.battleStat.AP);
            }
        }
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

            if (this.curHP <= (this.battleStat.MaxHp * 0.5) && phasecount == 0)
            {
                myAnim.SetTrigger("Howling");
                yield return new WaitForSeconds(2.0f);
                myAnim.SetTrigger("Rush");
                transform.Translate(Vector3.forward * rushSpeed * Time.deltaTime);
                phasecount = 1;

            }


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

    protected override IEnumerator DisApearing(float delay)
    {
        yield return new WaitForSeconds(delay);

        //Destroy(myHpBar.gameObject);
        float _fillAmount = 0.0f;
        while (_fillAmount < 1.0f)
        {
            _fillAmount = Mathf.Clamp(_fillAmount + Time.deltaTime, 0.0f, 1.0f);
            foreach (Renderer renderer in allRenderer)
            {
                renderer.material.SetFloat("_DissolveAmount", _fillAmount);
            }
            yield return null;
        }
        Destroy(gameObject);
    }
    public void VirticalAttackEffect()
    {
        Instantiate(virticalAttackEffect, hitPoint.transform.position, Quaternion.Euler(-60.0f, 0.0f, -90.0f), null);
    }

    public void HorizontalAttackEffect()
    {
        Instantiate(virticalAttackEffect, hitPoint.transform.position, Quaternion.identity, null);
    }

    public void SkillAttackEffect()
    {
        Instantiate(skillAttackEffect, hitPoint.transform.position, Quaternion.Euler(-100.0f, 0.0f, -140.0f), null);
    }

    public void GetHitEffect()
    {
        Instantiate(getHitEffect, transform.position, Quaternion.identity, null);
    }

}
