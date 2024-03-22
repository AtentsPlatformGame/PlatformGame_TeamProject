using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform[] cameraPos;
    [SerializeField] Transform orgPos;
    [SerializeField] CinemachineVirtualCamera playerCam;

    public UnityEvent<bool> changePlayerControllType;

    float keyDownTime = 0.0f;
    bool isCamera3D = false;
    Vector3 originCamRot;
    Coroutine rotatingC;
    Vector3 targetRot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCamera3D)
        {
            MoveCameraFocus();
        }

        
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

    public void SetTrackOX(int _dir)
    {
        Debug.Log(playerCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset);
        playerCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = new Vector3(_dir * 5.0f, 4.0f, 0f);
    }

    // 플레이모드가 2D일 때 카메라 위치 변경
    public void SetCamera2D()
    {
        isCamera3D = false;
        originCamRot = playerCam.transform.localEulerAngles;
        // localRoation x = 0, y = 270, z = 0;
        targetRot = new Vector3(0.0f, 270.0f, 0.0f);
        /*if(rotatingC != null)
        {
            StopCoroutine(rotatingC);
        }*/
        //rotatingC = StartCoroutine(RotatingCam(targetRot2D));
        playerCam.transform.localEulerAngles = targetRot;
        changePlayerControllType?.Invoke(true);

    }

    // 플레이 모드가 3D 일 때 카메라 위치 변경 >> 여긴 추후 개발 예정, 마우스의 움직임에 따라 카메라가 일정 거리에서 따라오게 만들거임
    public void SetCamera3D() // 여기선 Lerp
    {
        isCamera3D = true;
        // localRoation x = 30, y = 300, z = 0;
        originCamRot = playerCam.transform.localEulerAngles;
        targetRot = new Vector3(60.0f, 0.0f, 0.0f);

        /*if (rotatingC != null)
        {
            StopCoroutine(rotatingC);
        }
        rotatingC = StartCoroutine(RotatingCam(targetRot3D));*/
        /*Vector3 targetRot = Vector3.Lerp(originCamRot, targetRot3D, Time.deltaTime * 10.0f);
        */
        playerCam.transform.localEulerAngles = targetRot;
        changePlayerControllType?.Invoke(false);
    }

    // 플레이모드가 탑 뷰일 때 카메라 위치 변경
    public void SetCameraTopView()
    {
        isCamera3D = true;
        /*if (rotatingC != null)
        {
            StopCoroutine(rotatingC);
        }*/
        // localRoation x = 90, y = 0, z = 0;
        targetRot = new Vector3(90.0f, 0.0f, 0.0f);
        playerCam.transform.localEulerAngles = targetRot;
        changePlayerControllType?.Invoke(false);
    }

    IEnumerator RotatingCam(Vector3 targetRot)
    {
        while(targetRot != playerCam.transform.localEulerAngles)
        {
            Vector3 tmpRot = Vector3.Lerp(originCamRot, targetRot, Time.deltaTime * 10.0f);
            playerCam.transform.localEulerAngles = tmpRot;
            yield return new WaitForSeconds(0.1f);
        }
        
        playerCam.transform.localEulerAngles = targetRot;
    }
}
