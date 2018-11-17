using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using O2F;

[Serializable]
public class Price : MonoBehaviour
{
	[TextField(ETextFieldType.Float)]
	public float usd;

	[TextField(ETextFieldType.Float)]
	public float vic;

	public Price(float usd, float vic)
	{
		this.usd = usd;
		this.vic = vic;
	}
}