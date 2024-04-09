using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonWarriorController : EnemyState
{
    public Transform virticalAttackEffect;
    public Transform horizontalAttackEffect;

    public Transform slashPoint;
    public Transform spawnPoint;

    [SerializeField] Transform skeleton;
    [SerializeField] bool isPhaseChanged = false;
    [SerializeField] Transform LastBoss;

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
        if (this.myState == State.Death) return;
        if (!isPhaseChanged && this.curHP <= (this.battleStat.MaxHp * 0.5))
        {
            ChangeState(State.Phase);
            isPhaseChanged = true;
        }
        if (myState != State.Death)
        {
            base.IsGround();
        }
    }
    #region ���̷��� ��ȯ ����
    IEnumerator SpawnSkeleton(Transform spawnPoint)
    {
        Transform obj;
        obj = Instantiate(skeleton, spawnPoint.transform.position, Quaternion.identity, spawnPoint);
        //Instantiate(skeleton, spawnPoint.transform.position, Quaternion.identity, spawnPoint);
        obj.gameObject.transform.SetParent(null);
        yield return StartCoroutine(DelayChangeState(State.Normal, 1.5f));
    }
    #endregion
    #region ��������, ����Ʈ
    public new void OnAttack()
    {
        Collider[] list = Physics.OverlapSphere(attackPoint.position, 5.0f, enemyMask);

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

    public void VirticalAttackEffect()
    {
        Transform vtEff;
        vtEff = Instantiate(virticalAttackEffect, slashPoint.transform.position, Quaternion.Euler(-60.0f, transform.rotation.eulerAngles.y, -90.0f), null);
        vtEff.localScale = new Vector3(2f, 2f, 2f);
    }

    public void HorizontalAttackEffect()
    {
        //Instantiate(virticalAttackEffect, slashPoint.transform.position, Quaternion.identity, null);
        Transform hzEff;
        hzEff = Instantiate(virticalAttackEffect, slashPoint.transform.position, Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y - 100.0f, 0.0f), null);
        hzEff.localScale = new Vector3(2f, 2f, 2f);
    }
    #endregion

    #region �������
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

        yield return StartCoroutine(SpawingLastBoss());
        Debug.Log("��ȯ����");
        Destroy(gameObject);
        
    }

    public void SpawnLastBoss()
    {
        StartCoroutine(SpawingLastBoss());
    }

    IEnumerator SpawingLastBoss()
    {
        yield return new WaitForSeconds(1f);
        if (LastBoss != null) LastBoss.gameObject.SetActive(true);
    }
    #endregion
}
