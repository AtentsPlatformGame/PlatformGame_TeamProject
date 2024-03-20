using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gold_Update : GoldManager
{
    public Text GoldCount;

    void Awake()
    {
        GoldCount = GetComponent<Text>();    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GoldCount.text = $"{gold}";

    }
}
