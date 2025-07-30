
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BallSound : MonoBehaviour
{
    public AudioClip hitSound; // 効果音ファイル
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // マウス左ボタンを押している間だけ処理
        if (Input.GetMouseButtonDown(0)) // 左クリック押下時
        {
            audioSource.Play();
        }
    }
}
