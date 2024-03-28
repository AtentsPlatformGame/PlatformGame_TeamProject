using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalIn : MonoBehaviour
{
    [Header("골인 레이 오브젝트")] public GameObject goalinpoint;
    // Start is called before the first frame update
    public UnityEvent clearGame;
    public LayerMask goalMask;
    private void Update()
    {
        RaycastHit Tphit;
        if (Physics.Raycast(goalinpoint.transform.position, goalinpoint.transform.forward, out Tphit, Mathf.Infinity, goalMask) ||
            Physics.Raycast(goalinpoint.transform.position + Vector3.forward * 0.5f, goalinpoint.transform.forward, out Tphit, Mathf.Infinity, goalMask) ||
            Physics.Raycast(goalinpoint.transform.position + Vector3.back * 0.5f, goalinpoint.transform.forward, out Tphit, Mathf.Infinity, goalMask))
        {
            clearGame?.Invoke();
            this.gameObject.SetActive(false);
        }
        
    }
}
