using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class DragShoot : MonoBehaviour
{
    private Rigidbody rb; // ボールに付いているRigidbody

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
        // 将来的に UI 表示や線・矢印の描画などに使用可能
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
    }

    // Rigidbody に力を加えてボールを飛ばす処理
    void ShootBall(Vector3 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode.Impulse); // 一瞬の力（インパルス）を加える
    }

    // ボールが動いているかどうかを判定（一定の速さ以下なら静止とみなす）
    bool IsBallMoving()
    {
        return rb.velocity.magnitude > stopThreshold;
    }

}