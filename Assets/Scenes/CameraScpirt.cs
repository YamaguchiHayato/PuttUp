using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScpirt : MonoBehaviour
{
    public Transform target; // 追いかけるボール（プレイヤー）
    public Vector3 offset = new Vector3(0, 5, -10); // カメラの位置のずれ
    public float smoothSpeed = 0.125f; // カメラの追従スピード
    public float rotateSpeed = 100f; // 視点回転スピード
    public float zoomSpeed = 2f; // ズームスピード
    public float minZoom = 3f; // 最小ズーム距離
    public float maxZoom = 20f; // 最大ズーム距離

    private float yaw = 0f;   // 左右回転（Y軸）
    private float pitch = 0f; // 上下回転（X軸）
    private float currentZoom; // 現在のズーム距離


    // Start is called before the first frame update
    void Start()
    {
        currentZoom = offset.magnitude; // 初期ズーム距離を設定

    }

    void LateUpdate()
    {
        if (target == null) return;

       // ボールの位置にオフセットを加えた位置へ、なめらかに移動
       //Vector3 desiredPosition = target.position + offset;
       // Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
       // transform.position = smoothedPosition;

       // ボールの方向を見る（任意）
       // transform.LookAt(target);





        if (Camera.main == null) return;

        // カメラの方向に向ける
        transform.forward = Camera.main.transform.forward;

        if (target == null) return;

        // 視点回転（方向キー）
        if (Input.GetKey(KeyCode.LeftArrow)) yaw -= rotateSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow)) yaw += rotateSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.UpArrow)) pitch -= rotateSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.DownArrow)) pitch += rotateSpeed * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, -45f, 45f); // 上下回転制限
        // ズームイン・アウト（マウスホイール）
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // オフセットをズーム距離に応じて更新
        Vector3 zoomedOffset = offset.normalized * currentZoom;

        // カメラ位置を計算
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = target.position + rotation * zoomedOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // ターゲットを見る
        transform.LookAt(target);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Translate(0, 0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(0, 0, -1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(-1, 0, 0);
        }

       
            float rotateSpeed = 200f; // 回転スピード

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                // 左方向に回転（Y軸）
                transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                // 右方向に回転（Y軸）
                transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                // 上方向に回転（X軸）
                transform.Rotate(-rotateSpeed * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                // 下方向に回転（X軸）
                transform.Rotate(rotateSpeed * Time.deltaTime, 0, 0);
            }
    }
}

public class CameraScript : MonoBehaviour
{
    void LateUpdate()
    {
    }
}
