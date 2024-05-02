using InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemGet : MonoBehaviour
{
    [Header("아이템 캔버스")] public GameObject GKeyPopup;

    public bool isTpobject = false;
    public bool isPopup = false;

    public UnityEvent itemget;
    public LayerMask goalMask;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit Tphit;
        if (Physics.Raycast(transform.position, transform.forward, out Tphit, Mathf.Infinity, goalMask) ||
            Physics.Raycast(transform.position + Vector3.forward * 0.5f, transform.forward, out Tphit, Mathf.Infinity, goalMask) ||
            Physics.Raycast(transform.position + Vector3.back * 0.5f, transform.forward, out Tphit, Mathf.Infinity, goalMask))
        {
            itemget?.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
