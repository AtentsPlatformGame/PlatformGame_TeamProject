using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public static int gold;
    
    public TextMeshProUGUI Owngold;
    // Start is called before the first frame update
    void Start()
    {
        gold = 0;
        Owngold = GetComponent<TextMeshProUGUI>();
        Owngold.text = gold.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        CountGold();
        Owngold.text = gold.ToString();
    }

    //CountGold => ÇÊµå °ñµå È¹µæ·®°ú »óÁ¡ÀÇ °ñµå·® °è»êÇÏ¿© ¹Ý¿µ
    public void CountGold()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            gold++;
        }
    }
}
