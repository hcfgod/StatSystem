using UnityEngine;
using System;

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
}