using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// �����̃J�����𐧌䂷��
public class CS_CameraManager : MonoBehaviour
{
    //**
    //* �ϐ�
    //**

    // �O���I�u�W�F�N�g
    [Header("�O���I�u�W�F�N�g")]
    public CinemachineVirtualCamera[] virtualCameras;// �J�����̃��X�g

    // ���g�̃R���|�[�l���g
    private CinemachineImpulseSource impulseSource;// �U��

    //**
    //* ������
    //**

    void Start()
    {
        // ���g�̃R���|�[�l���g���擾
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    //**
    //* �X�V
    //**

    void Update()
    {

    }

    //**
    //* �J������U��������
    //*
    //* in:����
    //* out:����
    //**

    public void ShakeCamera(float amplitude, float frequency)
    {
        // Impulse Source�̐ݒ�
        impulseSource.m_ImpulseDefinition.m_AmplitudeGain = amplitude;// �U���̑傫��
        impulseSource.m_ImpulseDefinition.m_FrequencyGain = frequency;// �U�������鎞��

        // �U�����J�n
        impulseSource.GenerateImpulse();
    }

    //**
    //* �J������؂�ւ���
    //*
    //* in:�؂�ւ���
    //* out:����
    //**

    public void SwitchingCamera(int index)
    {
        // �S�ẴJ������Priority��������
        foreach (var virtualCamera in virtualCameras)
        {
            virtualCamera.Priority = -1;
        }
        // �Ώۂ̃J������Priority���グ��
        virtualCameras[index].Priority = 10;
    }
}
