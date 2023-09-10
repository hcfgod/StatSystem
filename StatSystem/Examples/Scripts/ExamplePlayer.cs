using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
	public class ExamplePlayer : MonoBehaviour
	{
		private StatManager _statManager;
	
		[SerializeField] private StatData healthStatData;
		[SerializeField] private StatData speedStatData;
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
			
			Stat speedStat = _statManager.GetStat(speedStatData);
			
			Stat agilityStat = _statManager.GetStat(agilityStatData);
			StatCondition highAgilityCondition = new StatCondition("Agility", 40, StatConditionType.GreaterThan);
			agilityStat.OnConditionMet += HighAgilityConditionMet;
			
			agilityStat.AddCondition(highAgilityCondition);
		
			Stat manaStat = _statManager.GetStat(manaStatData);
			manaStat.SetFormula(new ManaFormula(healthStat, agilityStat));
		
			Stat attackPowerStat = _statManager.GetStat(attackPowerStatData);
			attackPowerStat.AddDependentStat(healthStat);
			attackPowerStat.AddDependentStat(manaStat);	
			
			_statManager.GetStatInteractionManager().RegisterInteraction("Agility Interaction1", new SpeedAgilityInteraction());
			
			agilityStat.OnValueChanged += AgilityStatValueChanged;
		}
	
		private void AgilityStatValueChanged(float updatedValue)
		{
			Debug.Log(updatedValue.ToString());
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
				_statManager.GetStatInteractionManager().TriggerInteraction("Agility Interaction1", _statManager.GetStat(speedStatData), _statManager.GetStat(agilityStatData));
			}
		}
		
		private void OnDestroy()
		{
			_statManager.GetStat(agilityStatData).OnValueChanged -= AgilityStatValueChanged;
		}
	}
}
