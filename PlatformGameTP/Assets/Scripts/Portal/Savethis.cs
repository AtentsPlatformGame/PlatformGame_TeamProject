using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savethis : MonoBehaviour
{
    public Transform portalPosition;
    void Start()
    {
        // 첫 번째 씬에서 포탈 위치를 저장
        PlayerPrefs.SetFloat("PortalX", portalPosition.position.x);
        PlayerPrefs.SetFloat("PortalY", portalPosition.position.y);
        PlayerPrefs.SetFloat("PortalZ", portalPosition.position.z);
    }
}
