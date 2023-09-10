using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
	public class ManaFormula : MonoBehaviour, IStatFormula
	{
		private Stat healthStat;
		private Stat agilityStat;

		public ManaFormula(Stat healthStat, Stat agilityStat)
		{
			this.healthStat = healthStat;
			this.agilityStat = agilityStat;
		}

		public float Calculate(List<Stat> baseStats)
		{
			return healthStat.Value * 0.3f + agilityStat.Value * 0.7f;
		}
	}
}