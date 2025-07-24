using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    // スコアを表示するオブジェクト
    [SerializeField] GameObject scoreObject;
    ScoreTMP scoreTMPScript;

    private Rigidbody rb; // ボールに付いているRigidbody
    public LineRenderer lineRenderer;// ドラッグ中に線を描画するためのLineRenderer 

    private Vector3 dragStartPos; // マウスを押した位置（ワールド座標）
    private Vector3 dragEndPos;   // マウスを離した位置（ワールド座標）

    public const float forceMultiplier = 5.0f; // 力の倍率（ドラッグ距離に掛ける）
    public const float maxForce = 20.0f;       // 力の上限（ドラッグが長くてもこれ以上にならない）

    private const float stopThreshold = 1.0f;
    private bool isDragging = false; // ドラッグ中かどうかのフラグ


    void Start()
    {
        // Rigidbody コンポーネントの取得
        rb = GetComponent<Rigidbody>();
        // スコア表示用のスクリプトを取得
        scoreTMPScript = scoreObject.GetComponent<ScoreTMP>();
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;// 線の頂点数を2に設定
            lineRenderer.enabled = false; // 初期状態では線を非表示にする
        }
    }

    void Update()
    {
        // ボールが動いている間は操作できないようにする
        if (IsBallMoving()) return;

        // マウス左ボタンを押したときにドラッグを開始
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }

        // マウスを押し続けている間、ドラッグ中フラグが true のとき
        if (Input.GetMouseButton(0) && isDragging)
        {
            UpdateDrag();
        }

        // マウスボタンを離したときにショットを実行
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            ReleaseShot();
        }
    }

    // ドラッグの開始時に呼ばれる処理
    void StartDrag()
    {
        isDragging = true;

        // カメラからマウス位置に向けてレイを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 地面やオブジェクトと交差した地点を取得
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            dragStartPos = hit.point; // ドラッグの開始位置を記録
        }
    }

    // ドラッグ中に呼ばれる処理
    void UpdateDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 currentDragPos = hit.point;
            Vector3 direction = (dragStartPos - currentDragPos).normalized;
            float dragDistance = Vector3.Distance(dragStartPos, currentDragPos);
            float force = Mathf.Clamp(dragDistance * forceMultiplier, 0.0f, maxForce);
            Vector3 endPoint = transform.position + direction * force;

            // 線の始点：ボールの位置、終点：方向×力
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, endPoint);
            lineRenderer.enabled = true;
        }
    }

    // マウスを離してショットを実行する処理
    void ReleaseShot()
    {
        isDragging = false;

        // 再度レイキャストして終了位置を取得
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            dragEndPos = hit.point;

            // ドラッグ方向（始点→終点）に反対向きの力を加える
            Vector3 direction = (dragStartPos - dragEndPos).normalized;

            // 距離に応じた力を計算（上限付き）
            float dragDistance = Vector3.Distance(dragStartPos, dragEndPos);
            float force = Mathf.Clamp(dragDistance * forceMultiplier, 0.0f, maxForce);

            // 実際にボールを打つ
            ShootBall(direction, force);
        }
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false; // マウスを離したら線を非表示に
        }
        // スコアを加算
        scoreTMPScript.AddScore(1);
    }

    // Rigidbody に力を加えてボールを飛ばす処理
    void ShootBall(Vector3 direction, float force)
    {
        // 一瞬の力（インパルス）を加える
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    // ボールが動いているかどうかを判定（一定の速さ以下なら静止とみなす）
    bool IsBallMoving()
    {
        // Rigidbody の速度が閾値以下なら静止とみなす
        return rb.velocity.magnitude > stopThreshold;
    }

}