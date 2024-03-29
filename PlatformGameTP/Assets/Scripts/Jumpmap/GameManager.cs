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
    [Header("카운트다운 캔버스")][SerializeField] GameObject CountDownCanvas;
    [Header("아이템보상 캔버스")][SerializeField] GameObject ItemGetCanvas;

    [Header("팝업 캔버스")] public GameObject GKeyPopup;
    [Header("입장 레이 오브젝트")] public GameObject Raypoint;
    [Header("플레이어")] public Transform Player;
    [Header("기믹 시작 위치")] public Transform GimicStart;
    [Header("내보낼 때 위치(문 앞)")] public Transform GimicEnd;

    public LayerMask TP;
    public bool isTpobject = false;
    public bool isPopup = false;
    public float GimicHp = 1; //최대 기믹 입장 횟수
    public float PlayerHp; // 현재 기믹 입장 횟수

    // Start is called before the first frame update
    void Start()
    {
        PlayerHp = GimicHp;
        GKeyPopup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit Tphit;
        if (Physics.Raycast(Raypoint.transform.position, Raypoint.transform.forward, out Tphit, Mathf.Infinity, TP) ||
            Physics.Raycast(Raypoint.transform.position + Vector3.forward * 0.1f, Raypoint.transform.forward, out Tphit, Mathf.Infinity, TP) ||
            Physics.Raycast(Raypoint.transform.position + Vector3.back * 0.1f, Raypoint.transform.forward, out Tphit, Mathf.Infinity, TP))
        {
            isTpobject = true;
            GKeyPopup.SetActive(true);
        }
        else
        {
            isTpobject = false;
            GKeyPopup.SetActive(false);
        }
        if (isTpobject && Input.GetKeyDown(KeyCode.G) && PlayerHp == 1)
        {
            TimeScaleOff();
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

    void Hp()
    {
        GimicHp--;
        if(GimicHp <= 0)
        {
            GimicHp = 0;
            PlayerHp = GimicHp;
        }
    }

    void CanvasOn()
    {
        Canvas.SetActive(true);  //버튼 캔버스 온
        GKeyPopup.SetActive(false);  
        isPopup = true;
    }

    void CanvasOff()
    {
        Canvas.SetActive(false);
        isPopup = false;
    }

    //입장 버튼 GameStart
    public void GameStart()
    {
        TimeScaleOn();
        CanvasOff();
        Tp(Player, GimicStart);
        CountDownCanvas.gameObject.SetActive(true);
    }

    //나가기 버튼 Exit
    public void Exit()
    {
        TimeScaleOn();
        CanvasOff();
    }

    //기믹 실패 FailGimic
    public void FailGimic()
    {
        FailCanvas.SetActive(true); //Fail UI //버튼을 누르면 문앞으로 플레이어를 내보낸다.
        TimeScaleOff();
        Hp();
    }

    void TimeScaleOff()
    {
        Time.timeScale = 0.0f;
    }

    void TimeScaleOn()
    {
        Time.timeScale = 1.0f;
    }

    //기믹 성공 ClearGimic
    public void ClearGimic()
    {
        CountDownCanvas.gameObject.SetActive(false); // 카운트다운을 꺼버리던가, 멈춘다.
        ClearCanvas.SetActive(true); //Clear UI //버튼을 누르면 문앞으로 플레이어를 내보낸다.
        Hp();
        TimeScaleOff();
    }

    //아이템 보상 ItemGet
    public void ItemGet()
    {
        ItemGetCanvas.SetActive(true);
        TimeScaleOff();
    }

    public void CountDownEnd()
    {
        CountDownCanvas.gameObject.SetActive(false); //카운트다운을 꺼버린다.
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

    //Fail 버튼을 누르면 문앞으로 나가진다.
    public void FailAct()
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
        TimeScaleOn();
    }

    //겟 버튼을 누르면 시간이 흐른다.
    public void itemGetAct()
    {
        ItemGetCanvas.SetActive(false);
        TimeScaleOn();
    }

}
