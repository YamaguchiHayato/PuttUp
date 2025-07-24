using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerTMP : MonoBehaviour
{
    public TMP_Text timerText; // �E��ɕ\������^�C�}�[

    public const float startTime = 120.0f; // �^�C�}�[�̊J�n����
    private float remainingTime;

    void Start()
    {
        // �c�莞�Ԃ�������
        remainingTime = startTime;

        // �ŏ��ɕ\���X�V
        UpdateTimer();
    }

    void Update()
    {
        // �c�莞�Ԃ�0���傫����΍X�V
        if (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
            remainingTime = Mathf.Max(remainingTime, 0f); // ���̒l�ɂȂ�Ȃ��悤��

            UpdateTimer();
        }
    }

    // �o�ߎ��Ԃ��X�V���\��
    void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
