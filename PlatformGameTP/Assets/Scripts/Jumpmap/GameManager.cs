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
    

    [Header("팝업 캔버스")] public GameObject GKeyPopup;
    [Header("입장 레이 오브젝트")] public GameObject Raypoint;
    [Header("플레이어")] public Transform Player;
    [Header("기믹 시작 위치")] public Transform GimicStart;
    [Header("내보낼 때 위치(문 앞)")] public Transform GimicEnd;

    [Header("성공 시 뒤로 못 가게 하는 콜라이더")] public GameObject NotGoBack; //성공 시 뒤로 돌아가 죽는 거 방지

    public LayerMask TP;
    public bool isTpobject = false;
    public bool isPopup = false;
    public float GimicHp = 1; //최대 기믹 입장 횟수
    public float PlayerHp; // 현재 기믹 입장 횟수

    public GameObject card1;
    public GameObject card2;
    public GameObject card3;
    public GameObject card4;
    public GameObject card5;
    public GameObject card6;
    public int cardCount;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHp = GimicHp;
        GKeyPopup.SetActive(false);
        NotGoBack.SetActive(false); 
        cardCount = 0;
        card1.SetActive(false);
        card2.SetActive(false);
        card3.SetActive(false);
        card4.SetActive(false);
        card5.SetActive(false);
        card6.SetActive(false);
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
        NotGoBack.SetActive(false);
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
        
        RandomCard();
        TimeScaleOff();
    }

    public void CountDownEnd()
    {
        NotGoBack.SetActive(true);
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
        NotGoBack.SetActive(false );
    }

    //겟 버튼을 누르면 시간이 흐른다.
    public void itemGetAct()
    {
        
        
        TimeScaleOn();
    }

    public void DieFalling()
    {
        PlayerHp = 0.0f;
        GimicHp = 0.0f; 
    }
  
    public void RandomCard()
    {
        
        int RndcardNum = Random.Range(1, 7);
        if (RndcardNum == 1 && cardCount == 0)
        {
            card1.SetActive(true);
            cardCount++;
        }
        if (RndcardNum == 2 && cardCount == 0)
        {
            card2.SetActive(true);
            cardCount++;
        }
        if (RndcardNum == 3 && cardCount == 0)
        {
            card3.SetActive(true);
            cardCount++;
        }
        if (RndcardNum == 4 && cardCount == 0)
        {
            card4.SetActive(true);
            cardCount++;
        }
        if (RndcardNum == 5 && cardCount == 0)
        {
            card5.SetActive(true);
            cardCount++;
        }
        if (RndcardNum == 6 && cardCount == 0)
        {
            card6.SetActive(true);
            cardCount++;
        }
    }

}
