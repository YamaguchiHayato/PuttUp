using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraScpirt : MonoBehaviour
{
    public Transform target; // スフィアなどの追従対象
    public float distance = 10.0f;
    public float zoomSpeed = 2.0f;
    public float rotationSpeed = 5.0f;
    public float minDistance = 3.0f;
    public float maxDistance = 20.0f;

    private float currentX = 0.0f;
    private float currentY = 10.0f;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    void LateUpdate()
    {
        if (target == null) return;

        // マウスの左右移動でカメラ回転
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        currentY = Mathf.Clamp(currentY, yMinLimit, yMaxLimit);

        // マウスホイールでズーム
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // カメラ位置の計算
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }
}
