using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_AirBall : MonoBehaviour
{

    [SerializeField, Header("�U����")]
    private float AttackPower = 1.0f;
    [SerializeField, Header("����")]
    private float AttackSpeed = 1.0f;


    /// <summary>
    /// Power
    /// </summary>
    public float Power
    {
        get
        {
            return AttackPower;
        }
    }

    private void FixedUpdate()
    {
        //�����ʒu����O�����ɔ���
        transform.position += transform.forward * AttackSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool GimmickHit = collision.gameObject.tag == "Burst";
        
        if (GimmickHit)
        {

            CS_Burst_of_object burst = collision.transform.GetComponent<CS_Burst_of_object>();
            if (burst == null) { Debug.LogWarning("null component"); return; }
            burst.HitDamage(Power);

            //---------------------------------------------------------------
            // �Փ˂����I�u�W�F�N�g�ɍU���͂����Z����H�I�u�W�F�N�g���ł��H
            //GetComponent<�Փ˂����I�u�W�F�N�g�̃R���|�[�l���g>.�ϋv�l;
            //�ϋv�l - AttackPower;
            //���Ȃ炱��Ȋ����H
            
            Destroy(this.gameObject);   //�Փ˂����玩�M��j��
        }
        
    }

}
