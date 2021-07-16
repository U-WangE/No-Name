using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    public GameObject Setting;
    string ButtonName;

    void Update()
    {
        Setting = GameObject.Find("UICanvas").transform.Find("Setting").gameObject;

        // Setting 창
        if (Input.GetButtonDown("Cancel"))
        { // Escape key 클릭 시 Setting 창 Open
            if (Setting.activeSelf)
                Setting.SetActive(false);
            else
                Setting.SetActive(true);
        }

        try
        {
            ButtonName = EventSystem.current.currentSelectedGameObject.name;

            // Setting 창 내에 버튼 클릭
            if (ButtonName == "btn_Menu")  // MainPage로 이동 버튼
            {
                GameManager.Instance.TimerActive = false;
                moveScene();
            }
            else if (ButtonName == "btn_Exit") // 게임 종료 버튼
            {
                Application.Quit();
            }
            else if (ButtonName == "btn_newGame")  // 새 게임 버튼
            {
                GameManager.Instance.TimerActive = true;
            }

            ActiveMenu();
        }
        catch (NullReferenceException)
        {
            return;
        }
    }

    // MainPage로 돌아가는 버튼 SurvivalPage에서 한정 활성화
    private void ActiveMenu()
    {
        if (Setting.activeSelf)
            GameObject.Find("Timer").GetComponent<TimerManager>().timePause = 0;
        else
            GameObject.Find("Timer").GetComponent<TimerManager>().timePause = 1;
    }

    // MainPage로 이동
    public void moveScene()
    {
        Setting.SetActive(false);
        SceneManager.LoadScene("MainPage");
    }

    // 게임 종료
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
