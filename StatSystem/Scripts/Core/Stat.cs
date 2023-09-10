using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StatSystem
{
	// The Stat class represents an individual statistic.
	public class Stat
	{
		public event Action<float> OnValueChanged;
		public event Action<StatModifier> OnModifierAdded;
		public event Action<StatModifier> OnModifierRemoved;
		public event Action OnDependentStatRecalculated;
		public event Action<StatCondition> OnConditionMet;

		public string Name { get; }
	
		public float Value
		{
			get => _value;
		
			private set
			{
				_value = value;
				OnValueChanged?.Invoke(_value);
			}
		}
	
		private float _value;
	
		public float MaxValue { get; }

		public Coroutine CurrentCoroutine { get; set; }
	
		public IStatFormula Formula { get; private set; } = new DefaultFormula();
	
		private List<StatModifier> statModifiers = new List<StatModifier>();
		private List<StatCondition> statConditions = new List<StatCondition>();
		private List<Stat> dependentStats = new List<Stat>();
	
		// Constructor to initialize a new Stat object.
		public Stat(StatData statData)
		{
			if (statData == null)
			{
				throw new ArgumentException("statData cannot be null.");
			}
			if (statData.initialValue < 0 || statData.maxValue < 0)
			{
				throw new ArgumentException("Initial and max values must be non-negative.");
			}
			if (statData.initialValue > statData.maxValue)
			{
				throw new ArgumentException("Initial value cannot be greater than max value.");
			}

			Name = statData.name;
			Value = statData.initialValue;
			MaxValue = statData.maxValue;
		}

		// Increases the stat value by a given amount.
		public void IncreaseStatValue(float amount)
		{
			if (amount < 0)
			{
				throw new ArgumentException("Amount must be non-negative.");
			}
		
			Value = Mathf.Min(Value + amount, MaxValue);
			
			CheckConditions();
		}

		// Decreases the stat value by a given amount.
		public void DecreaseStatValue(float amount)
		{
			if (amount < 0)
			{
				throw new ArgumentException("Amount must be non-negative.");
			}
		
			Value = Mathf.Max(Value - amount, 0);
			
			CheckConditions();
		}
	
	 public void AddModifier(StatModifier modifier)
		{
			statModifiers.Add(modifier);
			OnModifierAdded?.Invoke(modifier);
			RecalculateValue();
		}

		public void RemoveModifier(StatModifier modifier)
		{
			statModifiers.Remove(modifier);
			OnModifierRemoved?.Invoke(modifier);
			RecalculateValue();
		}
	
		public void AddDependentStat(Stat stat)
		{
			dependentStats.Add(stat);
		}
	
		public void RemoveDependentStat(Stat stat)
		{
			dependentStats.Remove(stat);
		}
	
		public void AddCondition(StatCondition condition)
		{
			statConditions.Add(condition);
		}

		public void CheckConditions()
		{
			foreach (var condition in statConditions)
			{
				if (condition.CheckCondition(this))
				{
					OnConditionMet?.Invoke(condition);
				}
			}
		}
	
		private void RecalculateValue()
		{
			if (Formula != null)
			{
				Value = Formula.Calculate(dependentStats);
				OnValueChanged?.Invoke(Value);
				return;
			}
		
			float finalValue = Value;

			// Apply additive modifiers
			foreach (var mod in statModifiers.Where(m => m.Type == StatModifierType.Additive))
			{
				finalValue += mod.Value;
			}

			// Apply multiplicative modifiers
			foreach (var mod in statModifiers.Where(m => m.Type == StatModifierType.Multiplicative))
			{
				finalValue *= mod.Value;
			}

			Value = finalValue;
			OnValueChanged?.Invoke(Value);
		
			// Recalculate dependent stats
			foreach (var stat in dependentStats)
			{
				stat.RecalculateValue();
			}
		
			CheckConditions();
		
			OnDependentStatRecalculated?.Invoke();
		}
	
		public void SetFormula(IStatFormula formula)
		{
			Formula = formula;
		}
		
		public List<StatModifier> GetStatModifierList()
		{
			return statModifiers;
		}
	
		public List<StatCondition> GetStatCondidtions()
		{
			return statConditions;
		}
	}
}