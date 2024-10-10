using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

//---------------------------------
//�S���F��
//GameUI�Ǘ��N���X
//---------------------------------

namespace Assets.C_Script.C_GameUI
{

    public class CS_GameUIClass
    {
        //�R���X�g���N�^
        //CS_GameUIClass() {}

        //�R���X�g���N�^(�ő僊�\�[�X�l�ƃQ�[�W�̃T�C�Y�ŃQ�[�W�̊�����ݒ�)
        public CS_GameUIClass(float _Maxresource, RectTransform _gageTrans)
        {
            GageRate = _gageTrans.sizeDelta.x / _Maxresource;
            MaxResource = _Maxresource;
        }

        /// <summary>
        /// ���U���gUI�̕\��
        /// </summary>
        /// <param �N���AUI���Q�[���I�[�o�[UI="_GameResultUI"></param>
        public void ViewResultUI(GameObject _GameResultUI)
        {
            //�Q�[�����̎��Ԃ��~�߂�
            Time.timeScale = 0f;
            _GameResultUI.SetActive(true);

        }

        /// <summary>
        /// ���\�[�X�ω�
        /// </summary>
        /// <param �v���C���[�̌��݃��\�[�X="_Resource"></param>
        /// <param �ω���="_ChangeAmount"></param>
        ///  /// <param �Q�[�W�̃T�C�Y="_gageTrans"></param>
        public void ResourceChange(ref float _Resource, float _ChangeAmount, RectTransform _gageTrans)
        {
            //���\�[�X�̏㉺��
            bool resourcelimit = (_Resource <= 0 && _ChangeAmount < 0) || (_Resource >= MaxResource && _ChangeAmount > 0);
            if(resourcelimit) { return; }

            _Resource += _ChangeAmount;

            //�ω��ʂ���Q�[�W�̕���ݒ�
            Vector2 NowSizeDelta = _gageTrans.sizeDelta;
            NowSizeDelta.x += _ChangeAmount * GageRate;
            _gageTrans.sizeDelta = NowSizeDelta;

        }

        //�Q�[�W�̑�����
        private float GageRate = 1.0f;

        //���\�[�X�ő�l
        private float MaxResource = 1.0f;
    }
}
