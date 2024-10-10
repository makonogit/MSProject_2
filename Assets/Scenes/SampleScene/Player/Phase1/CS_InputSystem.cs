using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// InputSystem��c#�ň������߂̃X�N���v�g
public class CS_InputSystem : MonoBehaviour
{
    //**
    //* �C���v�b�g�V�X�e��
    //**

    // �C���X�^���X
    private InputSystem inputSystem;

    // Dpad�̓��͏��
    private bool isDpadUpTriggered;
    private bool isDpadDownTriggered;
    private bool isDpadLeftTriggered;
    private bool isDpadRightTriggered;
    private bool isDpadUpPressed;
    private bool isDpadDownPressed;
    private bool isDpadLeftPressed;
    private bool isDpadRightPressed;

    // �g���K�[�̓��͏��
    private float leftTrigger;
    private float rightTrigger;

    // �{�^���̓��͏��
    private bool isButtonATriggered;
    private bool isButtonBTriggered;
    private bool isButtonYTriggered;
    private bool isButtonXTriggered;
    private bool isButtonLTriggered;
    private bool isButtonRTriggered;
    private bool isButtonAPressed;
    private bool isButtonBPressed;
    private bool isButtonYPressed;
    private bool isButtonXPressed;
    private bool isButtonRPressed;
    private bool isButtonLPressed;

    // �X�e�b�N�̓��͏��
    private bool isLeftStick;
    private bool isRightStick;
    private Vector2 leftStick;
    private Vector2 rightStick;
    private bool isLeftStickPush;
    private bool isRightStickPush;


    private void Awake()
    {
        // �A�N�V�����A�Z�b�g���琶�����ꂽ�N���X���C���X�^���X��
        inputSystem = new InputSystem();
    }

    private void OnEnable()
    {
        // �A�N�V�����}�b�v��L����
        inputSystem.Enable();
    }

    private void OnDisable()
    {
        // �A�N�V�����}�b�v�𖳌���
        inputSystem.Disable();
    }

    void Update()
    {
        isDpadUpPressed = IsDpadUpPressed();
        isDpadDownPressed = IsDpadDownPressed();
        isDpadLeftPressed = IsDpadLeftPressed();
        isDpadRightPressed = IsDpadRightPressed();

        isDpadUpTriggered = IsDpadUpTriggered();
        isDpadDownTriggered = IsDpadDownTriggered();
        isDpadLeftTriggered = IsDpadLeftTriggered();
        isDpadRightTriggered = IsDpadRightTriggered();

        leftTrigger = inputSystem.Controller.Trigger_L.ReadValue<float>();
        rightTrigger = inputSystem.Controller.Trigger_R.ReadValue<float>();

        isButtonAPressed = IsButtonAPressed();
        isButtonBPressed = IsButtonBPressed();
        isButtonYPressed = IsButtonYPressed();
        isButtonXPressed = IsButtonXPressed();
        isButtonRPressed = IsButtonRPressed();
        isButtonLPressed = IsButtonLPressed();

        isButtonRTriggered = IsButtonRTriggered();
        isButtonLTriggered = IsButtonLTriggered();
        isButtonATriggered = IsButtonATriggered();
        isButtonBTriggered = IsButtonBTriggered();
        isButtonYTriggered = IsButtonYTriggered();
        isButtonXTriggered = IsButtonXTriggered();

        isLeftStick = IsLeftStickActive(0.5f);
        isRightStick = IsRightStickActive(0.5f);
        leftStick = inputSystem.Controller.Stick_L.ReadValue<Vector2>();
        rightStick = inputSystem.Controller.Stick_R.ReadValue<Vector2>();

        isLeftStickPush = IsLeftStickPush();
        isRightStickPush = IsRightStickPush();
    }

    // �Q�b�^�[
    public bool GetDpadUpPressed() => IsDpadUpPressed();
    public bool GetDpadDownPressed() => IsDpadDownPressed();
    public bool GetDpadRightPressed() => IsDpadRightPressed();
    public bool GetDpadLeftPressed() => IsDpadLeftPressed();
    public bool GetDpadUpTriggered() => IsDpadUpTriggered();
    public bool GetDpadDownTriggered() => IsDpadDownTriggered();
    public bool GetDpadRightTriggered() => IsDpadRightTriggered();
    public bool GetDpadLeftTriggered() => IsDpadLeftTriggered();

    public float GetLeftTrigger() => leftTrigger;
    public float GetRightTrigger() => rightTrigger;

    public bool GetButtonAPressed() => isButtonAPressed;
    public bool GetButtonBPressed() => isButtonBPressed;
    public bool GetButtonXPressed() => isButtonXPressed;
    public bool GetButtonYPressed() => isButtonYPressed;
    public bool GetButtonRPressed() => isButtonRPressed;
    public bool GetButtonLPressed() => isButtonLPressed;

    public bool GetButtonRTriggered() => isButtonRTriggered;
    public bool GetButtonLTriggered() => isButtonLTriggered;
    public bool GetButtonATriggered() => isButtonATriggered;
    public bool GetButtonBTriggered() => isButtonBTriggered;
    public bool GetButtonXTriggered() => isButtonXTriggered;
    public bool GetButtonYTriggered() => isButtonYTriggered;

    public bool GetLeftStickActive() => isLeftStick;
    public bool GetRightStickActive() => isRightStick;

    public Vector2 GetLeftStick() => leftStick;
    public Vector2 GetRightStick() => rightStick;

    public bool GetLeftStickPush()=> isLeftStickPush;
    public bool GetRightStickPush() => isRightStickPush;






    // Dpad
    private bool IsDpadUpPressed() => inputSystem.Controller.Dpad_up.ReadValue<float>() > 0.1f;
    private bool IsDpadDownPressed() => inputSystem.Controller.Dpad_down.ReadValue<float>() > 0.1f;
    private bool IsDpadRightPressed() => inputSystem.Controller.Dpad_right.ReadValue<float>() > 0.1f;
    private bool IsDpadLeftPressed() => inputSystem.Controller.Dpad_left.ReadValue<float>() > 0.1f;

    private bool IsDpadUpTriggered() => inputSystem.Controller.Dpad_up.triggered;
    private bool IsDpadDownTriggered() => inputSystem.Controller.Dpad_down.triggered;
    private bool IsDpadRightTriggered() => inputSystem.Controller.Dpad_right.triggered;
    private bool IsDpadLeftTriggered() => inputSystem.Controller.Dpad_left.triggered;

    // �g���K�[
    //private float GetLeftTrigger() => inputSystem.Controller.Trigger_L.ReadValue<float>();
    //private float GetRightTrigger() => inputSystem.Controller.Trigger_R.ReadValue<float>();

    // �{�^��
    private bool IsButtonAPressed() => inputSystem.Controller.Button_A.ReadValue<float>() > 0.1f;
    private bool IsButtonBPressed() => inputSystem.Controller.Button_B.ReadValue<float>() > 0.1f;
    private bool IsButtonYPressed() => inputSystem.Controller.Button_Y.ReadValue<float>() > 0.1f;
    private bool IsButtonXPressed() => inputSystem.Controller.Button_X.ReadValue<float>() > 0.1f;
    private bool IsButtonRPressed() => inputSystem.Controller.Button_R.ReadValue<float>() > 0.1f;
    private bool IsButtonLPressed() => inputSystem.Controller.Button_L.ReadValue<float>() > 0.1f;


    private bool IsButtonATriggered() => inputSystem.Controller.Button_A.triggered;
    private bool IsButtonBTriggered() => inputSystem.Controller.Button_B.triggered;
    private bool IsButtonYTriggered() => inputSystem.Controller.Button_Y.triggered;
    private bool IsButtonXTriggered() => inputSystem.Controller.Button_X.triggered;
    private bool IsButtonRTriggered() => inputSystem.Controller.Button_R.triggered;
    private bool IsButtonLTriggered() => inputSystem.Controller.Button_L.triggered;

    // �X�e�B�b�N
    private bool IsLeftStickActive(float min)
    {
        var input = inputSystem.Controller.Stick_L.ReadValue<Vector2>();
        return Mathf.Abs(input.x) > min || Mathf.Abs(input.y) > min;
    }

    private bool IsRightStickActive(float min)
    {
        var input = inputSystem.Controller.Stick_R.ReadValue<Vector2>();
        return Mathf.Abs(input.x) > min || Mathf.Abs(input.y) > min;
    }

    private bool IsLeftStickPush() => inputSystem.Controller.Stick_L_Press.ReadValue<float>() > 0.1f;
    private bool IsRightStickPush() => inputSystem.Controller.Stick_R_Press.ReadValue<float>() > 0.1f;



    //private Vector2 GetLeftStick() => inputSystem.Controller.Stick_L.ReadValue<Vector2>();
    //private Vector2 GetRightStick() => inputSystem.Controller.Stick_R.ReadValue<Vector2>();

}
