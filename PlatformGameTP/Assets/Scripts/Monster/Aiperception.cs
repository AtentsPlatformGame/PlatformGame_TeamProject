using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Aiperception : MonoBehaviour
{
    public LayerMask enemyMask;
    [SerializeField] Transform myTarget;
    public UnityEvent<Transform> findEnemyAct;
    public UnityEvent lostEnemyAct;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if ((1 << collision.gameObject.layer & enemyMask) != 0)
        {
            if (myTarget == null)
            {
                myTarget = collision.transform;
                findEnemyAct?.Invoke(myTarget);
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if ((1 << collision.gameObject.layer & enemyMask) != 0)
        {
            if (myTarget == collision.transform)
            {
                myTarget = null;
                lostEnemyAct?.Invoke();
            }
        }
    }
}
