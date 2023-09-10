using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
	public class ManaFormula : MonoBehaviour, IStatFormula
	{
		private Stat _healthStat;
		
		public ManaFormula(Stat healthStat)
		{
			_healthStat = healthStat;
		}
		
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
}