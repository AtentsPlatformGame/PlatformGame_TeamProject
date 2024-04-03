using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Transform myTarget;
    public Slider mySlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(myTarget.position);
        if (screenPos.z > 0.0f)
        {
            transform.position = screenPos;
        }
        else
        {
            transform.position = new Vector3(0, 100000, 0);
        }
    }

    public void ChangeHpSlider(float v)
    {
        mySlider.value = v;
    }

}
