using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
	public interface IStatFormula
	{
		public float Calculate(List<Stat> baseStats);
	}
}
