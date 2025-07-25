using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource; // AudioSource���A�^�b�`
    public AudioClip titleBGM;      // �^�C�g����ʂ�BGM
    public AudioClip playFirstBGM;  // 4�X�e�[�W�ڂ܂ł�BGM
    public AudioClip playSecondBGM; // 5�`9�X�e�[�W�ڈȍ~��BGM
    public AudioClip resultBGM;     // ���U���g��ʂ�BGM

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);         // �V�[���Ԃŉ��y��ێ�
        SceneManager.sceneLoaded += OnSceneLoaded;  // �V�[�����[�h���ɌĂ΂��
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // ���������[�N�h�~
    }

    // �V�[�������[�h���ꂽ�Ƃ��ɌĂ΂��
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            // �P�[�X:�^�C�g��
            case "Title":
                ChangeBGM(titleBGM);
                break;
            // �P�[�X:�v���C
            case "Jam":
                ChangeBGM(playFirstBGM);
                break;
            // �P�[�X:�v���C�i5�`9�X�e�[�W�ځj
            //case "PlaySecond":
            //    ChangeBGM(playSecondBGM);
            //    break;
            // �P�[�X:���U���g
            case "GameResult":
                ChangeBGM(resultBGM);
                break;
        }
    }

    // BGM��ύX���郁�\�b�h
    void ChangeBGM(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}