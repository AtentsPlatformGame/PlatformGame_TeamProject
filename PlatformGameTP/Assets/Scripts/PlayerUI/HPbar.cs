using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    public static HPbar Instance;

    [SerializeField]public Slider myHpSlider;
    [SerializeField] public Transform APstat;
    [SerializeField] public Transform Rngstat;
    [SerializeField] public Transform Spdstat;

    public Transform player;
    PlayerController pc;

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pc = player.GetComponent<PlayerController>();
    }

    public void UpdateHpbar(float curHp, float maxHp)
    {
        myHpSlider.value = (float)curHp / maxHp;//체력 비율로 설정
    }

    public void UpdateStats(float Ack, float Rng, float Spd)
    {
        APstat.GetComponent<Text>().text = Ack.ToString();
    }

    void UpdateUI()
    {
        if(pc != null)
        {
            HPbar.Instance.UpdateHpbar(pc.GetCurHP(),pc.GetMaxHP());
            HPbar.Instance.UpdateStats(pc.GetAp(),pc.GetAttackRange(),pc.GetMoveSpeed());
        }
        
    }

    void Update()
    {
       
    }


    public void ChangeHpSlider()
    {
        myHpSlider.value = Mathf.Lerp(myHpSlider.value, pc.GetCurHP(), Time.deltaTime*2) ;
    }
}
