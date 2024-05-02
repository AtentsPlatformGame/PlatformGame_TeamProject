using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TombStone : MonoBehaviour
{
    [SerializeField] bool isTombStoneGimicOn = false;
    [SerializeField] float patternTime;
    [SerializeField] Slider gimicTimeSlider;

    public Transform summonEffect;
    public Transform summonAura;
    public Transform spawnEffect;
    public Transform summonPoint;
    public Transform destroyPoint;

    [Header("플레이어 움직이게 하는 함수를 바인딩"), Space(5)]public UnityEvent playerMoveTrueAct;
    [Header("플레이어 못움직이게 하는 함수를 바인딩"),Space(5)] public UnityEvent playerMoveFalseAct;
    [Header("보스가 아래로 내려와 스턴을 걸리게 하는 함수를 바인딩"), Space(5)] public UnityEvent gimicClearAck;
    [Header("보스가 아래로 내려와 체력을 회복하게 하는 함수를 바인딩"), Space(5)] public UnityEvent gimicFailAck;

    public LayerMask playerMask;
    public GameObject GKeyPopUp;
    public GameObject GimicScreen;
    public float spawnSpeed = 20.0f;

    BoxCollider myCollider;
    public AudioClip Sound;
    public AudioSource myAudioSource;

    float playTime = 0.0f;
    bool clearGimmic = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        myCollider = GetComponent<BoxCollider>();
        StartCoroutine(SummonTombStone());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SummonTombStone() // 비석 소환
    {
        summonAura.gameObject.SetActive(true);
        spawnEffect.GetComponent<ParticleSystem>().Play();
        Vector3 dir = summonPoint.position - this.transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        float delta = Time.deltaTime * spawnSpeed;

        while (!Mathf.Approximately(dist, 0.0f))
        {
            if (dist < delta) dist = delta;
            dist -= delta;
            transform.Translate(dir * delta, Space.World);
            yield return new WaitForSeconds(0.1f);
        }
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 18f, this.gameObject.transform.position.z);
        myCollider.enabled = true;
        summonEffect.gameObject.SetActive(true);

        yield return StartCoroutine(TombStonePattern());
    }

    IEnumerator DestroyTombStone() // 비석 파괴
    {
        zeoliteSound(Sound);
        spawnEffect.GetComponent<ParticleSystem>().Play();
        myCollider.enabled = false;
        summonEffect.gameObject.SetActive(false);
        summonAura.gameObject.SetActive(false);

        Debug.Log("삭제 시작");
        Vector3 dir = destroyPoint.position - this.transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        float delta = Time.deltaTime * spawnSpeed;
        Debug.Log(dist);

        while (!Mathf.Approximately(dist, 0.0f))
        {
            if (dist < delta) dist = delta;
            dist -= delta;
            Debug.Log(dist);
            transform.Translate(dir * delta, Space.World);
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(this.gameObject);
    }

    IEnumerator TombStonePattern()
    {
        while(playTime < patternTime)
        {
            gimicTimeSlider.value = (patternTime - playTime) / patternTime;
            playTime += Time.deltaTime;
            if (clearGimmic) break;
            yield return null;
        }

        playerMoveTrueAct?.Invoke();
        if(GimicScreen.activeSelf) GimicScreen.gameObject.SetActive(false);

        if (clearGimmic)
        {
            // 다시 보스를 아래로 내리고 기절시킴
            Debug.Log("클리어");
            gimicClearAck?.Invoke();// 보스가 아래로 내려와 스턴을 걸리게 하는 코루틴을 실행하는 함수
        }
        else
        {
            // 보스의 피를 일정량 채움
            gimicFailAck?.Invoke(); // 보스가 아래로 내려와 체력을 회복시키는 코루틴을 실행하는 함수
            Debug.Log("실패");
        }

        yield return StartCoroutine(DestroyTombStone());
    }

    private void OnTriggerEnter(Collider other)
    {
       if((1 << other.gameObject.layer & playerMask) != 0)
        {
            Debug.Log("플레이어 들어옴");
            GKeyPopUp.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            Debug.Log("플레이어 들어옴");
            if (!isTombStoneGimicOn)
            {
                Debug.Log("기믹 대기중");
                if (Input.GetKeyDown(KeyCode.G))
                {
                    Debug.Log("기믹 시작");
                    playerMoveFalseAct?.Invoke();
                    isTombStoneGimicOn = true;
                    GKeyPopUp.gameObject.SetActive(false);
                    // 기믹 실행
                    GimicScreen.gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GKeyPopUp.gameObject.SetActive(false);
    }

    public void ClearGimic()
    {
        this.clearGimmic = true;
    }

    public void FailGimic()
    {
        this.playTime = patternTime;
    }

    public void zeoliteSound(AudioClip clip)
    {
        if (myAudioSource != null)
        {
            myAudioSource.clip = clip;
            myAudioSource.Play();
        }
    }



}
