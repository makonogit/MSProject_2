using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

using Cinemachine;

// TPS�J����
public class CS_TpsCamera : MonoBehaviour
{
    //**
    //* �ϐ�
    //**

    // �O���I�u�W�F�N�g
    [Header("�O���I�u�W�F�N�g")]
    public Transform target; // �ǔ��Ώ�
    public CS_InputSystem inputSystem;// �C���v�b�g�}�l�[�W���[


    // �ړ��E��]
    [Header("�ʒu")]
    public Vector3 offsetPos = new Vector3 (0, 8, 0);// �ʒu
    public Vector3 offsetFocus = new Vector3(0, 3, 0);// �œ_
    [Header("�ړ�/��]�̑���")]
    public float moveSpeed = 50.0f;             // �ړ��X�s�[�h
    public float rotationSpeed = 50.0f;         // ��]�X�s�[�h
    [Header("�ړ�/��]�̐���")]
    public float rotationLimitMax = 80.0f; // X����]�̐����i�ő�j
    public float rotationLimitMin = 10.0f; // X����]�̐����i�ŏ��j
    private float cameraRotX = 45.0f;     // X����]�̈ړ���
    private float cameraRotY = 0.0f;       // Y���]�̈ړ���

    // ���g�̃R���|�[�l���g
    private CinemachineVirtualCamera camera; 

    //**
    //* ������
    //**
    void Start()
    {
        // �J�����R���|�[�l���g���擾
        camera = GetComponent<CinemachineVirtualCamera>();

        // �J�����������ʒu�ɃZ�b�g
        CameraReset();
    }

    //**
    //* �X�V
    //**
    void FixedUpdate()
    {
        // ���͂ɉ����ăJ��������]������

        if (camera.Priority == 10)
        {
            // �E�X�e�B�b�N�̓��͂��擾
            Vector2 stick = inputSystem.GetRightStick();

            // �J��������͂ɉ����Ĉړ�
            Vector3 rotVec = new Vector3(stick.y, stick.x, 0);
            rotVec = rotVec.normalized;
            MoveCamera(rotVec);
        }
    }

    //**
    //* �J�����̈ړ��ʂ����Z�b�g����
    //*
    //* in�F����
    //* out�F����
    //**
    public void CameraReset()
    {
        // �^�[�Q�b�g�̔w�ʂɃJ������z�u
        Vector3 targetPosition = target.position - target.forward;
        transform.position = targetPosition + offsetPos;
    }

    //**
    //* �J�������^�[�Q�b�g�𒆐S�Ɉړ�������
    //*
    //* in�F�ړ����������
    //* out�F����
    //**
    void MoveCamera(Vector3 direction)
    {
        // �J�����̈ʒu���X�V
        UpdateCameraPosition(direction);

        // �J�����̊p�x���X�V
        UpdateCameraRotation();
    }

    //**
    //* �J�����̈ʒu���X�V����
    //*
    //* in�F�ړ����������
    //* out�F����
    //**
    void UpdateCameraPosition(Vector3 direction)
    {
        // �ړ����x
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // �ړ�����
        Vector3 normalizedDirection = direction.normalized;

        // X���̉�]���X�V
        if (Mathf.Abs(Vector3.Dot(normalizedDirection, Vector3.right)) > 0.1f)
        {
            cameraRotX += rotationAmount * Mathf.Sign(Vector3.Dot(normalizedDirection, Vector3.right));
            cameraRotX = Mathf.Clamp(cameraRotX, rotationLimitMin, rotationLimitMax);
        }

        // Y���̉�]���X�V
        if (Mathf.Abs(Vector3.Dot(normalizedDirection, Vector3.up)) > 0.1f)
        {
            cameraRotY += rotationAmount * Mathf.Sign(Vector3.Dot(normalizedDirection, Vector3.up));
        }

        // �J�����̈ʒu�����炩�ɍX�V
        Quaternion rotation = Quaternion.Euler(cameraRotX, cameraRotY, 0);
        Vector3 offset = rotation * offsetPos;
        Vector3 desiredPosition = target.position + offset + offsetFocus;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 10.0f * Time.deltaTime);
    }

    //**
    //* �J�����̉�]���X�V����
    //* in�F����
    //* out�F����
    //**
    void UpdateCameraRotation()
    {
        // �^�[�Q�b�g�̕����������悤�ɃJ��������]������
        Vector3 directionToTarget = (target.position + offsetFocus) - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}


