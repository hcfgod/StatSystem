using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
	public class StatInteractionManager
	{
		private Dictionary<string, List<Action<Stat>>> interactions = new Dictionary<string, List<Action<Stat>>>();

		public void RegisterInteraction(string statName, Action<Stat> interaction)
		{
			if (!interactions.ContainsKey(statName))
			{
				interactions[statName] = new List<Action<Stat>>();
			}
			interactions[statName].Add(interaction);
		}

		public void TriggerInteractions(Stat stat)
		{
			if (interactions.ContainsKey(stat.Name))
			{
				foreach (var interaction in interactions[stat.Name])
				{
					interaction(stat);
				}
			}
		}
	}
}
