using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier
{
	public float Value;
	public enum ModifierType { Additive, Multiplicative }
	public ModifierType Type;
	public float Duration;  // set to -1 for permanent modifiers

	public StatModifier(float value, ModifierType type, float duration = -1)
	{
		Value = value;
		Type = type;
		Duration = duration;
	}
}