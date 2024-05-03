using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoToNextStage : MonoBehaviour
{
    public Canvas goToNextCanvas;
    public GameObject gKeyPopUp;
    public LayerMask playerMask;
    public UnityEvent playerMoveFalseAct;
    public UnityEvent playerMoveTrueAct;

    bool isPlayerIn;

    private void OnTriggerEnter(Collider other)
    {
        if((1 << other.gameObject.layer & playerMask) != 0)
        {
            gKeyPopUp.SetActive(true);
            isPlayerIn = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.G) && isPlayerIn)
        {
            playerMoveFalseAct?.Invoke();
            goToNextCanvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            gKeyPopUp.SetActive(false);
            isPlayerIn = false;
        }
    }
    public void CloseWindow()
    {
        playerMoveTrueAct?.Invoke();
        goToNextCanvas.gameObject.SetActive(false);
    }
}
