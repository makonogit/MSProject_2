////------------------------------------------
//// ��C�C/���h���̊֐���`
//// ���ƂŃv���C���[�ɍ���
////------------------------------------------
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CS_AirGun : MonoBehaviour
//{
//    [SerializeField, Header("��C�C�̍U����")]
//    private float AirAttackPowar = 1.0f;

//    [SerializeField, Header("��C�C�̒e�I�u�W�F�N�g")]
//    private GameObject AirBall;

//    [SerializeField, Header("���h���̒����Ԋu")]
//    [Header("���U����/�����Ԋu")]
//    private const float Injection_Interval = 0.5f;

//    private const float MaxPressure = 3.0f; //�ő刳��

//    //�����Ԋu�v�Z�p
//    private float Injection_IntarvalTime = 0.0f;

//    private void FixedUpdate()
//    {
//        //����Ă݂�
//        //AirGun(KeyCode.E, false);
//    }


//    //**
//    //* �����t�@�C�����Đ�����Ă��邩
//    //*
//    //* in�F����
//    //* out:�Đ�����Ă��邩 bool
//    //**

//    //bool IsPlayingSound(int indexSource)
//    //{
//    //    return audioSource[indexSource].isPlaying;
//    //}


//    //----------------------------
//    // ��C�C�֐�
//    // ����:���̓L�[,�I�u�W�F�N�g�ɋ߂Â��Ă��邩
//    // �߂�l�F�Ȃ�
//    //----------------------------
//    void AirGun(string button)
//    {
//        //���ˉ\��(�L�[�������ꂽ�u��&�I�u�W�F�N�g�ɋ߂Â��Ă��Ȃ�)
//        bool StartShooting = Input.GetButtonDown(button); //&& !HitBurstObjFlag;

//        if (!StartShooting) { return; }

//        //SE���Đ�����Ă�����~�߂�
//        //if (IsPlayingSound(1)) { StopPlayingSound(1); }

//        //PlaySoundEffect(1, 3);

//        Vector3 forwardVec = transform.forward; // = cameraTransform.forward;

//        //���͂�����Βe�𐶐�
//        //�|�C���^�̈ʒu����@Instantiate(AirBall,transform.pointa);
//        GameObject ballobj = Instantiate(AirBall);

//        Vector3 pos = Vector3.zero;
//        float scaler = 2.0f;
//        Vector3 offset = new Vector3(0, 1, 0);

//        pos = this.transform.position;
//        pos += offset;
//        pos += forwardVec * scaler;
//        ballobj.transform.position = pos;
//        ballobj.transform.forward = forwardVec;

//    }


//    //----------------------------
//    // ���h��(��C����)�֐�
//    // ����:���̓L�[
//    // �߂�l�F�Ȃ�
//    //----------------------------
//    void AirInjection(string button)
//    {

//        ////�����\��(�L�[�����͂���Ă��ăI�u�W�F�N�g�ɋ߂Â��Ă���)
//        //bool Injection = Input.GetButtonDown(button) && HitBurstObjFlag;

//        //if (!Injection)
//        //{
//        //    return;
//        //}
//        //else
//        //{
//        //    StopPlayingSound(1);    //�������Ă�����~�߂�
//        //    PlaySoundEffect(1, 4);  //�}��SE
//        //    InjectionState = true;  //�������̃t���O��On
//        //}

//        ////����������Ȃ���ΏI��
//        //if (!InjectionState)
//        //{
//        //    return;
//        //}

//        //PlaySoundEffect(1, 6);  //�}��SE

//        ////�{�^���������ꂽ or �Ώۂ����ł�����I��
//        //InjectionState = !(Input.GetButtonUp(button) || !csButstofObj);

//        ////���Ԍv��
//        //Injection_IntarvalTime += Time.deltaTime;
//        //bool TimeProgress = Injection_IntarvalTime > Injection_Interval;   //�����Ԋu�����Ԍo�߂��Ă��邩
//        //if (!TimeProgress) { return; }

//        //Injection_IntarvalTime = 0.0f;  //���Ԃ����Z�b�g

//        //if (!csButstofObj)
//        //{
//        //    Debug.LogWarning("null");
//        //    return;
//        //}

//        ////���͂��ő�ɂȂ����火
//        //bool MaxPressure = true;
//        //csButstofObj.AddPressure(InjectionPower);

//        ////�ő�ɂȂ����璍���I��
//        //if (MaxPressure)
//        //{
//        //    InjectionState = false;
//        //}
//    }

//}
