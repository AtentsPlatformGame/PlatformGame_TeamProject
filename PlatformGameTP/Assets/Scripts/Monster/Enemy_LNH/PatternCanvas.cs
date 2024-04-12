using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternCanvas : MonoBehaviour
{
    [SerializeField, Header("1관 보스 패턴 이미지")] Transform boss1PatternImg;
    [SerializeField, Header("2관 보스 패턴 이미지")] Transform boss2PatternImg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnboss1PatternImg()
    {
        boss1PatternImg.gameObject.SetActive(true);
    }

    public void TurnOffboss1PatternImg()
    {
        boss1PatternImg.gameObject.SetActive(false);
    }

    public void TurnOnboss2PatternImg()
    {
        boss2PatternImg.gameObject.SetActive(true);
    }

    public void TurnOffboss2PatternImg()
    {
        boss2PatternImg.gameObject.SetActive(false);
    }
}
