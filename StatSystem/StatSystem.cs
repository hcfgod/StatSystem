using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatSystem
{
	private Dictionary<StatData, Stat> stats = new Dictionary<StatData, Stat>();

	public void AddStat(StatData statData, Stat stat)
	{		
		stats[statData] = stat;
	}

	public Stat GetStat(StatData statData)
	{
		stats.TryGetValue(statData, out Stat stat);
		return stat;
	}
	
	public void IncreaseStatValue(StatData statData, float amount)
	{
		Stat stat = GetStat(statData);
		
		if (stat != null)
		{
			stat.IncreaseStatValue(amount);
		}
	}

	public void DecreaseStatValue(StatData statData, float amount)
	{
		Stat stat = GetStat(statData);
		
		if (stat != null)
		{
			stat.DecreaseStatValue(amount);
		}
	}

	public IEnumerator RegenerateOverTime(MonoBehaviour host, Stat stat, float delay, float regenAmount)
	{
		if (stat == null) yield break;

		while (stat.Value < stat.MaxValue)
		{
			stat.IncreaseStatValue(regenAmount);
			yield return new WaitForSeconds(delay);
		}
	}

	public IEnumerator DepleteOverTime(MonoBehaviour host, Stat stat, float delay, float decrementAmount)
	{
		if (stat == null) yield break;

		while (stat.Value > 0)
		{
			stat.DecreaseStatValue(decrementAmount);
			yield return new WaitForSeconds(delay);
		}
	}
	
	public  Dictionary<StatData, Stat> GetStatDictionary()
	{
		return stats;
	}
}
