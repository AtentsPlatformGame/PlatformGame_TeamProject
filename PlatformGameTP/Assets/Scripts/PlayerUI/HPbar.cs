using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    public static HPbar Instance;
    [SerializeField] public Slider myHpSlider;

    public TextMeshProUGUI APstat;
    public TextMeshProUGUI MoveSpdstat;
    public TextMeshProUGUI AttackRangestat;


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

    public void APStats(float Ack)
    {
        APstat.GetComponent<TextMeshProUGUI>().text = Ack.ToString();
        
    }

    public void SPDstats(float Spd)
    {
        MoveSpdstat.GetComponent<TextMeshProUGUI>().text = Spd.ToString();
    }

    public void AttackRange(float Rng)
    {
        AttackRangestat.GetComponent<TextMeshProUGUI>().text = Rng.ToString();
    }

    public void Update()
    {
       
    }

    void UpdateUI()
    {
        if(pc != null)
        {
            HPbar.Instance.UpdateHpbar(pc.GetCurHP(),pc.GetMaxHP());
          //HPbar.Instance.UpdateStats(pc.GetAp(),pc.GetAttackRange(),pc.GetMoveSpeed());
        }
        
    }

    public void ChangeHpSlider()
    {
        myHpSlider.value = Mathf.Lerp(myHpSlider.value, pc.GetCurHP(), Time.deltaTime*2) ;
    }

   

}
