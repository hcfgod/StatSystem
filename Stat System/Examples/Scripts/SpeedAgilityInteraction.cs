using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
	public class SpeedAgilityInteraction : IStatInteraction
	{
		public void ApplyInteraction(Stat speed, Stat agility)
		{
			// Custom logic to interact speed and agility
			if (agility.Value > 50)
			{
				speed.IncreaseStatValue(2);
			}
			else
			{
				speed.DecreaseStatValue(1);
			}
		}
	}
}
