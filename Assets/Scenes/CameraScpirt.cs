using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScpirt : MonoBehaviour
{
    public Transform target; // 追いかけるボール（プレイヤー）
    public Vector3 offset = new Vector3(0, 5, -10); // カメラの位置のずれ
    public float smoothSpeed = 0.125f; // カメラの追従スピード

    // Start is called before the first frame update
    void Start()
    {

    }

    void LateUpdate()
    {
        if (target == null) return;

        // ボールの位置にオフセットを加えた位置へ、なめらかに移動
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // ボールの方向を見る（任意）
        transform.LookAt(target);



      

        if (Camera.main == null) return;

        // カメラの方向に向ける
        transform.forward = Camera.main.transform.forward;
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
    }
}