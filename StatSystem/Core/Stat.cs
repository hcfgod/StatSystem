using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

// The Stat class represents an individual statistic.
public class Stat
{
	// Event triggered when the stat value changes.
	public event Action<float> OnValueChanged;

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
	
	private List<StatModifier> statModifiers = new List<StatModifier>();
	private List<Stat> dependentStats = new List<Stat>();
	
	// Constructor to initialize a new Stat object.
	public Stat(string name, float initialValue, float maxValue)
	{
		if (string.IsNullOrEmpty(name))
		{
			throw new ArgumentException("Name cannot be null or empty.");
		}
		if (initialValue < 0 || maxValue < 0)
		{
			throw new ArgumentException("Initial and max values must be non-negative.");
		}
		if (initialValue > maxValue)
		{
			throw new ArgumentException("Initial value cannot be greater than max value.");
		}

		Name = name;
		Value = initialValue;
		MaxValue = maxValue;
	}

    // Increases the stat value by a given amount.
	public void IncreaseStatValue(float amount)
	{
		if (amount < 0)
		{
			throw new ArgumentException("Amount must be non-negative.");
		}
		
		Value = Mathf.Min(Value + amount, MaxValue);
	}

	// Decreases the stat value by a given amount.
	public void DecreaseStatValue(float amount)
	{
		if (amount < 0)
		{
			throw new ArgumentException("Amount must be non-negative.");
		}
		
		Value = Mathf.Max(Value - amount, 0);
	}
	
	 public void AddModifier(StatModifier modifier)
	{
		statModifiers.Add(modifier);
		RecalculateValue();
	}

	public void RemoveModifier(StatModifier modifier)
	{
		statModifiers.Remove(modifier);
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
	
	private void RecalculateValue()
	{
		float finalValue = Value;

		// Apply additive modifiers
		foreach (var mod in statModifiers.Where(m => m.Type == StatModifier.ModifierType.Additive))
		{
			finalValue += mod.Value;
		}

		// Apply multiplicative modifiers
		foreach (var mod in statModifiers.Where(m => m.Type == StatModifier.ModifierType.Multiplicative))
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
	}
	
	public List<StatModifier> GetStatModifierList()
	{
		return statModifiers;
	}
}