using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Item_explan : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Transform myExplanation;
    public void OnPointerEnter(PointerEventData eventData)
    {
        myExplanation.gameObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        myExplanation.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        myExplanation.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }


}
