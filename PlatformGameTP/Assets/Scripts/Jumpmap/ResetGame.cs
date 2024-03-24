using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public GameObject objectToReset;
    [SerializeField] GameObject countDown;
    [SerializeField] GameObject countDown2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetObject();
            ResetCountdown();
        }
    }

    void ResetObject()
    {
        objectToReset.SetActive(false);
        objectToReset.SetActive(true);
    }

    void ResetCountdown()
    {
        if (countDown != false)
            countDown.SetActive(false);
    }

}
