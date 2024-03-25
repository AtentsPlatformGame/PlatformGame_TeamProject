using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingRandomItem : MonoBehaviour
{
    public ItemProperty[] itemArray; // 패시브, 스펠 기타 등등이 들어가는 배열
    public ItemProperty[] itemButtons; // 보상 화면에서 나타날 버튼 배열
    int origin = -1;
    // Start is called before the first frame update
    void OnEnable()
    {
        SetItemButton();
        Debug.Log(itemButtons[1].GetItemStat());
    }

    public void SetItemButton()
    {
        for (int i = 0; i < 3;)
        {
            int idx = Random.Range(0, itemArray.Length);
            if (origin != idx)
            {
                itemButtons[i] = itemArray[idx];
                //
                origin = idx;

                i++;
            }
            else
            {
                continue;
            }
        }
    }
}
