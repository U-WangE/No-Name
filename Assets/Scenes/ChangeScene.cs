using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ChangeScene : MonoBehaviour
{
    string ButtonName = "";

    void Update()
    {
        // string에 null 들어가는 거 방지
        try
        {
            ButtonName = EventSystem.current.currentSelectedGameObject.name;

            // 화면 이동 관련
            if (Input.GetMouseButtonDown(0) && (ButtonName == "btn_gameStart" || ButtonName == "btn_back"))
                SceneManager.LoadScene("MainPage");
            else if (Input.GetMouseButtonDown(0) && ButtonName == "btn_newGame")
                SceneManager.LoadScene("SurvivalPage");
            else if (Input.GetMouseButtonDown(0) && ButtonName == "btn_status")
                SceneManager.LoadScene("ShopPage");
            else if (Input.GetMouseButtonDown(0) && ButtonName == "btn_shop")
                SceneManager.LoadScene("StatusPage");
        }
        catch (NullReferenceException)
        {
            return;
        }
    }
}
