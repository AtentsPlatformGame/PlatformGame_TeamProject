using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    float originTime;
    [SerializeField] float setTime = 60.0f;
    [SerializeField] TMPro.TMP_Text countdownText;

    Life life;
    string minutesS = "";
    string secondsS = "";
    int minute;
    float second;
    // Start is called before the first frame update
    void Start()
    {
        life = FindObjectOfType<Life>();
        originTime = setTime;
        countdownText.text = setTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (setTime > 0.0f)
            setTime -= Time.deltaTime;
        else if (setTime <= 0.0f)
        {
            /*
             setTime = 0 -> 지정해놓은 플레이타임을 다 썼다 -> 라이프를 1줄인다.
            // 라이프를 1 줄인다.
            // 라이프가 Life.cs에 그 정보가 있으니까
            // UnityEvent나 기타 다른 방법을 이용해서 그 라이프를 가져온다
            // 
             */
            life.Time();
            setTime = originTime;
        }
            

        //countdownText.text = Mathf.Round(setTime).ToString();
        minute = (int)(setTime / 60.0f);
        second = setTime % 60.0f;

        minutesS = minute.ToString();
        secondsS = Mathf.Round(second).ToString();
        countdownText.text = minutesS + " : " + secondsS;
    }

    public void SetTime()
    {
        setTime = originTime;
    }
    
}

