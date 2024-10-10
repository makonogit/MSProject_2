using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����M�~�b�N
/// �S���F��
/// </summary>
public class CS_Falling : MonoBehaviour
{
    [SerializeField, Header("�������������STrans")]
    private Transform CenterTrans;

    [SerializeField, Header("�����I�u�W�F�N�g�v���n�u")]
    private List<GameObject> FallingObj;

    [Header("�����v�Z�p")]
    [SerializeField,Tooltip("�����������Ԋu")]
    private float FallingSpace;
    [SerializeField, Tooltip("������C��R\n�傫���قǗ�����̒x���Ȃ��")]
    private float FallingDrag;
    [SerializeField, Tooltip("�����������͈�(���a)")]
    private float FallingRange;

    //���Ԍv���p
    private float TimeMeasure = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //���Ԍv��
        TimeMeasure += Time.deltaTime;
        bool CreateTime = TimeMeasure > FallingSpace;

        if (CreateTime)
        {
            //�����I�u�W�F�N�g�����߂�
            int CreateObj = Random.Range(0, FallingObj.Count);

            //�͈͓��̃����_���ȍ��W���w��
            Vector3 FallingPos = Random.insideUnitCircle * FallingRange;
            FallingPos.x += CenterTrans.position.x;
            FallingPos.z += CenterTrans.position.z;
            FallingPos.y = CenterTrans.position.y + 5.0f;   //�����ʒu�Ƃ肠��������+5
            //����
            GameObject obj = Instantiate(FallingObj[CreateObj]);
            obj.transform.position = FallingPos;
            
            Rigidbody objrigid = obj.GetComponent<Rigidbody>();
            objrigid.drag = FallingDrag;

            //�Ȃ񂩉�]�����Ă݂�
            objrigid.AddTorque(1, 0.5f, 0);
            
            //���̐����܂Ŏ��Ԃ����Z�b�g
            TimeMeasure = 0.0f;
        }
    }
}
