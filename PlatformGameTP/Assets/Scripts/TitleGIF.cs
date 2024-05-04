using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleGIF : MonoBehaviour
{
    public GameObject Title1;
    public GameObject Title2;
    public Transform sceneChagner;
    public Transform soundManager;
    public Button startGameBT;
    public Button exitGameBT;

    private void Awake()
    {
        if (SceneChanger.instance == null)
        {
            Instantiate(sceneChagner, null);
        }
        if (SoundManager.Instance == null)
        {
            Instantiate(soundManager, null);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Title1.SetActive(false);
        Title2.SetActive(false);
        StartCoroutine(GIFImgage());

        startGameBT.onClick.AddListener(SceneChanger.instance.GoToIntro);
        exitGameBT.onClick.AddListener(Application.Quit);
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
