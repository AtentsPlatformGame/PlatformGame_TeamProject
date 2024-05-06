using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheatKey : MonoBehaviour
{
    public ItemStat cheatItem;
    public UnityEvent<ItemStat> updateInventory;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("");
            updateInventory?.Invoke(cheatItem);
        }   
    }
}
