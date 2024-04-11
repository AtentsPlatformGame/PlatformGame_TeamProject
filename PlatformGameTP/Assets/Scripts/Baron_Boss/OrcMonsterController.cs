using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcMonsterController : EnemyState
{
    public GameObject PatternPillar;
    public GameObject PatternStons;

    public Transform virticalAttackEffect;
    public Transform horizontalAttackEffect;
    public Transform buffEffect;
    public Transform getHitEffect;

    public Transform hitPoint;
    public Transform spawnPoint;

    [SerializeField] bool isPhaseChanged = false;
   
    public int phasecount = 0;
    public Vector3 PatternPos;
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
        PatternPos = new(0.0f, 0.0f, 15.0f);

        PatternPillar.SetActive(false);
        PatternStons.SetActive(false);
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

            if (this.curHP <= (this.battleStat.MaxHp * 0.8) && phasecount == 0)
            {
                this.transform.position = PatternPos;
                myAnim.SetBool("Running", true);
                myAnim.SetBool("Roaming", true);
                myAnim.SetTrigger("PatternP");
                PatternPillar.SetActive(true);
                // 오크의 체력이 80%일때 기둥을 쓰러트리는 패턴
                yield return new WaitForSeconds(1.0f);
                //Destroy(PatternPillar);
                PatternPillar.SetActive(false);
                myAnim.SetBool("Running", false);
                myAnim.SetBool("Roaming", false);
                phasecount = 1;
            }

            if (this.curHP <= (this.battleStat.MaxHp * 0.5) && phasecount == 1)
            {
                this.transform.position = PatternPos;
                myAnim.SetBool("Running", true);
                myAnim.SetBool("Roaming", true);
                myAnim.SetTrigger("Howling");
                // 오크의 체력이 절반이 되었을때 능력치를 버프하는 패턴 
                this.battleStat.AP += 2;
                this.battleStat.MoveSpeed += 1;
                phasecount = 2;
                Debug.Log("공업 이속업");
                yield return new WaitForSeconds(3.0f);
                myAnim.SetBool("Running", false);
                myAnim.SetBool("Roaming", false);
            }

            if (this.curHp <=(this.battleStat.MaxHp * 0.2) && phasecount == 2 )
            {
                this.transform.position = PatternPos;
                myAnim.SetBool("Running", true);
                myAnim.SetBool("Roaming", true);
                myAnim.SetTrigger("PatternS");
                PatternStons.SetActive(true);
                // 오크의 체력이 20%남았을때 낙석 패턴
                yield return new WaitForSeconds(2.0f);
                Destroy(PatternStons);
                myAnim.SetBool("Running", false);
                myAnim.SetBool("Roaming", false);
                phasecount = 3;

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

   
    public void BuffEffect()
    {
        Instantiate(buffEffect, transform.position, Quaternion.identity, null);
    }

    public void GetHitEffect()
    {
        Instantiate(getHitEffect, transform.position, Quaternion.identity, null);
    }

}
