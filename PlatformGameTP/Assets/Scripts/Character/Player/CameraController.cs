using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform[] cameraPos;
    [SerializeField] Transform orgPos;
    [SerializeField] CinemachineVirtualCamera playerCam;
    float keyDownTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCameraFocus();
    }

    void MoveCameraFocus()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerCam.Follow = cameraPos[0];
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            playerCam.Follow = orgPos;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            playerCam.Follow = cameraPos[1];
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            playerCam.Follow = orgPos;
        }

        if (Input.GetKey(KeyCode.S))
        {
            keyDownTime += Time.deltaTime;

            if(keyDownTime >= 1.0f)
            {
                playerCam.Follow = cameraPos[2];
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            keyDownTime = 0.0f;
            playerCam.Follow = orgPos;
        }
    }
}
