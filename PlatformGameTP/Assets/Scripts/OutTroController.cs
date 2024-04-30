using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OutTroController : MonoBehaviour
{
    public int keyCount;
    public GameObject Intro1;
    public GameObject Intro2;
    public GameObject Intro3;
    public GameObject Intro4;
    public GameObject Intro5;
    

    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;
    public TMP_Text text5;
  

    string dialog1;
    string dialog2;
    string dialog3;
    string dialog4;
    string dialog5;
   

    // Start is called before the first frame update
    void Start()
    {

        Intro1.SetActive(false);
        Intro2.SetActive(false);
        Intro3.SetActive(false);
        Intro4.SetActive(false);
        Intro5.SetActive(false);
     

        dialog1 = "결국엔 마왕을 쓰러트리고";
        dialog2 = "용사의 무덤에 찾아간다.";
        dialog3 = "용사의 무덤 앞에 마왕에게서 얻은 전리품을 놓고";
        dialog4 = "페허가 되버린 마을을 한참 바라보다";
        dialog5 = "다시 모험을 떠난다.";
       
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

        if (SceneChanger.instance != null) SceneChanger.instance.GoToMain();
    }
}
