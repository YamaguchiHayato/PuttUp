using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class DragShoot : MonoBehaviour
{
    private Rigidbody rb; // �{�[���ɕt���Ă���Rigidbody

    private Vector3 dragStartPos; // �}�E�X���������ʒu�i���[���h���W�j
    private Vector3 dragEndPos;   // �}�E�X�𗣂����ʒu�i���[���h���W�j

    public const float forceMultiplier = 5.0f; // �͂̔{���i�h���b�O�����Ɋ|����j
    public const float maxForce = 20.0f;       // �͂̏���i�h���b�O�������Ă�����ȏ�ɂȂ�Ȃ��j

    private const float stopThreshold = 1.0f;
    private bool isDragging = false; // �h���b�O�����ǂ����̃t���O


    void Start()
    {
        // Rigidbody �R���|�[�l���g�̎擾
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // �{�[���������Ă���Ԃ͑���ł��Ȃ��悤�ɂ���
        if (IsBallMoving()) return;

        // �}�E�X���{�^�����������Ƃ��Ƀh���b�O���J�n
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }

        // �}�E�X�����������Ă���ԁA�h���b�O���t���O�� true �̂Ƃ�
        if (Input.GetMouseButton(0) && isDragging)
        {
            UpdateDrag();
        }

        // �}�E�X�{�^���𗣂����Ƃ��ɃV���b�g�����s
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            ReleaseShot();
        }
    }

    // �h���b�O�̊J�n���ɌĂ΂�鏈��
    void StartDrag()
    {
        isDragging = true;

        // �J��������}�E�X�ʒu�Ɍ����ă��C���΂�
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // �n�ʂ�I�u�W�F�N�g�ƌ��������n�_���擾
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            dragStartPos = hit.point; // �h���b�O�̊J�n�ʒu���L�^
        }
    }

    // �h���b�O���ɌĂ΂�鏈��
    void UpdateDrag()
    {
        // �����I�� UI �\������E���̕`��ȂǂɎg�p�\
    }

    // �}�E�X�𗣂��ăV���b�g�����s���鏈��
    void ReleaseShot()
    {
        isDragging = false;

        // �ēx���C�L���X�g���ďI���ʒu���擾
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            dragEndPos = hit.point;

            // �h���b�O�����i�n�_���I�_�j�ɔ��Ό����̗͂�������
            Vector3 direction = (dragStartPos - dragEndPos).normalized;

            // �����ɉ������͂��v�Z�i����t���j
            float dragDistance = Vector3.Distance(dragStartPos, dragEndPos);
            float force = Mathf.Clamp(dragDistance * forceMultiplier, 0.0f, maxForce);

            // ���ۂɃ{�[����ł�
            ShootBall(direction, force);
        }
    }

    // Rigidbody �ɗ͂������ă{�[�����΂�����
    void ShootBall(Vector3 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode.Impulse); // ��u�̗́i�C���p���X�j��������
    }

    // �{�[���������Ă��邩�ǂ����𔻒�i���̑����ȉ��Ȃ�Î~�Ƃ݂Ȃ��j
    bool IsBallMoving()
    {
        return rb.velocity.magnitude > stopThreshold;
    }

}