using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager_TMP : MonoBehaviour
{
    int playerGold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGold(int _dropGold)
    {
        playerGold += _dropGold;
    }

    public int GetGold()
    {
        return this.playerGold;
    }

    public void SetGold(int _gold)
    {
        playerGold = _gold;
    }
}
