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

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = Score.ToString(); // 初期スコアを表示

    }
}
