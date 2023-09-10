using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatFormula
{
	float Calculate(List<Stat> baseStats);
}
