using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fireball : MonoBehaviour
{
    //public GameObject orgEffect;
    public LayerMask crashMask;
    public float projectileSpeed = 10.0f;
    public UnityEvent getApAct;
    bool isFire = false;
    int dmg = 1;
    float attackRange;
    
    Vector3 oldPos;
    Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        spawnPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(oldPos, (transform.position - oldPos).normalized);
        if (Physics.Raycast(ray, out RaycastHit hit, (transform.position - oldPos).magnitude, crashMask))// 맞았다.
        {
            Destroy(gameObject);
            //Effect
            //Instantiate(orgEffect, hit.point, Quaternion.identity);
            //hit.point;
            BattleSystem bs = hit.transform.GetComponent<BattleSystem>();
            if(bs != null)
            {
                //float dmg = getApAct?.Invoke();
                bs.TakeDamage(dmg);
            }

        }
        oldPos = transform.position;
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);

        // 사거리를 벗어나면 사라지게
        Vector3 dir = transform.position - spawnPos;
        float dist = dir.magnitude;

        if(dist >= attackRange)
        {
            Destroy(gameObject);
        }
    }
    #region get, set
    public void SetFireBallAP(int _dmg)
    {
        this.dmg = _dmg;
    }

    public void SetAttackRange(float _range)
    {
        this.attackRange = _range;
    }

    public void SetProjectileSpeed(float _pSpeed)
    {
        this.projectileSpeed = _pSpeed;
    }
    #endregion

}
