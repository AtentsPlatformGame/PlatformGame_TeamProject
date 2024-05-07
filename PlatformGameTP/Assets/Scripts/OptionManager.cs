using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [Header("메인으로 버튼")][SerializeField] GameObject MainCanvas;
    [Header("게임으로 버튼")][SerializeField] GameObject GameCanvas;
    [SerializeField] Transform MyOptions;
    PlayerController player;
    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    void Update()
    {

    }

    public void GoMain()
    {
        SceneChanger.instance.GoToMain();
    }

    public void GoGame()
    {
        PopDown(MyOptions);
        player.ControllPlayerAttack(true);
    }

    public void PopDown(Transform popup)
    {
        popup.gameObject.SetActive(false);
    }

}
