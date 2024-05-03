using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeathCanvas : MonoBehaviour
{
    public Button goToMainBT;

    private void OnEnable()
    {
        goToMainBT.onClick.AddListener(SceneChanger.instance.GoToMain);
    }

    public void goMainButton()
    {
        SceneChanger.instance.GoToMain();
    }
}
