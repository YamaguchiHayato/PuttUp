using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;      // 追従するターゲット（ボール）
    public Rigidbody targetRb;    // ボールのRigidbody
    public Vector3 offset = new Vector3(0, 5, -10); // 相対位置（後ろ向き）
    public float smoothSpeed = 5f; // 追従のなめらかさ

    void LateUpdate()
    {
        if (target == null || targetRb == null) return;

        // 進行方向（速度ベクトル）が十分大きい場合のみ、その方向を使う
        Vector3 moveDir = targetRb.velocity.normalized;
        if (moveDir.magnitude < 0.1f)
        {
            moveDir = target.forward; // 停止中はforwardを使う
        }

        // 進行方向の後ろにoffsetを回転させて配置
        Quaternion rot = Quaternion.LookRotation(moveDir, Vector3.up);
        Vector3 desiredPosition = target.position + rot * offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
} 