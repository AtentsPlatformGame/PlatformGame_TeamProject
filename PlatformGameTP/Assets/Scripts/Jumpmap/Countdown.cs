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
    void OnEnable()
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

