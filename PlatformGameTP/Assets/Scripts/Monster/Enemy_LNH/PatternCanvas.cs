using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternCanvas : MonoBehaviour
{
    [SerializeField, Header("1관 보스 패턴 이미지")] Transform boss1PatternImg;
    [SerializeField, Header("2관 보스 패턴 이미지")] Transform boss2PatternImg;


    public void TurnOnBoss1Pattern()
    {
        StartCoroutine(StartBossPattern(boss1PatternImg));
    }

    public void TurnOnBoss2Pattern()
    {
        StartCoroutine(StartBossPattern(boss2PatternImg));
    }

    IEnumerator StartBossPattern(Transform _patternImg)
    {
        _patternImg.gameObject.SetActive(true);
        yield return StartCoroutine(EndBossPattern(_patternImg));
    }

    IEnumerator EndBossPattern(Transform _patternImg)
    {
        yield return new WaitForSeconds(3f);
        _patternImg.gameObject.SetActive(false);
    }
    
}
