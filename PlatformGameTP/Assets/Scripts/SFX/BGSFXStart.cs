using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSFXStart : MonoBehaviour
{
    public AudioSource bgSFX;
    public float bgSFXStartTime;
    // Start is called before the first frame update
    void Start()
    {
        if(bgSFX != null) bgSFX.time = bgSFXStartTime;
    }

    
}
