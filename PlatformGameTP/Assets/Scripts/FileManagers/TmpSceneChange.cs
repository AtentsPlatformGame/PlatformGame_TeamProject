using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class TmpSceneChange : MonoBehaviour
{
    public GameObject tmpCanvas;
    public GameObject tmpButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F7))
        {
            tmpCanvas.SetActive(true);
            tmpButton.GetComponent<Button>().onClick.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {

        }
    }
}
