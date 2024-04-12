using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class TestPortal1: MonoBehaviour
{
    // 캐릭터의 Transform 컴포넌트
    public LayerMask Player;
    public float raycastDistance = 1000;
    private SceneFadeInOut FadeManager;
    private Vector3 savedPosition;

    void Start()
    {
        FadeManager = FindObjectOfType<SceneFadeInOut>();
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, Player))
        {
            savedPosition = transform.position;
            FadeManager.FadeOutAndLoadScene("Stage1");
        }
    }
}