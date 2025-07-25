using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource; // AudioSourceをアタッチ
    public AudioClip titleBGM;      // タイトル画面のBGM
    public AudioClip playFirstBGM;  // 4ステージ目までのBGM
    public AudioClip playSecondBGM; // 5～9ステージ目以降のBGM
    public AudioClip resultBGM;     // リザルト画面のBGM

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);         // シーン間で音楽を保持
        SceneManager.sceneLoaded += OnSceneLoaded;  // シーンロード時に呼ばれる
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // メモリリーク防止
    }

    // シーンがロードされたときに呼ばれる
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            // ケース:タイトル
            case "Title":
                ChangeBGM(titleBGM);
                break;
            // ケース:プレイ
            case "Jam":
                ChangeBGM(playFirstBGM);
                break;
            // ケース:プレイ（5～9ステージ目）
            //case "PlaySecond":
            //    ChangeBGM(playSecondBGM);
            //    break;
            // ケース:リザルト
            case "GameResult":
                ChangeBGM(resultBGM);
                break;
        }
    }

    // BGMを変更するメソッド
    void ChangeBGM(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}