using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CountdownRay : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent countdownend;
    public LayerMask goalMask;
    private void Update()
    {
        RaycastHit Tphit;
        if (Physics.Raycast(transform.position, transform.forward, out Tphit, Mathf.Infinity, goalMask) ||
            Physics.Raycast(transform.position + Vector3.forward * 0.5f, transform.forward, out Tphit, Mathf.Infinity, goalMask) ||
            Physics.Raycast(transform.position + Vector3.back * 0.5f, transform.forward, out Tphit, Mathf.Infinity, goalMask))
        {
            countdownend?.Invoke();
            this.gameObject.SetActive(false);
        }

    }
}
