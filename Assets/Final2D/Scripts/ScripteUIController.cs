using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScripteUIController : MonoBehaviour
{
    public GameObject PauseUI;
    public bool isPause;

    public Text uiText;

    public bool isGetKey;

    void Start()
    {
        PauseUI.SetActive(false);
        isGetKey = false;

        uiText.text = "PAUSE";
    }

    void Update()
    {
        if (!isPause && Input.GetKeyDown(KeyCode.P))
        {
            PauseUI.SetActive(true);
            isPause = true;
            Time.timeScale = 0;
        }
        else if(isPause && Input.GetKeyDown(KeyCode.P))
        {
            PauseUI.SetActive(false);
            isPause = false;
            Time.timeScale = 1;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gotoExitGame();
        }

        if(isGetKey)
        {
            uiText.text = "Mission Complete";
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Dead()
    {
        PauseUI.SetActive(true);
        uiText.text = "GAME OVER";
        Time.timeScale = 0;
    }
    public void toContinue()
    {
        PauseUI.SetActive(false);
        isPause = false;
        Time.timeScale = 1;
    }
    public void gotoMenu()
    {
        loadingScene(0);
    }
    public void gotoHowToPlay()
    {
        loadingScene(1);
    }
    public void gotoSelectLevel()
    {
        loadingScene(2);
    }
    public void gotoSceneGame1()
    {
        loadingScene(3);
    }
    public void gotoSceneGame2()
    {
        loadingScene(4);
    }
    public void gotoSceneGame3()
    {
        loadingScene(5);
    }
    public void gotoHowtoPlay2()
    {
        loadingScene(6);
    }

    public void loadingScene(int scenenum)
    {
        SceneManager.LoadScene(scenenum);
        Time.timeScale = 1;
        ScriptSoundsManager.instance.walkSource.volume = 0;
        Start();
    }
    public void gotoExitGame()
    {
        Application.Quit();
    }

}
