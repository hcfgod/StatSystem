using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
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

	private void HandleStatValueChanged(float newValue)
	{
		Debug.Log("Stat changed. New value: " + newValue);
	}
	
	public Stat GetStat(StatData statData)
	{
		return statSystem.GetStat(statData);
	}

	public void Increase(StatData statData, float amount)
	{
		statSystem.Increase(statData, amount);
	}

	public void Decrease(StatData statData, float amount)
	{
		statSystem.Decrease(statData, amount);
	}
	
	public void RegenerateOverTime(StatData statData, float delay, float regenAmount)
	{
		Stat stat = GetStat(statData);
		if (stat == null) return;

		stat.CurrentCoroutine = StartCoroutine(statSystem.RegenerateOverTime(this, stat, delay, regenAmount));
	}

	public void DepleteOverTime(StatData statData, float delay, float decrementAmount)
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
}
