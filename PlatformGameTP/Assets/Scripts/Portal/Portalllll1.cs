using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portalllll1 : MonoBehaviour
{
    public LayerMask Player;
    public float raycastDistance = 100;
    private FadeInOut FadeManager;
    void Start()
    {
        FadeManager = FindObjectOfType<FadeInOut>();
    }
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, Player))
        {
            FadeManager.FadeOutAndLoadScene("TutorialStage");
            float portalX = PlayerPrefs.GetFloat("PortalX");
            float portalY = PlayerPrefs.GetFloat("PortalY");
            float portalZ = PlayerPrefs.GetFloat("PortalZ");
            transform.position = new Vector3(portalX, portalY, portalZ);
        }
    }

}