using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProperty : MonoBehaviour
{
    Image _image;
    
    protected Image myImage
    {
        get
        {
            if(_image == null)
            {
                _image = GetComponentInChildren<Image>();
            }
            return _image;
        }
    }
}
