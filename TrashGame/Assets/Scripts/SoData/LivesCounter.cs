using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LivesCounter : ScriptableObject
{
    [SerializeField]
    private int mValue = 5;

    public int Lives
    {
        get { return mValue; }
        set { mValue = value; }
    }

}
