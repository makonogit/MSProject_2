using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------
//�S���F��
//Item�Ǘ��N���X
//---------------------------------
public class CS_ItemClass
{
    public CS_ItemClass() { }

    /// <summary>
    /// ItemGet���̃��\�[�X�v�Z
    /// </summary>
    /// <param ���݂̃��\�[�X��="_Resource"></param>
    /// <param �A�C�e���̉񕜗�="_Itempoint"></param>
    /// <returns>�񕜂������\�[�X��</returns>
    public float GetItem(float _Resource,float _Itempoint) 
    {
        //if(_Resource > ) { return 0f; }

        return _Resource + _Itempoint;
    }

    public void GetCore()
    {

    }
}
