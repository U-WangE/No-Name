using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ChangeScene : MonoBehaviour
{
    void Update()
    {
        // 화면 이동 관련
        string ButtonName = EventSystem.current.currentSelectedGameObject.name;
        
        if (Input.GetMouseButtonDown(0) && (ButtonName == "btn_gameStart" || ButtonName == "btn_back"))
            SceneManager.LoadScene("MainPage");
        else if (Input.GetMouseButtonDown(0) && ButtonName == "btn_newGame")
            SceneManager.LoadScene("SurvivalPage");
        else if (Input.GetMouseButtonDown(0) && ButtonName == "btn_status")
            SceneManager.LoadScene("ShopPage");
        else if (Input.GetMouseButtonDown(0) && ButtonName == "btn_shop")
            SceneManager.LoadScene("StatusPage");
    }
}
