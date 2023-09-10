using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatData", menuName = "Stats/New Stat")]
public class StatData : ScriptableObject
{
	public string statName;
	public float initialValue;
	public float maxValue;
}
