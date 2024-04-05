using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portalllll : MonoBehaviour
{
    public LayerMask Player;
    public float raycastDistance = 100;
    private FadeInOut FadeManager;
    private Vector3 savedPosition;

    void Start()
    {
        FadeManager = FindObjectOfType<FadeInOut>();

    }
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, Player))
        {
            savedPosition = transform.position;
            FadeManager.FadeOutAndLoadScene("SampleScene");
        }
    }
}