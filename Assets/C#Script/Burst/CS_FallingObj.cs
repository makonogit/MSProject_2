using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����I�u�W�F�N�g
/// </summary>
public class CS_FallingObj : MonoBehaviour
{
    [SerializeField, Header("����Effect")]
    private GameObject BreakEffect;
    [SerializeField]
    private GameObject BreakDust;

    /// <summary>
    /// ���ƐڐG��������ł���
    /// </summary>
    /// <param �Փ˕�="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        //�����ƐڐG���������
        Destroy(this.gameObject);

        Vector3 pos = transform.position;
        Instantiate(BreakEffect,pos,Quaternion.identity);
        //Instantiate(BreakDust,pos,Quaternion.identity);

        //�v���C���[�ƐڐG������
        bool HitFloor = collision.gameObject.tag == "Player";
        if (HitFloor)
        {
            //Destroy(this.gameObject);
        }
    }
}
