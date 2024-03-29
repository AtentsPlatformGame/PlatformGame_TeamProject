using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectImgSet : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(TurnOffImg());
    }

    IEnumerator TurnOffImg()
    {
        yield return new WaitForSeconds(1f);
        int cnt = transform.childCount;

        for (int i = 0; i < cnt; i++)
        {
            Transform obj = transform.GetChild(i);
            obj.gameObject.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }
}
