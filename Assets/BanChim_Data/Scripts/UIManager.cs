using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public GameObject homeGUI;

    public GameObject gameGUI;

    public Dialog gameDialog;

    public Dialog pauseDialog;

    public Image fireRateFilled;

    public Text timeText;

    public Text killedCount;

    public Dialog curDialog;

    public Dialog CurDialog { get => curDialog; set => curDialog = value; }

    public override void Awake()
    {
        //ko lưu dữ liệu khi mở game 
        MakeSingleton(false);
    }

    public void ShowGameUI(bool isShow)
    {
        if (gameGUI)
        {
            gameGUI.SetActive(isShow);
        }

        //nếu mà game xuất hiện thì home sẽ biến mất và ngược lại


        if (homeGUI)
        {
            homeGUI.SetActive(!isShow);
        }
    }

    public void UpdateTime(string time)
    {
        if (timeText)
        {
            timeText.text = time;
        }
    }
    public void UpdateKillin(int killed)
    {
        if (killedCount)
        {
            killedCount.text = "x" + killed.ToString();
        }
    }

    public void GetFireRate(float rate)
    {
        if (fireRateFilled)
        {
            fireRateFilled.fillAmount = rate;
        }
    }

    public void PauseGame()
    {
        //dừng hết haotj động của game
        Time.timeScale = 0;
        //Muốn các hoạt động game quay trở lại thì timescale = 1
        if (pauseDialog)
        {
            pauseDialog.ShowDialog(true);
            pauseDialog.UpdateDialog("GAME PAUSE", "BEST KILLED : x" + Prefabs.bestScore);
            curDialog = pauseDialog;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        //đóng đi cái dialog hiện tại
        if (curDialog)
        {
            curDialog.ShowDialog(false);
        }

        curDialog = pauseDialog;
    }

    public void Home()
    {
        ResumeGame();

        //Load lại màn hinhfh hiện tại cảu nguoiwf chơi
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Replay()
    {
        if (curDialog)
        {
            curDialog.ShowDialog(false);

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            ShowGameUI(true);

            //gọi lại phương thức playgame
            GameManager.Ins.PlayGame();
        }


    }

    public void Exit()
    {
        ResumeGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //thoát khỏi ứng dụng ( chỉ hoạt động trên điệnt thoại)
        Application.Quit();
    }
}
