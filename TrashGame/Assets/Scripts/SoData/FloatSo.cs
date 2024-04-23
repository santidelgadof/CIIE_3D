using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatOs : ScriptableObject
{
	[SerializeField]
	private float mValue;

	public float Value
	{
		get { return mValue; }
		set { mValue = value; }
	}

}
