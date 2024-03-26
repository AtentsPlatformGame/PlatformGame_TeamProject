using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public LayerMask TP;
    [SerializeField] GameObject Canvas;
    public GameObject GKeyPopup;

    public bool isTpobject = false;
    public bool isPopup = false;

    // Start is called before the first frame update
    void Start()
    {
        GKeyPopup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit Tphit;
        if (Physics.Raycast(transform.position, transform.forward,out Tphit, Mathf.Infinity, TP))
        {
            isTpobject = true;
            GKeyPopup.SetActive(true);
        }
        else
        {
            isTpobject = false;
            GKeyPopup.SetActive(false);
        }
        if (isTpobject && Input.GetKeyDown(KeyCode.G))
        {
            if (isPopup)
            {
                OutTp();
            }
            else
            {
                InTp();
            }
        }
    }
    void InTp()
    {
        Canvas.SetActive(true);
        GKeyPopup.SetActive(false);
        isPopup = true;
    }

    void OutTp()
    {
        Canvas.SetActive(false);
        isPopup = false;
    }

}
