using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Mapmanager : MonoBehaviour
{
    [Header("¹Ì´Ï¸Ê Äµ¹ö½º")][SerializeField] GameObject Mapcanvas;
    // Start is called before the first frame update
    void Start()
    {
        CanvasOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            CanvasOn();
            if (Mapcanvas)
            {
                CanvasOn();
            }
            else
            {
                CanvasOff();
            }
        }
    }

    public void CanvasOn()
    {
        Mapcanvas.SetActive(true);
    }

    public void CanvasOff()
    {
        Mapcanvas.SetActive(false);
    }

}
