using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingGIF : MonoBehaviour
{
    public GameObject Title1;
    public GameObject Title2;
    public GameObject Title3;
    public GameObject Title4;
    // Start is called before the first frame update
    void Start()
    {
        Title1.SetActive(false);
        Title2.SetActive(false);
        Title3.SetActive(false);
        Title4.SetActive(false);
        StartCoroutine(GIFImgage());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GIFImgage()
    {
        while (true)
        {
            Title1.SetActive(true);
            Title2.SetActive(false);
            Title3.SetActive(false);
            Title4.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            Title1.SetActive(false);
            Title2.SetActive(true);
            Title3.SetActive(false);
            Title4.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            Title1.SetActive(false);
            Title2.SetActive(false);
            Title3.SetActive(true);
            Title4.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            Title1.SetActive(false);
            Title2.SetActive(false);
            Title3.SetActive(false);
            Title4.SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }


    }
}
