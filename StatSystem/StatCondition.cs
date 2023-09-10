using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCondition
{
	public string StatName { get; private set; }
	public float Threshold { get; private set; }
	public enum ConditionType { LessThan, GreaterThan, EqualTo }
	public ConditionType Type { get; private set; }

	public StatCondition(string statName, float threshold, ConditionType type)
	{
		StatName = statName;
		Threshold = threshold;
		Type = type;
	}

	public bool CheckCondition(Stat stat)
	{
		switch (Type)
		{
		case ConditionType.LessThan:
			return stat.Value < Threshold;
		case ConditionType.GreaterThan:
			return stat.Value > Threshold;
		case ConditionType.EqualTo:
			return stat.Value == Threshold;
		default:
			return false;
		}
	}
}