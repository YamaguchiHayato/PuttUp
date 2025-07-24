using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerTMP : MonoBehaviour
{
    public TMP_Text timerText; // 右上に表示するタイマー

    public const float startTime = 120.0f; // タイマーの開始時間
    private float remainingTime;

    void Start()
    {
        // 残り時間を初期化
        remainingTime = startTime;

        // 最初に表示更新
        UpdateTimer();
    }

    void Update()
    {
        // 残り時間が0より大きければ更新
        if (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
            remainingTime = Mathf.Max(remainingTime, 0f); // 負の値にならないように

            UpdateTimer();
        }
    }

    // 経過時間を更新し表示
    void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
