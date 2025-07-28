using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayEffect : MonoBehaviour
{

    [SerializeField]
    [Tooltip("GoalEffect")]
    private ParticleSystem particle;

    public GameObject effectPrefab;
    public Transform spawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        OnCollisionEnter(null); // 初期化のために呼び出す（必要に応じて削除）
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 衝突時の処理
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトが"Player"タグを持っているかチェック
        if (collision.gameObject.tag == "Player")
        {
            // パーティクルシステムのインスタンスを作成する。
            ParticleSystem newParticle = Instantiate(particle);
            // パーティクルの生成場所をこのスクリプトがアタッチされているGameObjectの場所にする。
            newParticle.transform.position = this.transform.position;
            // パーティクルを再生開始する。
            newParticle.Play();
            // インスタンス化したパーティクルシステムをGameObjectを5秒後に削除する。(メモリ)
            // これをnewParticleにするとコンポーネントだけが削除される。
            Destroy(newParticle.gameObject, 5.0f);
        }
    }
}