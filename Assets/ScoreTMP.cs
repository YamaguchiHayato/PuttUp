using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTMP : MonoBehaviour
{
    public TMP_Text ScoreText; // �E���ɕ\������X�R�A
    public int Score = 0; // �X�R�A�̏����l

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.GetComponent<TMP_Text>();
    }

    public void AddScore(int AddScore)
    {
        Score += AddScore; // �X�R�A�����Z
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = Score.ToString(); // �����X�R�A��\��

    }
}
