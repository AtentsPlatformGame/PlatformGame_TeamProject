using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public int keyCount;
    public GameObject Intro1;
    public GameObject Intro2;
    public GameObject Intro3;
    public GameObject Intro4;
    public GameObject Intro5;
    public GameObject Intro6;
    public GameObject Intro7;
    
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;
    public TMP_Text text5;
    public TMP_Text text6;
    public TMP_Text text7;
    
    string dialog1;
    string dialog2;
    string dialog3;
    string dialog4;
    string dialog5;
    string dialog6;
    string dialog7;

    // Start is called before the first frame update
    void Start()
    {

        Intro1.SetActive(false);
        Intro2.SetActive(false);
        Intro3.SetActive(false);
        Intro4.SetActive(false);
        Intro5.SetActive(false);
        Intro6.SetActive(false);
        Intro7.SetActive(false);
       
        dialog1 = "멀지않은 과거에 작지만 평화로운 마을이 있었다.";
        dialog2 = "그러던 어느날 마왕이 마을을 나타났고";
        dialog3 = "마을을 폐허로 만들기 시작했다.";
        dialog4 = "이 때, 마을을 위해 한 용감한 용사가 나타났다.";
        dialog5 = "그는 홀로 용감하게 마왕에게 맞섰으나.";
        dialog6 = "결국 마왕을 물리치지 못한채, 죽음을 맞이한다.";
        dialog7 = "용사는 영혼으로 세상에 남아 대적자를 기다린다.";

        keyCount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (keyCount == 0)
        {
            
            StartCoroutine(Typing1());
        }   
    }
    IEnumerator Typing1()
    {
        keyCount = 1;

        text1.text = null;
        text2.text = null;
        text3.text = null;
        text4.text = null;
        text5.text = null;
        text6.text = null;
        text7.text = null;


        Intro1.SetActive(true);
        for (int i = 0; i < dialog1.Length; i++)
        {
            text1.text += dialog1[i];
            yield return new WaitForSeconds(0.2f);
        }
        Debug.Log(text1);
        yield return new WaitForSeconds(1.5f);
        Intro1.SetActive(false);
        Intro2.SetActive(true);
        for (int i = 0; i < dialog2.Length; i++)
        {
            text2.text += dialog2[i];
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(1.5f);
        Intro2.SetActive(false);
        Intro3.SetActive(true);
        for (int i = 0; i < dialog3.Length; i++)
        {
            text3.text += dialog3[i];
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(1.5f);
        Intro3.SetActive(false);
        Intro4.SetActive(true);
        for (int i = 0; i < dialog4.Length; i++)
        {
            text4.text += dialog4[i];
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(1.5f);
        Intro4.SetActive(false);
        Intro5.SetActive(true);
        for (int i = 0; i < dialog5.Length; i++)
        {
            text5.text += dialog5[i];
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(1.5f);
        Intro5.SetActive(false);
        Intro6.SetActive(true);
        for (int i = 0; i < dialog6.Length; i++)
        {
            text6.text += dialog6[i];
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(1.5f);
        Intro6.SetActive(false);
        Intro7.SetActive(true);
        for (int i = 0; i < dialog7.Length; i++)
        {
            text7.text += dialog7[i];
            yield return new WaitForSeconds(0.2f);
        }
    }
}
