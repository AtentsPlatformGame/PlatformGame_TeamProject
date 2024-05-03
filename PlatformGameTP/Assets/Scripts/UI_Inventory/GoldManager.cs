using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    [SerializeField] int PlayerGold;

    public TextMeshProUGUI OwngoldMyInventory;
    public TextMeshProUGUI OwngoldShopInventory;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {

        //Owngold = GetComponent<TextMeshProUGUI>();
        if (OwngoldMyInventory != null) OwngoldMyInventory.text = PlayerGold.ToString();
        if (OwngoldShopInventory != null) OwngoldShopInventory.text = PlayerGold.ToString();
        /*Owngold = GetComponent<TextMeshProUGUI>();
        Owngold.text = PlayerGold.ToString();*/
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CountUpGold();
        if (OwngoldMyInventory.gameObject.activeSelf == true && OwngoldMyInventory != null) OwngoldMyInventory.text = PlayerGold.ToString();
        if (OwngoldShopInventory.gameObject.activeSelf == true && OwngoldShopInventory != null) OwngoldShopInventory.text = PlayerGold.ToString();
    }
    public void ChangeGold(int _gold)
    {
        if (player != null)
        {
            if (player.GetCA_GoldPenalty())
            {
                this.PlayerGold += (_gold * 3);
            }
            else
            {
                this.PlayerGold += _gold;
            }
        }

        if (this.PlayerGold <= 0) this.PlayerGold = 0;
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

    public void CountUpGold()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            PlayerGold++;
        }
    }

}
