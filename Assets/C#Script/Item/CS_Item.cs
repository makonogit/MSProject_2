using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Item : MonoBehaviour
{
    [SerializeField,Header("アイテムの回復ポイント")]
    private float ItemPoint;

    public float POINT
    {
        get
        {
            return ItemPoint;
        }
    }

}
