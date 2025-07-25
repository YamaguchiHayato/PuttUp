using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScpirt : MonoBehaviour
{
    public Transform target; // �ǂ�������{�[���i�v���C���[�j
    public Vector3 offset = new Vector3(0, 5, -10); // �J�����̈ʒu�̂���
    public float smoothSpeed = 0.125f; // �J�����̒Ǐ]�X�s�[�h

    // Start is called before the first frame update
    void Start()
    {

    }

    void LateUpdate()
    {
        if (target == null) return;

        // �{�[���̈ʒu�ɃI�t�Z�b�g���������ʒu�ցA�Ȃ߂炩�Ɉړ�
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // �{�[���̕���������i�C�Ӂj
        transform.LookAt(target);



      

        if (Camera.main == null) return;

        // �J�����̕����Ɍ�����
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