using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : MonoBehaviour
{
    public Transform myTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myTarget != null)
        {
            (transform as RectTransform).anchoredPosition =
                Camera.allCameras[1].WorldToViewportPoint(myTarget.position) * 400.0f;
        }
    }

        public void SetIcon(Transform target, Color color)
    {
        myTarget = target;
    }

}
