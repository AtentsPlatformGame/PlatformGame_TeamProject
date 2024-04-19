using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop_Item_Explain : Inventory, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    void Start()
    {
        MyExplanation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MyExplanation.gameObject.SetActive(true);
        Debug.Log("상점 설명 나와용");
        MyExplanation.GetComponent<Image>().sprite = this.GetComponent<ShopItem_LNH>().GetItemStat().itemDescriptionImage;

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        MyExplanation.gameObject.SetActive(false);
    }
}
