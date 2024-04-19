using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    [SerializeField] int PlayerGold;
    
    public TextMeshProUGUI Owngold;
    // Start is called before the first frame update
    void Start()
    {
        Owngold = GetComponent<TextMeshProUGUI>();
        Owngold.text = PlayerGold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //CountGold();
        Owngold.text = PlayerGold.ToString();
    }
    public void ChangeGold(int _gold)
    {
        this.PlayerGold += _gold;
        if(this.PlayerGold <= 0) this.PlayerGold = 0;
    }
    //CountGold => ÇÊµå °ñµå È¹µæ·®°ú »óÁ¡ÀÇ °ñµå·® °è»êÇÏ¿© ¹Ý¿µ

    public int GetPlayerGold()
    {
        return this.PlayerGold;
    }

    public void SetPlayerGold(int _gold)
    {
        this.PlayerGold = _gold;
    }
}
