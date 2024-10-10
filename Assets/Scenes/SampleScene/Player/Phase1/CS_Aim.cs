using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class CS_Aim : MonoBehaviour
{
    //**
    //* �ϐ�
    //**

    // �O���I�u�W�F�N�g
    [Header("�O���I�u�W�F�N�g")]
    public Transform target; // �ǔ��Ώ�
    public Transform focus; // �ǔ��Ώ�
    public CS_InputSystem inputSystem;// �C���v�b�g�}�l�[�W���[


    // �ړ��E��]
    [Header("�ʒu")]
    public Vector3 offsetPos = new Vector3(0, 8, 0);// �ʒu
    public Vector3 offsetFocus = new Vector3(0, 3, 0);// �œ_
    [Header("�ړ�/��]�̑���")]
    public float wideSpeed = 2.0f;             // ���X�s�[�h
    public float hydeSpeed = 0.25f;             // �c�X�s�[�h
    public float rotationSpeed = 50.0f;         // ��]�X�s�[�h
    [Header("�ړ�/��]�̐���")]
    public float rotationLimitMax = 80.0f; // X����]�̐����i�ő�j
    public float rotationLimitMin = 10.0f; // X����]�̐����i�ŏ��j
    private float cameraRotX = 0.0f;     // X����]�̈ړ���
    private float cameraRotY = 0.0f;       // Y���]�̈ړ���

    // ���g�̃R���|�[�l���g
    private CinemachineVirtualCamera camera;

    [Header("���R�C���ݒ�")]
    public float recoilAmount = 1f; // ���R�C���̋���
    public float recoilDuration = 0.1f; // ���R�C���̎�������
    public float returnSpeed = 0.1f; // ���ɖ߂鑬��
    private Quaternion originalRotation;
    private float recoilTimer = 0f;

    //**
    //* ������
    //**
    void Start()
    {
        // �J�����R���|�[�l���g���擾
        camera = GetComponent<CinemachineVirtualCamera>();
    }

    //**
    //* �X�V
    //**
    void FixedUpdate()
    {
        // ���͂ɉ����ăJ��������]������

        if(camera.Priority == 10)
        {
            // �E�X�e�B�b�N�̓��͂��擾
            Vector2 stick = inputSystem.GetRightStick();

            // �J��������͂ɉ����ĉ�]
            Vector3 rotVec = new Vector3(0, stick.x, 0);
            rotVec = rotVec.normalized;
            Vector3 rot = target.rotation.eulerAngles;
            rot += wideSpeed * rotVec;
            target.rotation = Quaternion.Euler(rot);

            // �^�[�Q�b�g�̔w�ʂɃJ������z�u
            Vector3 targetPosition = target.position - target.forward;
            transform.position = targetPosition + offsetPos;

            UpdateCameraRotation();

            offsetFocus.y += hydeSpeed * stick.y;
        }
        else
        {
            offsetFocus = new Vector3(0, 0, 0);
        }
    }

    public void TriggerRecoil()
    {
        originalRotation = transform.localRotation;
        recoilTimer = recoilDuration; // ���R�C�����g���K�[
    }

    //**
    //* �J�����̉�]���X�V����
    //* in�F����
    //* out�F����
    //**
    void UpdateCameraRotation()
    {
        Vector3 directionToTarget = (focus.position + offsetFocus) - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (recoilTimer > 0)
        {
            // ���R�C���̌���
            Quaternion recoilRotation = Quaternion.Euler(-recoilAmount, 0, 0);
            transform.localRotation *= recoilRotation; // ���R�C����K�p
            recoilTimer -= Time.deltaTime; // �^�C�}�[������
        }
        else
        {
            // ���ɖ߂�
            transform.localRotation = Quaternion.Lerp(transform.localRotation, originalRotation, returnSpeed * Time.deltaTime);
        }
    }
}
