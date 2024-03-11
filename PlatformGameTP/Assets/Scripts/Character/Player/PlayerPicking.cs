using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPicking : MonoBehaviour
{
    public Transform spellImg;
    public UnityEvent attackAct; // PlayerController Attack()
    public LayerMask layerMask;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isAlive() == false || playerController == null) return;
        
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
                playerController.ReadyToUseSpell(true);
            }
        }
        else // 스펠 사용 준비 후
        {
            
            if (Input.GetMouseButtonDown(0)) // 스펠 사용
            {
                playerController.UsingSpell();
                playerController.ReadyToUseSpell(false);
                Debug.Log("스펠 사용");
                //playerController.ResetSpellTrigger();
            }
            else if (Input.GetMouseButtonDown(1)) // 스펠 사용 취소
            {
                Debug.Log("스펠 사용 취소");
                playerController.ReadyToUseSpell(false);
            }
            
        }
        

    }

   
}
