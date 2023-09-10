﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultFormula : IStatFormula
{
	public float Calculate(List<Stat> baseStats)
	{
		float totalValue = 0;
		foreach (var stat in baseStats)
		{
			totalValue += stat.Value;
		}
		return totalValue;
	}
}