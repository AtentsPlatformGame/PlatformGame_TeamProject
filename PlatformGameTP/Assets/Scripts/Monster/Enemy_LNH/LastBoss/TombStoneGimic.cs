using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TombStoneGimic : MonoBehaviour
{

    [SerializeField] Sprite[] iconsImage;
    [SerializeField] Image[] myChilds;
    [SerializeField] int idx = 0;
    public UnityEvent clearGimicAct;

    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(RandomKeyboard());
    }

    // Update is called once per frame
    void Update()
    {
        if (idx >= myChilds.Length)
        {
            clearGimicAct?.Invoke();
            return;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (myChilds[idx].GetComponent<KeyboardGimic>().GetMyArrow() == 0)
            {
                myChilds[idx].gameObject.SetActive(false);
                idx++;
            }
            else
            {
                for(int i = idx; i >= 0; i--)
                {
                    myChilds[i].gameObject.SetActive(true);
                }
                idx = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (myChilds[idx].GetComponent<KeyboardGimic>().GetMyArrow() == 1)
            {
                myChilds[idx].gameObject.SetActive(false);
                idx++;
            }
            else
            {
                for (int i = idx; i >= 0; i--)
                {
                    myChilds[i].gameObject.SetActive(true);
                }
                idx = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (myChilds[idx].GetComponent<KeyboardGimic>().GetMyArrow() == 2)
            {
                myChilds[idx].gameObject.SetActive(false);
                idx++;
            }
            else
            {
                for (int i = idx; i >= 0; i--)
                {
                    myChilds[i].gameObject.SetActive(true);
                }
                idx = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (myChilds[idx].GetComponent<KeyboardGimic>().GetMyArrow() == 3)
            {
                myChilds[idx].gameObject.SetActive(false);
                idx++;
            }
            else
            {
                for (int i = idx; i >= 0; i--)
                {
                    myChilds[i].gameObject.SetActive(true);
                }
                idx = 0;
            }
        }
    }

    IEnumerator RandomKeyboard()
    {
        for (int i = 0; i < myChilds.Length; i++)
        {
            int rnd = Random.Range(0, 4);
            switch (rnd)
            {
                case 0: // UP
                    myChilds[i].sprite = iconsImage[0];
                    myChilds[i].GetComponent<KeyboardGimic>().SetMyArrow(0);
                    break;
                case 1: // Down
                    myChilds[i].sprite = iconsImage[1];
                    myChilds[i].GetComponent<KeyboardGimic>().SetMyArrow(1);
                    break;
                case 2: // Left
                    myChilds[i].sprite = iconsImage[2];
                    myChilds[i].GetComponent<KeyboardGimic>().SetMyArrow(2);
                    break;
                case 3:// Right
                    myChilds[i].sprite = iconsImage[3];
                    myChilds[i].GetComponent<KeyboardGimic>().SetMyArrow(3);
                    break;
                default:
                    break;
            }
        }

        yield return null;
    }
}
