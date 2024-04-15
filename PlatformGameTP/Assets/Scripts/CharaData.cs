using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaData : MonoBehaviour
{
    // 캐릭터 정보를 저장할 변수들
    public string characterName;
    public int health = 10;
    public int attack = 1;
    public Equipment equipment;
    public Animations animations;

    // 캐릭터의 장비 정보를 저장할 클래스
    [System.Serializable]
    public class Equipment
    {
        public string weapon;
        public string amor;
        public string item;
        public string acces;
    }

    // 캐릭터의 애니메이션 정보를 저장할 클래스
    [System.Serializable]
    public class Animations
    {
        public string idle;
        public string walk;
        public string attack;
        public string hurt;
        public string die;
    }
}
