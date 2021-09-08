using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefabs 
{
    //để lưu dữ liệu ng chơi vào máy đth thì ta dùng cái này PlayerPrefs

    public static int bestScore
    {
        //lấy ra key để lưu vào bộ nhớ 
        get => PlayerPrefs.GetInt(GameConst.BEST_SCORE, 0);

        //set để lưu lại key
        set
        {
            int curBestScore = PlayerPrefs.GetInt(GameConst.BEST_SCORE);

            //nếu giá trị hiện tại lớn hơn điểm số cao nhất  ban đầu
            if(value > curBestScore)
            {
                //setint để lưu giá trị
                PlayerPrefs.SetInt(GameConst.BEST_SCORE, value);
            }
        }
    }
}
