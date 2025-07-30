using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sphere : MonoBehaviour
{
    // スコアを表示するオブジェクト
    GameObject scoreObject;
    ScoreTMP scoreTMPScript;

    private Rigidbody rb; // ボールに付いているRigidbody
    public LineRenderer lineRenderer;// ドラッグ中に線を描画するためのLineRenderer 

    private Vector3 dragStartPos; // マウスを押した位置（ワールド座標）
    private Vector3 dragEndPos;   // マウスを離した位置（ワールド座標）

    public const float forceMultiplier = 3.5f; // 力の倍率（ドラッグ距離に掛ける）

    private const float stopThreshold = 1.0f;
    private bool isDragging = false; // ドラッグ中かどうかのフラグ


    private Vector3 startMousePos; // マウスを押した位置

    // 各ゴールの座標を配列で管理
    private Vector3[] goalPositions = new Vector3[]
    {
        new Vector3(4.291f, 6.88f, -30.28f),    // Goal_01
        new Vector3(-16.7f, 4.1f, -38.8f),      // Goal_02
        new Vector3(-0.7f, 4.7f, -18.8f),       // Goal_03
        new Vector3(23.0f, 6.9f, -9.2f),        // Goal_04
        new Vector3(99.7f, 2.4f, -91.3f),       // Goal_05
        new Vector3(109.8f, 2.3f, -81.5f),      // Goal_06
        new Vector3(105.48f, 3.6f, -83.7f),     // Goal_07
        new Vector3(99.4f, 1.99f, -95.0f),      // Goal_08
        new Vector3(80.0f, 2.15f, -89.87f),     // Goal_09
        new Vector3(51.6f, 16.76f, -96.1f),     // Goal_10
        new Vector3(61.0f, 23.93f, -65.54f),    // Goal_11
        // Goal_12はリザルト遷移なので座標は不要
    };

    private int lastGoalIndex = -1; // 最後に衝突したゴール番号（0～11）

    void Start()
    {
        // Rigidbody コンポーネントの取得
        rb = GetComponent<Rigidbody>();
        // スコア表示用のスクリプトを取得
        scoreObject = GameObject.Find("ScoreText");
        scoreTMPScript = scoreObject.GetComponent<ScoreTMP>();


        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;// 線の頂点数を2に設定
            lineRenderer.enabled = false; // 初期状態では線を非表示にする
        }
    }

    void Update()
    {
        if (rb.velocity.magnitude < 0.1f)
        {
            MouseOperation(); // マウス操作を実行
        }
        else
        {
            // ボールが動いてる間はラインを非表示にする
            lineRenderer.enabled = false;
        }

        // スペースキーで座標を戻す
        if (Input.GetKeyDown(KeyCode.Space) && lastGoalIndex >= 0 && lastGoalIndex < goalPositions.Length)
        {
            transform.position = goalPositions[lastGoalIndex];
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    // ボールが動いているかどうかを判定（一定の速さ以下なら静止とみなす）
    bool IsBallMoving()
    {
        // Rigidbody の速度が閾値以下なら静止とみなす
        return rb.velocity.magnitude > stopThreshold;
    }

    // マウスでの操作処理をまとめておくメソッド。
    public void MouseOperation()
    {
        // ボールが動いている間は操作できないようにする
        if (IsBallMoving()) return;

        // マウス左ボタンを押したときにドラッグを開始
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Input.mousePosition; // ドラッグ開始点を記録
            isDragging = true;
            lineRenderer.enabled = true; // ラインを表示

        }
        // マウスを押し続けている間、ドラッグ中フラグが true のとき
        if (Input.GetMouseButton(0) && isDragging)
        {
            //UpdateDrag();
            Vector3 currentMousePos = Input.mousePosition; // 現在のマウス位置
            Vector3 dragVector = startMousePos - currentMousePos; // ドラッグ方向

            // ドラッグ方向をワールド座標に変換
            Vector3 worldDirection = new Vector3(dragVector.x, 0, dragVector.y).normalized;
            Vector3 ballPos = transform.position;

            // ラインの始点：ボールの位置
            lineRenderer.SetPosition(0, ballPos);

            // ラインの終点：ドラッグ方向 × 距離で線の長さを表現
            lineRenderer.SetPosition(1, ballPos + worldDirection * dragVector.magnitude * 0.05f);

        }

        // マウスボタンを離したときにショットを実行
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            // ReleaseShot();
            Vector3 endMousePos = Input.mousePosition;
            Vector3 dragVector = startMousePos - endMousePos;

            // ドラッグ方向に力を加える）
            Vector3 forceDirection = new Vector3(dragVector.x, 0, dragVector.y);
            rb.AddForce(forceDirection * forceMultiplier);

            // ボールを打つたびにスコア加算
            scoreTMPScript.AddScore(1);

            // ラインを非表示にして状態リセット
            lineRenderer.enabled = false;
            isDragging = false;

        }

    }

    //Sphereが特定のTagを持つオブジェクトと衝突したときの処理
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Goal_01")
        {
            lastGoalIndex = 0;
            Destroy(other.gameObject);

            // ゴール到達時
            scoreTMPScript.SaveScoreForGoal(1); // スコア保存
            scoreTMPScript.ResetScore();       // スコアリセット

            // Sphereの座標を変更
            transform.position = goalPositions[0];

            //タイマーをリセット
            GameObject timerObject = GameObject.Find("TimerText"); // TimerTMPがアタッチされているオブジェクト名
            if (timerObject != null)
            {
                TimerTMP timerTMPScript = timerObject.GetComponent<TimerTMP>();
                if (timerTMPScript != null)
                {
                    timerTMPScript.ResetTimer();
                }
            }

            // Rigidbodyの速度もリセットしておく
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else if (other.gameObject.tag == "Goal_02")
        {
            lastGoalIndex = 1;
            Destroy(other.gameObject);

            // ゴール到達時
            scoreTMPScript.SaveScoreForGoal(2); // スコア保存
            scoreTMPScript.ResetScore();       // スコアリセット

            // Sphereの座標を変更
            transform.position = goalPositions[1];

            //タイマーをリセット
            GameObject timerObject = GameObject.Find("TimerText"); // TimerTMPがアタッチされているオブジェクト名
            if (timerObject != null)
            {
                TimerTMP timerTMPScript = timerObject.GetComponent<TimerTMP>();
                if (timerTMPScript != null)
                {
                    timerTMPScript.ResetTimer();
                }
            }

            // Rigidbodyの速度もリセットしておく
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else if (other.gameObject.tag == "Goal_03")
        {
            lastGoalIndex = 2;
            Destroy(other.gameObject);

            // ゴール到達時
            scoreTMPScript.SaveScoreForGoal(3); // スコア保存
            scoreTMPScript.ResetScore();       // スコアリセット

            // Sphereの座標を変更
            transform.position = goalPositions[2];

            //タイマーをリセット
            GameObject timerObject = GameObject.Find("TimerText"); // TimerTMPがアタッチされているオブジェクト名
            if (timerObject != null)
            {
                TimerTMP timerTMPScript = timerObject.GetComponent<TimerTMP>();
                if (timerTMPScript != null)
                {
                    timerTMPScript.ResetTimer();
                }
            }

            // Rigidbodyの速度もリセットしておく
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else if (other.gameObject.tag == "Goal_04")
        {
            lastGoalIndex = 3;
            Destroy(other.gameObject);

            // ゴール到達時
            scoreTMPScript.SaveScoreForGoal(4); // スコア保存
            scoreTMPScript.ResetScore();       // スコアリセット

            // Sphereの座標を変更
            transform.position = goalPositions[3];

            //タイマーをリセット
            GameObject timerObject = GameObject.Find("TimerText"); // TimerTMPがアタッチされているオブジェクト名
            if (timerObject != null)
            {
                TimerTMP timerTMPScript = timerObject.GetComponent<TimerTMP>();
                if (timerTMPScript != null)
                {
                    timerTMPScript.ResetTimer();
                }
            }

            // Rigidbodyの速度もリセットしておく
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else if (other.gameObject.tag == "Goal_05")
        {
            lastGoalIndex = 4;
            Destroy(other.gameObject);

            // ゴール到達時
            scoreTMPScript.SaveScoreForGoal(5); // スコア保存
            scoreTMPScript.ResetScore();       // スコアリセット

            // Sphereの座標を変更
            transform.position = goalPositions[4];

            //タイマーをリセット
            GameObject timerObject = GameObject.Find("TimerText"); // TimerTMPがアタッチされているオブジェクト名
            if (timerObject != null)
            {
                TimerTMP timerTMPScript = timerObject.GetComponent<TimerTMP>();
                if (timerTMPScript != null)
                {
                    timerTMPScript.ResetTimer();
                }
            }

            // Rigidbodyの速度もリセットしておく
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else if (other.gameObject.tag == "Goal_06")
        {
            lastGoalIndex = 5;
            Destroy(other.gameObject);

            // ゴール到達時
            scoreTMPScript.SaveScoreForGoal(6); // スコア保存
            scoreTMPScript.ResetScore();       // スコアリセット

            // Sphereの座標を変更
            transform.position = goalPositions[5];

            //タイマーをリセット
            GameObject timerObject = GameObject.Find("TimerText"); // TimerTMPがアタッチされているオブジェクト名
            if (timerObject != null)
            {
                TimerTMP timerTMPScript = timerObject.GetComponent<TimerTMP>();
                if (timerTMPScript != null)
                {
                    timerTMPScript.ResetTimer();
                }
            }

            // Rigidbodyの速度もリセットしておく
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else if (other.gameObject.tag == "Goal_07")
        {
            lastGoalIndex = 6;
            Destroy(other.gameObject);

            // ゴール到達時
            scoreTMPScript.SaveScoreForGoal(7); // スコア保存
            scoreTMPScript.ResetScore();       // スコアリセット

            // Sphereの座標を変更
            transform.position = goalPositions[6];

            //タイマーをリセット
            GameObject timerObject = GameObject.Find("TimerText"); // TimerTMPがアタッチされているオブジェクト名
            if (timerObject != null)
            {
                TimerTMP timerTMPScript = timerObject.GetComponent<TimerTMP>();
                if (timerTMPScript != null)
                {
                    timerTMPScript.ResetTimer();
                }
            }

            // Rigidbodyの速度もリセットしておく
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else if (other.gameObject.tag == "Goal_08")
        {
            lastGoalIndex = 7;
            Destroy(other.gameObject);

            // ゴール到達時
            scoreTMPScript.SaveScoreForGoal(8); // スコア保存
            scoreTMPScript.ResetScore();       // スコアリセット

            // Sphereの座標を変更
            transform.position = goalPositions[7];

            //タイマーをリセット
            GameObject timerObject = GameObject.Find("TimerText"); // TimerTMPがアタッチされているオブジェクト名
            if (timerObject != null)
            {
                TimerTMP timerTMPScript = timerObject.GetComponent<TimerTMP>();
                if (timerTMPScript != null)
                {
                    timerTMPScript.ResetTimer();
                }
            }

            // Rigidbodyの速度もリセットしておく
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else if (other.gameObject.tag == "Goal_09")
        {
            lastGoalIndex = 8;
            Destroy(other.gameObject);

            // ゴール到達時
            scoreTMPScript.SaveScoreForGoal(9); // スコア保存
            scoreTMPScript.ResetScore();       // スコアリセット

            // Sphereの座標を変更
            transform.position = goalPositions[8];

            //タイマーをリセット
            GameObject timerObject = GameObject.Find("TimerText"); // TimerTMPがアタッチされているオブジェクト名
            if (timerObject != null)
            {
                TimerTMP timerTMPScript = timerObject.GetComponent<TimerTMP>();
                if (timerTMPScript != null)
                {
                    timerTMPScript.ResetTimer();
                }
            }

            // Rigidbodyの速度もリセットしておく
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else if (other.gameObject.tag == "Goal_10")
        {
            lastGoalIndex = 9;
            Destroy(other.gameObject);

            // ゴール到達時
            scoreTMPScript.SaveScoreForGoal(10); // スコア保存
            scoreTMPScript.ResetScore();        // スコアリセット

            // Sphereの座標を変更
            transform.position = goalPositions[9];

            //タイマーをリセット
            GameObject timerObject = GameObject.Find("TimerText"); // TimerTMPがアタッチされているオブジェクト名
            if (timerObject != null)
            {
                TimerTMP timerTMPScript = timerObject.GetComponent<TimerTMP>();
                if (timerTMPScript != null)
                {
                    timerTMPScript.ResetTimer();
                }
            }

            // Rigidbodyの速度もリセットしておく
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else if (other.gameObject.tag == "Goal_11")
        {
            lastGoalIndex = 10;
            Destroy(other.gameObject);

            // ゴール到達時
            scoreTMPScript.SaveScoreForGoal(11); // スコア保存
            scoreTMPScript.ResetScore();        // スコアリセット

            // Sphereの座標を変更
            transform.position = goalPositions[10];

            //タイマーをリセット
            GameObject timerObject = GameObject.Find("TimerText"); // TimerTMPがアタッチされているオブジェクト名
            if (timerObject != null)
            {
                TimerTMP timerTMPScript = timerObject.GetComponent<TimerTMP>();
                if (timerTMPScript != null)
                {
                    timerTMPScript.ResetTimer();
                }
            }

            // Rigidbodyの速度もリセットしておく
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else if (other.gameObject.tag == "Goal_12")
        {
            lastGoalIndex = 11;
            Destroy(other.gameObject);

            // ゴール到達時
            scoreTMPScript.SaveScoreForGoal(12); // スコア保存
            scoreTMPScript.ResetScore();        // スコアリセット

            SceneManager.LoadScene("GameResult");
        }
    }
}
