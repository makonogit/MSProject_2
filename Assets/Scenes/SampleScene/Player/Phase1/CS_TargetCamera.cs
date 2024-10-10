using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// ���b�N�I���p�J����
public class CS_TargetCamera : MonoBehaviour
{
    [Header("�ݒ�")]
    public Transform center;            // �����蔻��̒��S
    public float detectionRadius = 20f; // �����蔻��̔��a
    public string targetTag = "Enemy";  // �Ώۂ̃^�O
    public LayerMask targetLayer;       // �ΏۃI�u�W�F�N�g�̃��C���[

    // �^�[�Q�b�g���̃I�u�W�F�N�g��ۑ�
    private GameObject closest;

    // ���g�̃R���|�[�l���g
    private CinemachineTargetGroup cinemachineTargetGroup;

    private void Start()
    {
        // CinemachineTargetGroup�R���|�[�l���g���擾
        cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();
    }

    void Update()
    {
        // �͈͓��ŏ����ɍ����ł��߂��I�u�W�F�N�g������
        closest = FindClosest();

        if (closest != null)
        {
            // �^�[�Q�b�g�����������ꍇ�A�ǉ�
            AddTarget(closest.transform);
        }
        else
        {
            // �^�[�Q�b�g��������Ȃ������ꍇ�A�����̃^�[�Q�b�g���폜
            RemoveTarget();
        }
    }

    public GameObject GetClosest()
    {
        return closest;
    }

    //**
    //* �^�[�Q�b�g��ǉ�����
    //* 
    //* in: �ǉ�����^�[�Q�b�g
    //* out: �Ȃ�
    //**

    void AddTarget(Transform target)
    {
        // �����̃^�[�Q�b�g���擾
        var existingTargets = cinemachineTargetGroup.m_Targets;

        // �V�����^�[�Q�b�g��ǉ�
        existingTargets[1] = new CinemachineTargetGroup.Target
        {
            target = target, // �^�[�Q�b�g��ݒ�
            weight = 1f,     // �E�F�C�g��ݒ�
            radius = 2f      // ���a��ݒ�
        };
    }

    //**
    //* �^�[�Q�b�g����������
    //* 
    //* in: �Ȃ�
    //* out: �Ȃ�
    //**

    void RemoveTarget()
    {
        // �^�[�Q�b�g����ɐݒ�
        var existingTargets = cinemachineTargetGroup.m_Targets;
        existingTargets[1].target = null;
    }

    //**
    //* �͈͓���������ɍ����I�u�W�F�N�g���擾����
    //* 
    //* in: �Ȃ�
    //* out: �ł��߂��I�u�W�F�N�g
    //**

    private GameObject FindClosest()
    {
        // �w�肵���͈͓��̃R���C�_�[���擾
        Collider[] hitColliders = Physics.OverlapSphere(center.position, detectionRadius, targetLayer);
        GameObject closest = null; // �ł��߂��I�u�W�F�N�g��������
        float closestDistance = Mathf.Infinity; // �����𖳌���ŏ�����

        foreach (Collider collider in hitColliders)
        {
            // �^�O����v���邩�m�F
            if (collider.CompareTag(targetTag))
            {
                // ���S����̋������v�Z
                float distance = Vector3.Distance(center.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance; // �ŒZ�������X�V
                    closest = collider.gameObject; // �ł��߂��I�u�W�F�N�g��ۑ�
                }
            }
        }

        return closest; // �ł��߂��I�u�W�F�N�g��Ԃ�
    }

    // �f�o�b�O�p�ɔ͈͂�����
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Gizmo�̐F��Ԃɐݒ�
        Gizmos.DrawWireSphere(center.position, detectionRadius); // �͈͂�`��
    }
}

