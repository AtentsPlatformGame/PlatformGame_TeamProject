using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [Header("입장 캔버스")][SerializeField] GameObject Canvas;
    [Header("실패 캔버스")][SerializeField] GameObject FailCanvas;
    [Header("성공 캔버스")][SerializeField] GameObject ClearCanvas;
    [Header("나가기 캔버스")][SerializeField] GameObject ExitCanvas;
    [Header("카운트다운 캔버스")][SerializeField] GameObject CountDown;

    [Header("팝업 캔버스")] public GameObject GKeyPopup;
    [Header("입장 레이 오브젝트")] public GameObject Raypoint;
    [Header("플레이어")] public Transform Player;
    [Header("기믹 시작 위치")] public Transform GimicStart;
    [Header("내보낼 때 위치(문 앞)")] public Transform GimicEnd;

    public LayerMask TP;
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
        if (Physics.Raycast(Raypoint.transform.position, Raypoint.transform.forward, out Tphit, Mathf.Infinity, TP) ||
            Physics.Raycast(Raypoint.transform.position + Vector3.forward * 0.5f, Raypoint.transform.forward, out Tphit, Mathf.Infinity, TP) ||
            Physics.Raycast(Raypoint.transform.position + Vector3.back * 0.5f, Raypoint.transform.forward, out Tphit, Mathf.Infinity, TP))
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
                CanvasOff();
            }
            else
            {
                CanvasOn();  //게임시작
            }
        }



    }
    void CanvasOn()
    {
        Canvas.SetActive(true);  //버튼 캔버스 온
        GKeyPopup.SetActive(false);  //
        isPopup = true;  //
    }

    void CanvasOff()
    {
        Canvas.SetActive(false);
        isPopup = false;
    }

    public void GameStart()
    {
        CanvasOff();
        Tp(Player, GimicStart);
        CountDown.gameObject.SetActive(true);
    }

    public void Exit()
    {
        Canvas.SetActive(false);
    }

    public void FailGimic()
    {
        FailCanvas.SetActive(true); //Fail UI //버튼을 누르면 문앞으로 플레이어를 내보낸다.
        if (FailCanvas)
        {
            TimeScaleOff();
        }
        else
        {
            TimeScaleOn();
        }
    }

    void TimeScaleOff()
    {
        Time.timeScale = 0.0f;
    }

    void TimeScaleOn()
    {
        Time.timeScale = 1.0f;
    }

    //성공 ClearGimic
    public void ClearGimic()
    {
        CountDown.gameObject.SetActive(false); // 카운트다운을 꺼버리던가, 멈춘다.
        ClearCanvas.SetActive(true); //Clear UI //버튼을 누르면 문앞으로 플레이어를 내보낸다.
    }

    public void Tp(Transform tpstart, Transform tpend)
    {
        StartCoroutine(Teleport(tpstart, tpend));
    }

    IEnumerator Teleport(Transform tpstart, Transform tpend)
    {
        yield return new WaitForSeconds(0.1f);
        tpstart.transform.position = tpend.transform.position;
    }

    public void FailAct() // Act -> 버튼을 눌렀을 때 실행할 함수
    {
        FailCanvas.SetActive(false);
        Tp(Player, GimicEnd);
        TimeScaleOn();
    }

    //성공 버튼을 누르면 문앞으로 나가진다.
    public void ClearAct()
    {
        ClearCanvas.SetActive(false);
        Tp(Player, GimicEnd);
    }


}
