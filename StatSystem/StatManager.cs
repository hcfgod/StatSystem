using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatManager : MonoBehaviour, IDisposable
{
	public List<StatData> statDataList; // Assign in Unity Editor

	private StatSystem statSystem;

	void Awake()
	{
		statSystem = new StatSystem();
		InitializeStats();
	}

	private void InitializeStats()
	{
		foreach (var data in statDataList)
		{
			AddStat(data);
		}
	}

	public void AddStat(StatData statData)
	{
		Stat newStat = new Stat(name, statData.initialValue, statData.maxValue);
		newStat.OnValueChanged += HandleStatValueChanged;
		statSystem.AddStat(statData, newStat);
	}
	
	public Stat GetStat(StatData statData)
	{
		return statSystem.GetStat(statData);
	}

	public void IncreaseStatValue(StatData statData, float amount)
	{
		statSystem.IncreaseStatValue(statData, amount);
	}

	public void DecreaseStatValue(StatData statData, float amount)
	{
		statSystem.DecreaseStatValue(statData, amount);
	}
	
	public void RegenerateStatValueOverTime(StatData statData, float delay, float regenAmount)
	{
		Stat stat = GetStat(statData);
		if (stat == null) return;

		stat.CurrentCoroutine = StartCoroutine(statSystem.RegenerateOverTime(this, stat, delay, regenAmount));
	}

	public void DepleteStatValueOverTime(StatData statData, float delay, float decrementAmount)
	{
		Stat stat = GetStat(statData);
		if (stat == null) return;
    
		stat.CurrentCoroutine = StartCoroutine(statSystem.DepleteOverTime(this, stat, delay, decrementAmount));
	}
	
	public void StopStatCoroutine(StatData statData)
	{
		Stat stat = GetStat(statData);
		if (stat == null) return;
		
		if(stat.CurrentCoroutine == null) return;
			
		StopCoroutine(stat.CurrentCoroutine);
		stat.CurrentCoroutine = null;
	}
	
	private void HandleStatValueChanged(float newValue)
	{
		Debug.Log("Stat changed. New value: " + newValue);
	}
	
	public void Dispose()
	{
		foreach(Stat stat in statSystem.GetStatDictionary().Values)
		{
			stat.OnValueChanged -= HandleStatValueChanged;
		}
	}
}
