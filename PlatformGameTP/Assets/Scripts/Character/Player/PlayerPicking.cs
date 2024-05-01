using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerPicking : MonoBehaviour
{
    
    [Header("스펠 사용 오브젝트")]
    [Header("공격 스펠 사용 위치 이미지 ")]public Transform attackSpellPointImg;
    [Header("버프 스펠 사용 위치 이미지")] public Transform buffSpellPoinImg;
    [Header("스펠 사용 사정거리 이미지")]public Transform spellRangeImg;
    [Header("실제 스펠 사용 사정거리")]public float spellMaxRange = 6.0f;

    public LayerMask layermask;
    public UnityEvent attackAct; // PlayerController Attack()
    public UnityEvent<bool> spellReadyAct; // 스펠 사용 준비 
    public UnityEvent<Vector3> useSpellAct; // 스펠 사용
    public UnityEvent useBuffSpellAct;
    public AudioSource spellAudioSource;
    PlayerController playerController;
    public AudioClip attackspellClip;
    public AudioClip buffspellClip;
    public AudioClip canclespellClip;

    Vector3 pos;
    Ray ray;
    RaycastHit hit;
    Vector3 newHitPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();

        attackSpellPointImg.gameObject.SetActive(false);
        buffSpellPoinImg.gameObject.SetActive(false);
        spellRangeImg.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isAlive() == false || playerController == null || !playerController.GetMovePossible()) return;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (playerController.isSpellReady == false) // 스펠 사용 준비 전
        {
            
            if (Input.GetMouseButtonDown(0)) // 맘에 안듬 나중에 수정할듯 unityevents를 사용해볼 생각, 근데 isAlive는 어떻게 할지 모르겠음
            {
                attackAct?.Invoke();
                Debug.Log("Click");
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (playerController.GetCurrentSpell() == null) return;
                Debug.Log("스펠 사용 준비");
                spellReadyAct?.Invoke(true);
                // 이때부터 사정거리를 표시해야함
                if (playerController.GetCurrentSpell().gameObject.tag == "AttackSpell") // 사용하고자하는 스펠이 Attack일 때
                {
                    SpellObjectEnabled(attackSpellPointImg, spellRangeImg, true);
                    if (spellAudioSource != null)
                    {
                        spellAudioSource.clip = attackspellClip;
                        spellAudioSource.PlayOneShot(attackspellClip);
                    }
                    if (spellAudioSource.isPlaying) Debug.Log("공격주문 시전 효과음 ");
                }
                else // 사용하고자하는 스펠이 Buff일 때
                {
                    SpellObjectEnabled(buffSpellPoinImg, spellRangeImg, true);
                    if (spellAudioSource != null)
                    {
                        spellAudioSource.clip = buffspellClip;
                        spellAudioSource.PlayOneShot(buffspellClip);
                    }
                    if (spellAudioSource.isPlaying) Debug.Log("버프시전 효과음");
                }
                
            }
        }
        else // 스펠 사용 준비 후
        {
            if (playerController.GetCurrentSpell().gameObject.tag == "AttackSpell") // 사용하고자하는 스펠이 Attack일 때
            {
                // 2D 컨트롤 때 스펠 그리기
                DrawSpell(attackSpellPointImg);
            }

            if (Input.GetMouseButtonDown(0)) // 스펠 사용
            {

                Debug.Log("스펠 사용");
                //SpellCanvasEnabled(false);
                //playerController.ResetSpellTrigger();
                //SpellObjectEnabled(spellPointImg, spellRangeImg, false);
                if (playerController.GetCurrentSpell().gameObject.tag == "AttackSpell") // 사용하고자하는 스펠이 Attack일 때
                {
                    SpellObjectEnabled(attackSpellPointImg, spellRangeImg, false);
                    
                }
                else // 사용하고자하는 스펠이 Buff일 때
                {
                    SpellObjectEnabled(buffSpellPoinImg, spellRangeImg, false);
                    //useBuffSpellAct?.Invoke();
                }
                useSpellAct?.Invoke(newHitPoint);
                spellReadyAct?.Invoke(false);
            }
            else if (Input.GetMouseButtonDown(1)) // 스펠 사용 취소
            {
                Debug.Log("스펠 사용 취소");
                spellReadyAct?.Invoke(false);
                if (spellAudioSource != null)
                {
                    spellAudioSource.clip = canclespellClip;
                    spellAudioSource.PlayOneShot(canclespellClip);
                }
                if (spellAudioSource.isPlaying) Debug.Log("스펠 취소");
                //SpellCanvasEnabled(false);
                //SpellObjectEnabled(spellPointImg, spellRangeImg, false);
                if (playerController.GetCurrentSpell().gameObject.tag == "AttackSpell") // 사용하고자하는 스펠이 Attack일 때
                {
                    SpellObjectEnabled(attackSpellPointImg, spellRangeImg, false);
                }
                else // 사용하고자하는 스펠이 Buff일 때
                {
                    SpellObjectEnabled(buffSpellPoinImg, spellRangeImg, false);
                }
            }
            
        }
        
    }

    
    void SpellObjectEnabled(Transform _spellPointImg,Transform _spellRangeImg,bool state)
    {
        _spellPointImg.gameObject.SetActive(state);
        _spellRangeImg.gameObject.SetActive(state);
    }


    void DrawSpell(Transform _spellPointImg)
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
        {
            pos = hit.point;
        }

        Vector3 hitDir = (hit.point - transform.position).normalized;
        float distance = Vector3.Distance(hit.point, transform.position);

        distance = Mathf.Min(distance, spellMaxRange);

        newHitPoint = transform.position + hitDir * distance;
        // 2D일때
        if (playerController.GetControllType())
        {
            // _spellPointImg.transform.position = new Vector3(0, newHitPoint.y + 0.1f, newHitPoint.z);
            _spellPointImg.transform.position = new Vector3(0, playerController.gameObject.transform.position.y + 0.1f, newHitPoint.z);
        }
        else
        {
            //_spellPointImg.transform.position = new Vector3(newHitPoint.x, newHitPoint.y + 0.1f, newHitPoint.z);
            _spellPointImg.transform.position = new Vector3(newHitPoint.x, playerController.gameObject.transform.position.y + 0.1f, newHitPoint.z);
        }
        
    }

   


}
