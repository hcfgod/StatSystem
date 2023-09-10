using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
	public class ExamplePlayer : MonoBehaviour
	{
		private StatManager _statManager;
	
		[SerializeField] private StatData healthStatData;
		[SerializeField] private StatData manaStatData;
		[SerializeField] private StatData attackPowerStatData;
		[SerializeField] private StatData agilityStatData;
	
		private void Awake()
		{
			_statManager = GetComponent<StatManager>();
		}
	
		private void Start()
		{
			Stat healthStat = _statManager.GetStat(healthStatData);
			
			StatModifier healthBoost = new StatModifier(20, StatModifierType.Additive, 30);
			healthStat.AddModifier(healthBoost);

			StatCondition lowHealthCondition = new StatCondition("Health", 50, StatConditionType.LessThan);
			healthStat.AddCondition(lowHealthCondition);
			
			Stat agilityStat = _statManager.GetStat(agilityStatData);
			StatCondition highAgilityCondition = new StatCondition("Agility", 40, StatConditionType.GreaterThan);
			StatCondition lowAgilityCondition = new StatCondition("Agility", 20, StatConditionType.LessThan);

			agilityStat.OnConditionMet += HighAgilityConditionMet;
			agilityStat.OnConditionMet += LowAgilityConditionMet;
			
			agilityStat.AddCondition(highAgilityCondition);
			agilityStat.AddCondition(lowAgilityCondition);
		
			Stat manaStat = _statManager.GetStat(manaStatData);
			manaStat.SetFormula(new ManaFormula(healthStat, agilityStat));
		
			Stat attackPowerStat = _statManager.GetStat(attackPowerStatData);
			attackPowerStat.AddDependentStat(healthStat);
			attackPowerStat.AddDependentStat(manaStat);			
		}
	
		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.K))
			{
				_statManager.GetStat(agilityStatData).DecreaseStatValue(10);
			}
			if(Input.GetKeyDown(KeyCode.L))
			{
				_statManager.GetStat(agilityStatData).IncreaseStatValue(10);
			}
		}
		
		public void HighAgilityConditionMet(StatCondition condition)
		{
			if (condition.StatName == "Agility" && condition.Type == StatConditionType.GreaterThan)
			{
				Debug.Log("High Agility Condition Met");
			}
		}
		
		public void LowAgilityConditionMet(StatCondition condition)
		{
			if (condition.StatName == "Agility" && condition.Type == StatConditionType.LessThan)
			{
				Debug.Log("Low Agility Condition Met");
			}
		}
	}
}
