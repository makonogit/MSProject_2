using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_Title : MonoBehaviour
{
    [SerializeField,Tooltip("�Q�[���J�n���̃V�[���̖��O")]
    private string GameSceneName;

    /// <summary>
    /// �Q�[���X�^�[�g
    /// </summary>
    public void StartGame()
    {
        //�t�F�[�Y1�̊J�n
        SceneManager.LoadScene(GameSceneName);
    }


    /// <summary>
    /// �Q�[���I��
    /// </summary>
    public void EndGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif 

    }


}
