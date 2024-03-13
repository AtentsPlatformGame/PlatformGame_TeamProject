using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : PlayerController
{
    private enum MonsterState
    {
        Idle,
        Roaming,
        Battle,
        Dead
    }

    private MonsterState currentState;
    private Transform PlayerTransfrom;
    private float RoamingRange = 3.0f;//로밍 반경
    private float BattleRange = 1.0f;//배틀 반경
    
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(MonsterStateMachine());
    }

    IEnumerator MonsterStateMachine()
    {
        while (true)
        {
            switch (currentState)
            {
                case MonsterState.Idle: //몬스터 생성 상태
                    currentState = MonsterState.Roaming;
                    break;
                case MonsterState.Roaming: //몬스터 로밍 상태
                    yield return new WaitForSeconds(3f);
                    currentState = MonsterState.Battle;
                    break;
                case MonsterState.Battle: //몬스터 배틀 상태
                    yield return new WaitForSeconds(5f);
                    currentState = MonsterState.Dead;
                    break;
                case MonsterState.Dead: // 몬스터 죽음 상태 이후 상태 변화 없음
                    yield return null;
                    break;
                default:
                    yield return null;
                    break;
            }
        }
    }

    void Roam()
    {
        // 로밍 동작
    }

    void CheckDistance()
    {
        if (Vector3.Distance(transform.position, PlayerTransfrom.position) > RoamingRange)
        {
            currentState = MonsterState.Idle;
        }
        else if(Vector3.Distance(transform.position, PlayerTransfrom.position)> BattleRange)
        {
            currentState = MonsterState.Battle;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
