using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    public static HPbar Instance;
    [SerializeField] public Slider myHpSlider;

    public TextMeshProUGUI Apstat;


    [SerializeField] float APs;
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

    /*public void UpdateStats(float Ack, float Rng, float Spd)
    {
        APstat.GetComponent<Text>().text = Ack.ToString();
    }*/

    public void Update()
    {
        if(APs != 0)
        {
            Input.GetKeyDown(KeyCode.P);
            APs +=1;
        }
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

    public void ChangeAPstas(float _AP)
    {
       this.APs += _AP;
        if (this.APs <= 0) this.APs = 0;
    }

    public float GetStatepUp()
    {
        return this.APs;
    }

    public void SetPlayerAP(float _AP)
    {
        this.APs = _AP;
    }

}
