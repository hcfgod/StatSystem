using UnityEngine;
using System;

public class Stat
{
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
	
	public Stat(string name, float initialValue, float maxValue)
	{
		Name = name;
		Value = initialValue;
		MaxValue = maxValue;
	}

	public void IncreaseStatValue(float amount)
	{
		Value = Mathf.Min(Value + amount, MaxValue);
	}

	public void DecreaseStatValue(float amount)
	{
		Value = Mathf.Max(Value - amount, 0);
	}
}