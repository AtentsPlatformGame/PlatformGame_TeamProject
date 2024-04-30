using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapmanager : MonoBehaviour
{
    [Header("¹Ì´Ï¸Ê ¿Â Äµ¹ö½º")][SerializeField] GameObject Mapcanvas;

    public bool Mapobject = false;
    // Start is called before the first frame update
    void Start()
    {
        CanvasOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (Mapobject)
            {
                Mapobject = false;
                CanvasOff();
            }
            else
            {
                Mapobject = true;
                CanvasOn();
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
