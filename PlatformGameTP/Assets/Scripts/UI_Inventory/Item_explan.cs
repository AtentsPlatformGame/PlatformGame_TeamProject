using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Item_explan : Inventory, IPointerEnterHandler, IPointerExitHandler
{
    
    public Vector3 MousePos;
    public bool MouseOverCheck;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        MyExplanation.gameObject.SetActive(true);
        MyExplanation.position = MousePos;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        MyExplanation.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        MouseOverCheck = false;
        
       // MyExplanation.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MousePos = Input.mousePosition;
    }


}
