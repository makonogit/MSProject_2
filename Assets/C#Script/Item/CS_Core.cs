using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_Core : MonoBehaviour
{
    [SerializeField, Header("�J��Scene��")]
    private string Phase2SceneName;

    private void OnCollisionEnter(Collision collision)
    {
        //�v���C���[�ƏՓ˂�����
        bool PlayerHit = collision.transform.tag == "Player" && Phase2SceneName != null;

        if (PlayerHit)
        {
            SceneManager.LoadScene(Phase2SceneName);
        }
    }
}
