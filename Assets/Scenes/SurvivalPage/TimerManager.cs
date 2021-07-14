using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerManager : MonoBehaviour
{
    public Text[] ClockText;  // text 변경
    public float timePause = 1f;
    private float time = 0;  // 시간 계산


    void Update() {
        if (GameManager.Instance.TimerActive)
        {
            time += Time.deltaTime * timePause;
            ClockText[0].text = ((int)time / 3600).ToString("00");
            ClockText[1].text = ((int)time / 60 % 60).ToString("00");
            ClockText[2].text = ((int)time % 60).ToString("00");
        }
    }
}
