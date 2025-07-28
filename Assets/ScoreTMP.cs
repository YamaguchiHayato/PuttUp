using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTMP : MonoBehaviour
{
    public TMP_Text ScoreText; // 右下に表示するスコア
    public int Score = 0; // スコアの初期値

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.GetComponent<TMP_Text>();
    }

    public void AddScore(int AddScore)
    {
        Score += AddScore; // スコアを加算
    }

    // スコアをリセットするメソッドを追加
    public void ResetScore()
    {
        Score = 0;
    }

    //// スコアを読み込むメソッド
    //public void LoadScore()
    //{
    //    Score = PlayerPrefs.GetInt("Score", 0);
    //}

    // ゴール番号を指定してスコアを保存
    public void SaveScoreForGoal(int goalNumber)
    {
        PlayerPrefs.SetInt($"Score_Goal_{goalNumber}", Score);
        PlayerPrefs.Save();
    }

    // ゴール番号を指定してスコアを取得
    public int LoadScoreForGoal(int goalNumber)
    {
        return PlayerPrefs.GetInt($"Score_Goal_{goalNumber}", 0);
    }

    // 合計スコアを取得
    public int LoadTotalScore()
    {
        int total = 0;
        for (int i = 1; i <= 5; i++)
        {
            total += LoadScoreForGoal(i);
        }
        return total;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = Score.ToString(); // 初期スコアを表示

    }
}
