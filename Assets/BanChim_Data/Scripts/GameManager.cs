using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //tgian tạo chim
    public float spawnTime;

    public Bird[] birds;

    public int timeLimit;

    int curTimeLimit;

    int birdKilled;



    bool isGameOver;

    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    public int BirdKilled { get => birdKilled; set => birdKilled = value; }

    //khởi tạo ko lưu giá trị khi sang màn mới
    public override void Awake()
    {
        MakeSingleton(false);

        curTimeLimit = timeLimit;

        //PlayerPrefs.DeleteAll();
    }

    public override void Start()
    {
        UIManager.Ins.ShowGameUI(false);

        UIManager.Ins.UpdateKillin(birdKilled);

    }

    public void PlayGame()
    {
        StartCoroutine(GameSpawn());

        StartCoroutine(TimeCountDown());

        UIManager.Ins.ShowGameUI(true);
       
    }

    

    //hàm giảm giá trị timelimt
    IEnumerator TimeCountDown()
    {
        while(curTimeLimit > 0)
        {
            //đợi 1s thì giảm thời gian đi là 1;
            yield return new WaitForSeconds(1f);

            curTimeLimit--;

            UIManager.Ins.UpdateTime(InttoTime(curTimeLimit));
            if (curTimeLimit <= 0)
            {
                isGameOver = true;

                if(birdKilled > Prefabs.bestScore)
                {
                    UIManager.Ins.gameDialog.UpdateDialog("NEW BEST", "BEST KILLED : x" + birdKilled);
                }
                else if(birdKilled < Prefabs.bestScore){
                    UIManager.Ins.gameDialog.UpdateDialog("YOUR BEST", "BEST KILLED : x" + Prefabs.bestScore);
                }


                Prefabs.bestScore = birdKilled; 

                UIManager.Ins.gameDialog.ShowDialog(true);

                UIManager.Ins.curDialog = UIManager.Ins.gameDialog;

                
              
            }

        }
    }
    IEnumerator GameSpawn()
    {
        while (!isGameOver)
        {
            SpawnBird();
            //Đợi bao nhiêu giây để thực hiện vòng lặp tiếp theo
            yield return new WaitForSeconds(spawnTime);
        }
    }
    void SpawnBird()
    {
        //giá trị khởi tạo là vector 0
        Vector3 spawnPos = Vector3.zero;

        float randCheck = Random.Range(0f, 1f);

        if(randCheck >= 0.5f)
        {
            spawnPos = new Vector3(12, Random.Range(1.5f,4f),0);
        }else
        {
            spawnPos = new Vector3(-12, Random.Range(1.5f, 4f), 0);
        }

        if(birds != null && birds.Length > 0)
        {
            int randIdx = Random.Range(0, birds.Length); 

            if(birds[randIdx] != null)
            {
                Bird birdClone = Instantiate(birds[randIdx], spawnPos, Quaternion.identity);


            }
        }
    }

    string InttoTime(int time)
    {
        //làm tròn số
        float minute = Mathf.Floor(time / 60);

        //làm tròn số thập phân
        float second = Mathf.RoundToInt(time % 60);

        return minute.ToString("00") + " : " + second.ToString("00");
    }
    
}
