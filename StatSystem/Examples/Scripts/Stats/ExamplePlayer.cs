using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
	public class ExamplePlayer : MonoBehaviour
	{
		[SerializeField] private StatData healthStatData;
		[SerializeField] private StatData manaStatData;
		[SerializeField] private StatData attackPowerStatData;
		[SerializeField] private StatData agilityStatData;
		
		void Start()
		{
			Stat healthStat = new Stat(healthStatData);
			
			StatModifier healthBoost = new StatModifier(20, StatModifierType.Additive, 30);
			healthStat.AddModifier(healthBoost);

			StatCondition lowHealthCondition = new StatCondition("Health", 50, StatConditionType.LessThan);
			healthStat.AddCondition(lowHealthCondition);
			
			Stat manaStat = new Stat(manaStatData);
			manaStat.SetFormula(new ManaFormula(healthStat));
			
			Stat attackPowerStat = new Stat(attackPowerStatData);
			attackPowerStat.AddDependentStat(healthStat);
			attackPowerStat.AddDependentStat(manaStat);
			
			Stat agilityStat = new Stat(agilityStatData);
			StatCondition highAgilityCondition = new StatCondition("Agility", 40, StatConditionType.GreaterThan);
			StatCondition lowAgilityCondition = new StatCondition("Agility", 20, StatConditionType.LessThan);

			agilityStat.AddCondition(highAgilityCondition);
			agilityStat.AddCondition(lowAgilityCondition);
		}
	}
}
