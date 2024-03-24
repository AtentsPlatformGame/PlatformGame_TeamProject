using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class Item_explan : Inventory, IPointerEnterHandler, IPointerExitHandler
{
    
    public Vector3 MousePos;
    public Vector3 Vect;
    public bool MouseOverCheck;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        MyExplanation.gameObject.SetActive(true);
        MyExplanation.transform.position = gameObject.transform.position + Vect;
        MyExplanation.GetComponent<Image>().sprite = this.GetComponent<InventorySlot_LNH>().GetItemStat().itemDescriptionImage;

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        MyExplanation.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        MouseOverCheck = false;
        Vect = new Vector3(2, -60 , 0);
       // MyExplanation.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MousePos = Input.mousePosition;
    }


}
