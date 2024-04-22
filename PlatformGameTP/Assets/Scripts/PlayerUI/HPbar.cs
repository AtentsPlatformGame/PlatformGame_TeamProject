using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    [SerializeField]public Slider myHpSlider;

    private float MaxHp = 100;
    private float CurHp = 100;
    float imsi;

    // Start is called before the first frame update
    void Start()
    {
        imsi = (float)CurHp / (float)MaxHp;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
           if(CurHp > 0)
            {
                CurHp -= 10;
            }
           else
            {
                CurHp = 0;
            }
            imsi = (float)CurHp / (float)MaxHp;
        }
        ChangeHpSlider();
        if (Input.GetKeyDown(KeyCode.O))
        {
            if(CurHp < 0)
            {
                CurHp += 10;
            }
            else
            {
                CurHp = MaxHp;
            }
            imsi = (float)CurHp / (float)MaxHp;
        }
        ChangeHpSlider();
    }


    public void ChangeHpSlider()
    {
        myHpSlider.value = Mathf.Lerp(myHpSlider.value, imsi, Time.deltaTime*2) ;
    }
}
