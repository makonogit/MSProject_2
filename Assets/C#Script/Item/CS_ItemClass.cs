using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------
//担当：菅
//Item管理クラス
//---------------------------------
public class CS_ItemClass
{
    public CS_ItemClass() { }

    /// <summary>
    /// ItemGet時のリソース計算
    /// </summary>
    /// <param 現在のリソース量="_Resource"></param>
    /// <param アイテムの回復量="_Itempoint"></param>
    /// <returns>回復したリソース量</returns>
    public float GetItem(float _Resource,float _Itempoint) 
    {
        //if(_Resource > ) { return 0f; }

        return _Resource + _Itempoint;
    }

    public void GetCore()
    {

    }
}
