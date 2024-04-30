using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleGIF : MonoBehaviour
{
    public GameObject Title1;
    public GameObject Title2;
    // Start is called before the first frame update
    void Start()
    {
        Title1.SetActive(false);
        Title2.SetActive(false);
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
            yield return new WaitForSeconds(0.3f);
            Title1.SetActive(false);
            Title2.SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }


    }
}
