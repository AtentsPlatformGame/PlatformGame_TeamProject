using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeSceneToStage1Boss : MonoBehaviour
{
    public UnityEvent playerMoveTrueAct;
    public UnityEvent playerMoveFalseAct;
    public LayerMask playerMask;
    public Transform portalCanvas;
    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                portalCanvas.gameObject.SetActive(true);
                playerMoveFalseAct?.Invoke();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                portalCanvas.gameObject.SetActive(true);
                playerMoveFalseAct?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            portalCanvas.gameObject.SetActive(false);
        }
    }


    public void CancelCanvas()
    {
        portalCanvas.gameObject.SetActive(false);
        playerMoveTrueAct?.Invoke();
    }

    public void AcceptToChangeScene()
    {
        if (SceneChanger.instance != null) SceneChanger.instance.GoToStage1Boss();
    }

}
