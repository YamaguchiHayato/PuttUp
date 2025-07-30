using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{
    public TMP_Text[] goalScoreTexts; // Goal_01～Goal_12のスコア表示用
    public TMP_Text totalScoreText;   // 合計スコア表示用

    // Start is called before the first frame update
    void Start()
    {
        // 各ゴールのスコアを表示
        int total = 0;
        for (int i = 0; i < goalScoreTexts.Length; i++)
        {
            int score = PlayerPrefs.GetInt($"Score_Goal_{i + 1}", 0);
            goalScoreTexts[i].text = $"{score}";
            total += score;
        }
        totalScoreText.text = $"{total}";
    }
        
    // Update is called once per frame
    void Update()
    {
        
    }
}