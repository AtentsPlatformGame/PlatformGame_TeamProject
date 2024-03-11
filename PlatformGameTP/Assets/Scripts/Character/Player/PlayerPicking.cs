using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerPicking : MonoBehaviour
{
    [Header("스펠 사용 VFX")]
    public Canvas spellCanvas;
    public Image spellRange;
    public float spellMaxRange = 6.0f;

    public LayerMask layermask;
    public UnityEvent attackAct; // PlayerController Attack()
    public UnityEvent<bool> spellReadyAct; // 스펠 사용 준비 
    public UnityEvent useSpellAct; // 스펠 사용
    PlayerController playerController;

    Vector3 pos;
    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();

        spellCanvas.enabled = false;
        spellRange.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isAlive() == false || playerController == null) return;

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
                Debug.Log("스펠 사용 준비");
                spellReadyAct?.Invoke(true);
                // 이때부터 사정거리를 표시해야함
                SpellCanvasEnabled(true);
            }
        }
        else // 스펠 사용 준비 후
        {

            DrawSpellPoint();

            if (Input.GetMouseButtonDown(0)) // 스펠 사용
            {
                useSpellAct?.Invoke();
                spellReadyAct?.Invoke(false);
                Debug.Log("스펠 사용");
                SpellCanvasEnabled(false);
                //playerController.ResetSpellTrigger();
            }
            else if (Input.GetMouseButtonDown(1)) // 스펠 사용 취소
            {
                Debug.Log("스펠 사용 취소");
                spellReadyAct?.Invoke(false);
                SpellCanvasEnabled(false);
            }
            
        }
        
    }

    void SpellCanvasEnabled(bool state)
    {
        spellCanvas.enabled = state;
        spellRange.enabled = state;
    }

    void DrawSpellPoint()
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
        {
            pos = hit.point;
        }

        Vector3 hitDir = (hit.point - transform.position).normalized;
        float distance = Vector3.Distance(hit.point, transform.position);

        distance = Mathf.Min(distance, spellMaxRange);

        Vector3 newHitPoint = transform.position + hitDir * distance;
        spellCanvas.transform.position = new Vector3(0,newHitPoint.y, newHitPoint.z);



    }

   
}
