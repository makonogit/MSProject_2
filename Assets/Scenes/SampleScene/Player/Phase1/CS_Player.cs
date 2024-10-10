using Assets.C_Script.C_GameUI;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using Cinemachine;

//using System.Numerics;

//using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

// �v���C���[����
public class CS_Player : MonoBehaviour
{
    //**
    //* �ϐ�
    //**

    // �O���I�u�W�F�N�g
    [Header("�O���I�u�W�F�N�g")]
    [SerializeField, Header("���C���J����")]
    private Transform cameraTransform;
    [SerializeField, Header("�C���v�b�g�}�l�[�W���[")]
    private CS_InputSystem inputSystem;// �C���v�b�g�}�l�[�W���[
    [SerializeField, Header("�J�����}�l�[�W���[")]
    private CS_CameraManager cameraManager;// �J�����}�l�[�W���[
    [SerializeField, Header("���b�N�I���J����")]
    private CS_TargetCamera targetCamera;// ���b�N�I���J����
    [SerializeField, Header("�G�C���J����")]
    private CS_Aim aimCamera;// �G�C���J����
    [SerializeField, Header("�ǔ��J����")]
    private CS_TpsCamera tpsCamera;// tps�J����

    [Header("�v���C���[�ݒ�")]
    [SerializeField, Header("�̗͒l")]
    private float initHP = 100;
    [SerializeField]
    private float nowHP;

    // �W�����v
    [Header("�W�����v�ݒ�")]
    [SerializeField, Header("�W�����v��")]
    private float jumpForce = 5f;                // �W�����v��
    [SerializeField, Header("�n�ʂƂ̋���")]
    private float groundCheckDistance = 0.1f;    // �n�ʂƂ̋���
    [SerializeField, Header("�W�����v�\�ȃ��C���[")]
    private LayerMask targetLayer;               // �W�����v�\�ȃ��C���[
    [SerializeField, Header("���i�W�����v�񐔂̏����l")]
    private int initJumpStock = 1;// ���i�W�����v��
    private int jumpStock;
    private bool isJump = false;// �W�����v��

    // �ړ�
    [Header("�ړ��ݒ�")]
    [SerializeField, Header("�ړ����x")]
    private float speed = 1f;        // �ړ����x
    [SerializeField, Header("�ڕW���x")]
    private float targetSpeed = 10f; // �ڕW���x
    [SerializeField, Header("�ō����x�ɓ��B����܂ł̎���")]
    private float smoothTime = 0.5f; // �ō����x�ɓ��B����܂ł̎���
    private float velocity = 0f;    // ���݂̑��x
    private Vector3 moveVec;        // ���݂̈ړ�����
    private float initSpeed;        // �X�s�[�h�̏����l��ۑ����Ă����ϐ�

    // �Đ����鉹���t�@�C���̃��X�g
    [Header("���ʉ��ݒ�")]
    [SerializeField, Header("�I�[�f�B�I�\�[�X")]
    private AudioSource[] audioSource;
    [SerializeField, Header("�����t�@�C��")]
    private AudioClip[] audioClips;

    // ���g�̃R���|�[�l���g
    private Rigidbody rb;
    private Animator animator;

    [Header("�ˌ��ݒ�")]
    [SerializeField, Header("��C�C�̒e�I�u�W�F�N�g")]
    private GameObject AirBall;// �e
    [SerializeField, Header("���U���̏����l")]
    private int initMagazine;// ���U���̏����l
    [SerializeField, Header("���݂̑��U��")]
    private int magazine;// ���U��
    [SerializeField, Header("�A�ː�")]
    private int burstfire;// �A�ː�
    [SerializeField, Header("�c�e���̏����l")]
    private int initBulletStock;
    [SerializeField, Header("���݂̎c�e��")]
    private int bulletStock;// �c�e��
    private bool isShot = false;// �ˌ���
    [SerializeField, Header("���炷HP")]
    private float shotHp;

    [Header("UI")]
    CS_GameUIClass gameUiClass;
    [SerializeField]
    private GameObject GameOverPanal;
    [SerializeField]
    private GameObject GameClearPanal;
    [SerializeField]
    private RectTransform GageTrans;


    [SerializeField, Header("���h���̒����Ԋu")]
    [Header("���U����/�����Ԋu")]
    private const float Injection_Interval = 0.5f;

    //�����Ԋu�v�Z�p
    private float Injection_IntarvalTime = 0.0f;

    private bool HitBurstObjFlag = false;
    [SerializeField, Tooltip("���h���ς�[")]
    private int InjectionPower = 1;

    private bool InjectionState = false;

    //�e����I�u�W�F�N�g�̃X�N���v�g
    CS_Burst_of_object csButstofObj;

   

    //**
    //* ������
    //**
    void Start()
    {
        nowHP = initHP;

        gameUiClass = new CS_GameUIClass(initHP, GageTrans);

        jumpStock = initJumpStock;

        // �c�e����������
        bulletStock = initBulletStock;

        // ���U����������
        magazine = initMagazine;

        // �ړ����x�̏����l��ۑ�
        initSpeed = speed;

        // ���g�̃R���|�[�l���g���擾
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    //**
    //* �X�V
    //**

    void FixedUpdate()
    {
        // �J������������
        cameraManager.SwitchingCamera(0);

        // �ړ�����
        HandleMovement();
        // �W�����v����
        HandleJump();
        // �G�C������
        HandleAim();
        // �ˌ�����
        HandlShot();
        // �X���C�f�B���O
        HandlSliding();

        AirInjection();
        //AirGun();
        
        if(nowHP <= 0)
        {
            gameUiClass.ViewResultUI(GameOverPanal); 
        }
    }

    //**
    //* �X���C�f�B���O����
    //*
    //* in:����
    //* out:����
    //**
    void HandlSliding()
    {
        if (inputSystem.GetLeftStickPush())
        {
            animator.SetBool("Sliding", true);
        }
        else
        {
            animator.SetBool("Sliding", false);
        }
    }

    //**
    //* �ˌ�����
    //*
    //* in:����
    //* out:����
    //**
    void HandlShot()
    {
        // �ߐڔ���
        bool isAirInjection = !HitBurstObjFlag || !csButstofObj;
        // ���U������
        bool isMagazine = magazine > 0;

        // �R���g���[���[����/���˒�/�ߐ�/���U���𔻒�
        if (inputSystem.GetButtonXPressed() && !isShot && isAirInjection && animator.GetBool("Aim"))
        {
            // ���U����0�Ȃ烊���[�h
            if (isMagazine)
            {
                CreateBullet(burstfire);
                isShot = true;

                animator.SetBool("Shot", true);
            }
            else if (!animator.GetBool("Reload"))
            {
                isShot = true;
                StartCoroutine(ReloadCoroutine());
            }
        }
        else
        {
            animator.SetBool("Shot", false);
        }

        if (!inputSystem.GetButtonXPressed() && isShot)
        {
            isShot = false;
        }
    }

    //**
    //* �����[�h����
    //*
    //* in:�����[�h��
    //* out:����
    //**
    void ReloadMagazine(int reload)
    {
        if (bulletStock < reload) 
        { 
            magazine = bulletStock;
            bulletStock = 0;
        }
        else
        { 
            magazine = reload;
            bulletStock -= reload;
        }
    }
    private IEnumerator ReloadCoroutine()
    {
        // �����[�h�A�j���[�V�������J�n
        animator.SetBool("Reload", true);

        animator.SetTrigger("Reload");

        // �A�j���[�V�����̒������擾
        AnimationClip clip = animator.runtimeAnimatorController.animationClips[0];
        float animationLength = clip.length;

        // �A�j���[�V�������Đ������ǂ������m�F
        float elapsedTime = 0f;
        while (elapsedTime < animationLength)
        {
            elapsedTime += Time.deltaTime;
            yield return null; // ���̃t���[���܂őҋ@
        }

        // �����[�h�������s��
        ReloadMagazine(initMagazine);
        animator.SetBool("Reload", false);
    }

    //**
    //* �e�𐶐����鏈��
    //*
    //* in:������
    //* out:����
    //**
    void CreateBullet(int burst)
    {
        PlaySoundEffect(1, 3);

        Vector3 forwardVec = aimCamera.transform.forward;

        float offsetDistance = 1.5f; // �e�e�̊Ԋu

        if (burst > magazine)
        {
            burst = magazine;
        }

        for (int i = 0; i < burst; i++)
        {
            // �e�𐶐�
            GameObject ballobj = Instantiate(AirBall);

            Vector3 pos = this.transform.position;
            Vector3 offset = new Vector3(0, 1, 0);

            pos += offset;
            pos += forwardVec * (offsetDistance * (i + (burst - 1) / 2f));

            ballobj.transform.position = pos;
            ballobj.transform.forward = forwardVec;

            // ���U�������炷
            magazine--;
            gameUiClass.ResourceChange(ref nowHP, -shotHp, GageTrans);

            aimCamera.TriggerRecoil();
        }
    }



    //**
    //* �Ə�����
    //*
    //* in:����
    //* out:����
    //**
    void HandleAim()
    {
        // L�g���K�[�̓��͂�����ꍇ�A�^�[�Q�b�g�J�����ɐ؂�ւ���
        if (inputSystem.GetLeftTrigger() > 0.1f)
        {
            if (animator.GetBool("Aim") == false)
            {
                // �ǔ��J�����̕����Ɍ�����
                Vector3 normalizedDirection = cameraTransform.forward;
                float yRotation = Mathf.Atan2(normalizedDirection.x, normalizedDirection.z) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0, yRotation, 0);
                //transform.rotation = rotation;

                // ��]�ɃI�t�Z�b�g��K�p
                float offsetAngle = 45f;
                Quaternion offsetRotation = Quaternion.Euler(0, offsetAngle, 0);
                transform.rotation = rotation * offsetRotation;
            }

            cameraManager.SwitchingCamera(2);
            animator.SetBool("Aim", true);

            // �ړ����x��������
            speed = initSpeed;

            // �A�j���[�^�[�̒l��ύX
            animator.SetBool("Dash", false);
        }
        else
        {
            if (animator.GetBool("Aim") == true)
            {
                tpsCamera.CameraReset();
            }

            animator.SetBool("Aim", false);
        }
    }

    //**
    //* ���b�N�I������
    //*
    //* in:����
    //* out:����
    //**
    void HandleTarget()
    {
        // �^�[�Q�b�g���̃I�u�W�F�N�g���擾
        GameObject closest = targetCamera.GetClosest();

        // L�g���K�[�̓��͂�����ꍇ�A�^�[�Q�b�g�J�����ɐ؂�ւ���
        if ((inputSystem.GetLeftTrigger() > 0.1f) &&(closest != null))
        {
            cameraManager.SwitchingCamera(1);
            animator.SetBool("LockOn", true);
        }
        else
        {
            animator.SetBool("LockOn", false);

        }
    }
    // IK Pass
    private void OnAnimatorIK(int layerIndex)
    {
        if (animator.GetBool("Aim"))
        {
            // �����^�[�Q�b�g�Ɍ�����
            animator.SetLookAtWeight(1f, 0.3f, 1f, 0f, 0.5f);
            animator.SetLookAtPosition(aimCamera.transform.forward * 100f);
        }

        if (!animator.GetBool("LockOn")) return;

        // �^�[�Q�b�g���̃I�u�W�F�N�g���擾
        GameObject closest = targetCamera.GetClosest();

        // ���Ƙr���^�[�Q�b�g�̕����Ɍ�����
        if (closest != null)
        {
            // �����^�[�Q�b�g�Ɍ�����
            animator.SetLookAtWeight(1f, 0.3f, 1f, 0f, 0.5f);
            animator.SetLookAtPosition(closest.transform.position);

            // �E�r���^�[�Q�b�g�Ɍ�����
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
            animator.SetIKPosition(AvatarIKGoal.RightHand, closest.transform.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, closest.transform.rotation);
        }
    }

    //**
    //* �ړ�����
    //*
    //* in�F����
    //* out�F����
    //**
    void HandleMovement()
    {
        // L�X�e�b�N�̓��͂��`�F�b�N
        if (inputSystem.GetLeftStickActive())
        {
            // �X�e�B�b�N�̓��͂��擾
            Vector3 moveVec = GetMovementVector();

            // �ʒu���X�V
            MoveCharacter(moveVec);

            // �O���������X���[�Y�ɒ���
            AdjustForwardDirection(moveVec);
            animator.SetBool("Move", true);
        }
        else
        {
            // 0�ԃC���f�b�N�X�̌��ʉ����~
            StopPlayingSound(0);

            // �ړ����x��������
            speed = initSpeed;

            // �A�j���[�^�[�̒l��ύX
            animator.SetBool("Move", false);
            animator.SetBool("Dash", false);
        }
    }

    //**
    //* �W�����v����
    //*
    //* in�F����
    //* out�F����
    //**
    void HandleJump()
    {
        bool isJumpStock = jumpStock > 0;

        // �ڒn����ƏՓˏ�ԂƃW�����v�{�^���̓��͂��`�F�b�N
        //if (IsGrounded() && inputSystem.GetButtonAPressed() && !animator.GetBool("Jump") && !isJump)
        if (inputSystem.GetButtonAPressed() && !isJump && isJumpStock)
        {
            // ���ʉ����Đ�
            PlaySoundEffect(1, 1);

            // �W�����v�͂�������
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // �A�j���[�^�[�̒l��ύX
            animator.SetBool("Jump", true);

            isJump = true;
        }
        else
        {
            // �A�j���[�^�[�̒l��ύX
            animator.SetBool("Jump", false);
        }

        if (!inputSystem.GetButtonAPressed() && isJump)
        {
            isJump = false;
            jumpStock--;
        }

        if (IsGrounded())
        {
            jumpStock = initJumpStock;
        }
    }

    //**
    //* �X�e�B�b�N���͂���J�������猩���ړ��x�N�g�����擾����
    //*
    //* in�F����
    //* out�F�ړ��x�N�g��
    //**
    Vector3 GetMovementVector()
    {
        Vector2 stick = inputSystem.GetLeftStick();
        Vector3 forward = cameraTransform.forward;
        // Aim��
        if (animator.GetBool("Aim"))
        {
            forward = aimCamera.transform.forward;// Aim�J�������Q��
            stick = RoundDirection(stick);        // �X�e�B�b�N���͂𕽍s�ړ��ɕϊ�
        }
        Vector3 right = cameraTransform.right;
        Vector3 moveVec = forward * stick.y + right * stick.x;
        moveVec.y = 0f; // y ���̈ړ��͕s�v
        return moveVec.normalized;
    }

    Vector2 RoundDirection(Vector2 input)
    {
        // x����y���̐�Βl���傫������D�悵�ĕ���������
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            return input.x > 0 ? Vector2.right : Vector2.left;
        }
        else
        {
            return input.y > 0 ? Vector2.up : Vector2.down;
        }
    }

    //**
    //* �L�����N�^�[�̈ʒu���X�V����
    //*
    //* in�F�ړ��x�N�g��
    //* out�F����
    //**
    void MoveCharacter(Vector3 moveVec)
    {
        // L�g���K�[�̓��͒��͉�������
        float tri = inputSystem.GetRightTrigger();
        if ((tri > 0) && (animator.GetBool("Aim") == false))
        {
            speed = Mathf.SmoothDamp(speed, targetSpeed, ref velocity, smoothTime);

            animator.SetBool("Dash", true);
        }

        // ���ʉ����Đ�����
        if (animator.GetBool("Dash"))
        {
            // �_�b�V��
            PlaySoundEffect(0, 2);
        }
        else if (animator.GetBool("Move"))
        {
            // �ړ�
            PlaySoundEffect(0, 0);
        }

        // �v���C���[�̈ʒu���X�V
        Vector3 direction = moveVec * speed * Time.deltaTime;
        rb.MovePosition(rb.position + direction);
    }

    //**
    //* �L�����N�^�[��i�s�����Ɍ�����
    //*
    //* in�F�ړ��x�N�g��
    //* out�F����
    //**
    void AdjustForwardDirection(Vector3 moveVec)
    {
        if (animator.GetBool("Aim")) return;

        if (moveVec.sqrMagnitude > 0)
        {
            Vector3 targetForward = moveVec.normalized;
            transform.forward = Vector3.Lerp(transform.forward, targetForward, 0.1f);
        }
    }

    //**
    //* �n�ʂɐڒn���Ă��邩�𔻒f����
    //*
    //* in�F����
    //* out�F�ڒn����
    //**
    bool IsGrounded()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, Vector3.down, out hit, 0.01f, targetLayer);
    }

    //**
    //* �����t�@�C�����Đ��\���`�F�b�N����
    //*
    //* in�F�Đ����鉹���t�@�C���̃C���f�b�N�X
    //* out:�Đ��\���`�F�b�N
    //**
    bool CanPlaySound(int indexSource, int indexClip)
    {
        return audioSource[indexSource] != null
               && audioClips != null
               && indexClip >= 0
               && indexClip < audioClips.Length
               && (!audioSource[indexSource].isPlaying || audioSource[indexSource].clip != audioClips[indexClip]);
    }

    //**
    //* �����t�@�C�����Đ�����
    //*
    //* in�F�Đ����鉹���t�@�C���̃C���f�b�N�X
    //* out:����
    //**
    void PlaySoundEffect(int indexSource,int indexClip)
    {
        // �T�E���h�G�t�F�N�g���Đ�
        if (CanPlaySound(indexSource, indexClip))
        {
            audioSource[indexSource].clip = audioClips[indexClip];
            audioSource[indexSource].Play();
        }
    }

    //**
    //* �����t�@�C�����~����
    //*
    //* in�F����
    //* out:����
    //**
    void StopPlayingSound(int indexSource)
    {
        // �T�E���h�G�t�F�N�g���~
        if (audioSource[indexSource].isPlaying)
        {
            audioSource[indexSource].Stop();
        }
    }

    bool IsPlayingSound(int indexSource)
    {
        return audioSource[indexSource].isPlaying;
    }
    //----------------------------
    // ��C�C�֐�
    // ����:���̓L�[,�I�u�W�F�N�g�ɋ߂Â��Ă��邩
    // �߂�l�F�Ȃ�
    //----------------------------
    void AirGun()
    {
        animator.SetBool("Shot", false);

        //���ˉ\��(�L�[�������ꂽ�u��&�I�u�W�F�N�g�ɋ߂Â��Ă��Ȃ�)
        bool StartShooting = inputSystem.GetButtonXTriggered() && (!HitBurstObjFlag || !csButstofObj);

        if (!StartShooting) { return; }
        if (!animator.GetBool("Aim")) { return; }

        //SE���Đ�����Ă�����~�߂�
        if (IsPlayingSound(1)) { StopPlayingSound(1); }

        PlaySoundEffect(1, 3);

        Vector3 forwardVec = aimCamera.transform.forward;//cameraTransform.forward;

        //���͂�����Βe�𐶐�
        //�|�C���^�̈ʒu����@Instantiate(AirBall,transform.pointa);
        GameObject ballobj = Instantiate(AirBall);

        Vector3 pos = Vector3.zero;
        float scaler = 0.5f;
        Vector3 offset = new Vector3(0, 1, 0);

        pos = this.transform.position;
        pos += offset;
        pos += forwardVec * (scaler * 2f);
        ballobj.transform.position = pos;
        ballobj.transform.forward = forwardVec;

        aimCamera.TriggerRecoil();
        animator.SetBool("Shot", true);
    }

    //----------------------------
    // ���h��(��C����)�֐�
    // ����:���̓L�[,�߂Â��Ă��邩,�߂Â��Ă���I�u�W�F�N�g�̈���,�߂Â��Ă���I�u�W�F�N�g�̑ϋv�l
    // �߂�l�F�Ȃ�
    //----------------------------
    void AirInjection()
    {
        //�����\��(�L�[�����͂���Ă��ăI�u�W�F�N�g�ɋ߂Â��Ă���)
        bool Injection = inputSystem.GetButtonBPressed() && HitBurstObjFlag && csButstofObj;

        if (Injection)
        {
            StopPlayingSound(1);    //�������Ă�����~�߂�
            PlaySoundEffect(1, 4);  //�}��SE
            InjectionState = true;  //�������̃t���O��On
            gameUiClass.ResourceChange(ref nowHP, -1, GageTrans);
        }

        //����������Ȃ���ΏI��
        if (!InjectionState)
        {
            return;
        }

        //���Ԍv��
        Injection_IntarvalTime += Time.deltaTime;
        bool TimeProgress = Injection_IntarvalTime > Injection_Interval;   //�����Ԋu�����Ԍo�߂��Ă��邩
        if (!TimeProgress) { return; }

        Injection_IntarvalTime = 0.0f;  //���Ԃ����Z�b�g

        if (!csButstofObj)
        {
            //Debug.LogWarning("null");
            return;
        }

        PlaySoundEffect(1, 6);  //�}��SE

        //���͂��ő�ɂȂ����� or �{�^���𗣂�����
        bool MaxPressure = !inputSystem.GetButtonBPressed() || csButstofObj.AddPressure(InjectionPower);

        //�ő�ɂȂ����璍���I��
        if (MaxPressure)
        {
            StopPlayingSound(1);
            PlaySoundEffect(1, 5);
            InjectionState = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        bool isHitBurstObj = collision.gameObject.tag == "Burst";
        if (isHitBurstObj)
        {
            csButstofObj = collision.transform.GetComponent<CS_Burst_of_object>();
            HitBurstObjFlag = true;
            return;
        }

        // �Փ˂����I�u�W�F�N�g�̃^�O���`�F�b�N
        if (collision.gameObject.tag == "Item")
        {
            float itempoint = collision.gameObject.GetComponent<CS_Item>().POINT;

            gameUiClass.ResourceChange(ref nowHP, itempoint, GageTrans);

            Destroy(collision.gameObject);

        }
        else if (collision.gameObject.tag == "Enemy")
        {
            gameUiClass.ResourceChange(ref nowHP, -1, GageTrans);
        }
        else if (collision.gameObject.tag == "Goal")
        {
            gameUiClass.ViewResultUI(GameClearPanal);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        bool isHitBurstObj = collision.gameObject.tag == "Burst";
        if (isHitBurstObj) 
        {
            csButstofObj = null;
            HitBurstObjFlag = false; 
            return; 
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //**
        //* �߂荞�ݖh�~
        //**

        // �����ȕǂƏՓ˂����ꍇ�Ɉړ���Ԃ��~���A�������ړ��ʂ�0�ɂ���
        if ((collision.contactCount > 0) && (animator != null))
        {
            Vector3 collisionNormal = collision.contacts[0].normal;

            if (Mathf.Abs(collisionNormal.y) < 0.1f)
            {
                // 0�ԃC���f�b�N�X�̌��ʉ����~
                StopPlayingSound(0);

                // �ړ����x��������
                speed = initSpeed;

                // �A�j���[�^�[�̒l��ύX
                animator.SetBool("Move", false);
                animator.SetBool("Dash", false);

                // ���s�Ȉړ���������菜��
                Vector3 currentVelocity = rb.velocity;
                rb.velocity = new Vector3(0f, currentVelocity.y, 0f);

            }
        }
    }

}